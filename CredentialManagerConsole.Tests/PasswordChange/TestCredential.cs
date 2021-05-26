using CredentialManagerConsole.PasswordChange;

namespace CredentialManagerConsole.Tests.PasswordChange
{
    public class TestCredential : ICredential
    {
        private string _newPassword;

        public string Username { get; }

        public TestCredential(string username)
        {
            Username = username;
        }

        public void ChangePassword(string newPassword)
        {
            _newPassword = newPassword;
        }

        public bool PasswordWasChangedTo(string expectedPassword)
        {
            return _newPassword?.Equals(expectedPassword) ?? false;
        }

        public bool? PasswordWasNotChanged()
        {
            return _newPassword == null;
        }
    }
}