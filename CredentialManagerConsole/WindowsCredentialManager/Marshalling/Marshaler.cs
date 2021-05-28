using System;
using System.Net.Http;
using System.Runtime.InteropServices;

namespace CredentialManagerConsole.WindowsCredentialManager.Marshalling
{
    internal class Marshaler
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal class NativeCredential
        {
            public uint Flags;
            public CredentialType Type;
            public string TargetName;
            public string Comment;
            public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
            public uint CredentialBlobSize;
            public IntPtr CredentialBlob;
            public CredentialPersistence Persist;
            public uint AttributeCount;
            public IntPtr Attributes;
            public string TargetAlias;
            public string UserName;
        }

        public static Result<Credential> MarshalCredentialInstance(IntPtr pNativeData)
        {
            var rawNativeCredential = (NativeCredential)Marshal.PtrToStructure(pNativeData, typeof(NativeCredential));

            if (rawNativeCredential.AttributeCount > 0)
            {
                return Result<Credential>.Fail(
                    $"Credential {rawNativeCredential.TargetName} has more than 0 attributes. This is currently not supported");
            }

            var lCredential = new Credential()
            {
                UserName = rawNativeCredential.UserName,
                TargetName = rawNativeCredential.TargetName,
                TargetAlias = rawNativeCredential.TargetAlias,
                Persist = rawNativeCredential.Persist,
                Comment = rawNativeCredential.Comment,
                Flags = rawNativeCredential.Flags,
                LastWritten = rawNativeCredential.LastWritten,
                Type = rawNativeCredential.Type,
                CredentialBlob = new byte[rawNativeCredential.CredentialBlobSize],
                Attributes = new CredentialAttribute[rawNativeCredential.AttributeCount]
            };

            if (rawNativeCredential.CredentialBlob != IntPtr.Zero)
            {
                Marshal.Copy(rawNativeCredential.CredentialBlob, lCredential.CredentialBlob, 0,
                    (int)rawNativeCredential.CredentialBlobSize);
            }

            return Result<Credential>.Success(lCredential);
        }
    }
}