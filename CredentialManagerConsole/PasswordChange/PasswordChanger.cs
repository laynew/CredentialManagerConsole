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
            foreach (var credential in _credentialStore.ReadCredentials().Where(c => c.Username == username))
            {
                _passwordChangeHandler.RequestPasswordChange(credential, newPassword);
            }
        }
    }
}