namespace CredentialManagerConsole.WindowsCredentialManager
{
    public class Credential
    {
        public uint Flags;
        public CredentialType Type;
        public string TargetName;
        public string Comment;
        public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
        public byte[] CredentialBlob;
        public CredentialPersistence Persist;
        public CredentialAttribute[] Attributes;
        public string TargetAlias;
        public string UserName;
    }
}