using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using CredentialManagerConsole.PasswordChange;
using CredentialManagerConsole.WindowsCredentialManager.Marshalling;

namespace CredentialManagerConsole.WindowsCredentialManager
{
    public class CredentialStore : ICredentialStore
    {
        private static void ChangePassword(Credential credential, string newPassword)
        {
            var result = SaveCredential(credential, newPassword);

            if (!result)
            {
                Console.WriteLine($"Failed to change password for {credential}.");
            }
        }

        private static bool SaveCredential(Credential credential, string password)
        {
            var passwordUnicodeBytesLength = (uint)Encoding.Unicode.GetBytes(password).Length;

            var nativePassword = Marshal.StringToHGlobalUni(password);

            var nativeCredential = new Marshaler.NativeCredential
            {
                Flags = credential.Flags,
                Type = credential.Type,
                TargetName = credential.TargetName,
                CredentialBlob = nativePassword,
                CredentialBlobSize = passwordUnicodeBytesLength,
                Persist = credential.Persist,
                UserName = credential.UserName
            };

            var result = NativeInterop.CredWrite(nativeCredential, 0);
            return result;
        }

        public IReadOnlyList<ICredential> ReadCredentials()
        {
            return EnumerateCredentials()
                .Select(c => new CredentialAdapter(c, ChangePassword))
                .ToList();
        }

        private static IEnumerable<Credential> EnumerateCredentials()
        {
            IntPtr data = default;

            try
            {
                var result = NativeInterop.CredEnumerate(null, 0, out var count, out data);

                if (!result)
                {
                    return null;
                }

                var credentials = new List<Credential>();

                for (var index = 0; index < count; index++)
                {
                    var currentData = Marshal.ReadIntPtr(data + index * Marshal.SizeOf<IntPtr>());
                    var credential = Marshaler.MarshalCredentialInstance(currentData);
                    credentials.Add(credential);
                }

                return credentials;
            }
            finally
            {
                if (data != IntPtr.Zero)
                {
                    NativeInterop.CredFree(data);
                }
            }
        }
    }
}
