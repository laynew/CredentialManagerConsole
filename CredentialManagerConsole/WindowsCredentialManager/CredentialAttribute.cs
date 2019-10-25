using System;
using System.Runtime.InteropServices;

namespace CredentialManagerConsole.WindowsCredentialManager
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct CredentialAttribute
    {
        string Keyword;
        uint Flags;
        uint ValueSize;
        IntPtr Value;
    }
}