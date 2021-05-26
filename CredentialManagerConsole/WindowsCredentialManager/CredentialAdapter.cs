using System;
using CredentialManagerConsole.PasswordChange;

namespace CredentialManagerConsole.WindowsCredentialManager
{
    public class CredentialAdapter : ICredential
    {
        private readonly Action<Credential, string> _changePassword;
        public Credential Credential { get; }

        public string Username => Credential.UserName;

        public CredentialAdapter(Credential credential, Action<Credential, string> changePassword)
        {
            _changePassword = changePassword;
            Credential = credential;
        }

        public void ChangePassword(string newPassword)
        {
            _changePassword(Credential, newPassword);
        }

        public override string ToString()
        {
            return $"{Credential.UserName}:{Credential.TargetName}";
        }
    }
}