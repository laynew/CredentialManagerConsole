using System;
using System.Linq;

namespace CredentialManagerConsole.PasswordChange
{
    public class PasswordChanger
    {
        private readonly ICredentialStore _credentialStore;

        public PasswordChanger(ICredentialStore credentialStore)
        {
            _credentialStore = credentialStore;
        }

        public void ChangePasswordForUsername(string username, string newPassword)
        {
            var matchedCredentials = _credentialStore.ReadCredentials() 
                .Where(c => c.Username != null && c.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            foreach (var credential in matchedCredentials)
            {
                credential.ChangePassword(newPassword);
            }
        }
    }
}