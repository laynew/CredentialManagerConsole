using System;
using System.Runtime.InteropServices;
using CredentialManagerConsole.WindowsCredentialManager.Marshalling;

namespace CredentialManagerConsole.WindowsCredentialManager
{
    internal static class NativeInterop
    {
        [DllImport("advapi32", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CredEnumerate(
            string filter,
            int flag,
            out int count,
            out IntPtr pCredentials);

        [DllImport("advapi32", SetLastError = true)]
        public static extern bool CredFree([In] IntPtr buffer);

        [DllImport("advapi32", EntryPoint = "CredWriteW", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CredWrite(Marshaler.NativeCredential data, int flag);
    }
}