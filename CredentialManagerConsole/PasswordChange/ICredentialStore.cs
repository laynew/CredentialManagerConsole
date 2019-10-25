using System.Collections.Generic;

namespace CredentialManagerConsole.PasswordChange
{
    public interface ICredentialStore
    {
        IReadOnlyList<ICredential> ReadCredentials();
    }
}