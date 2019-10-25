using CredentialManagerConsole.PasswordChange;

namespace CredentialManagerConsole.Tests.PasswordChange
{
    public class TestCredential : ICredential
    {
        public string Username { get; }

        public TestCredential(string username)
        {
            Username = username;
        }
    }
}