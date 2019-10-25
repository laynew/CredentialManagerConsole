namespace CredentialManagerConsole.PasswordChange
{
    public interface IPasswordChangeHandler
    {
        void RequestPasswordChange(ICredential credential, string newPassword);
    }
}