using CredentialManagerConsole.PasswordChange;

namespace CredentialManagerConsole.WindowsCredentialManager
{
    public class CredentialAdapter : ICredential
    {
        public Credential Credential { get; }

        public string Username => Credential.UserName;

        public CredentialAdapter(Credential credential)
        {
            Credential = credential;
        }

        public override string ToString()
        {
            return $"{Credential.UserName}:{Credential.TargetName}";
        }
    }
}