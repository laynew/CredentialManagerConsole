namespace CredentialManagerConsole.PasswordChange
{
    public interface ICredential
    {
        string Username { get; }
        void ChangePassword(string newPassword);
    }
}