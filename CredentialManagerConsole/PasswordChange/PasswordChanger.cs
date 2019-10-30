using System;
using System.Linq;

namespace CredentialManagerConsole.PasswordChange
{
    public class PasswordChanger
    {
        private readonly ICredentialStore _credentialStore;
        private readonly IPasswordChangeHandler _passwordChangeHandler;

        public PasswordChanger(IPasswordChangeHandler passwordChangeHandler, ICredentialStore credentialStore)
        {
            _passwordChangeHandler = passwordChangeHandler;
            _credentialStore = credentialStore;
        }

        public void ChangePasswordForUsername(string username, string newPassword)
        {
            var matchedCredentials = _credentialStore.ReadCredentials() 
                .Where(c => c.Username != null && c.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            foreach (var credential in matchedCredentials)
            {
                _passwordChangeHandler.RequestPasswordChange(credential, newPassword);
            }
        }
    }
}