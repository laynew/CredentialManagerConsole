using System.Collections.Generic;
using CredentialManagerConsole.PasswordChange;
using FakeItEasy;
using NUnit.Framework;

namespace CredentialManagerConsole.Tests.PasswordChange
{
    [TestFixture]
    public class ChangePasswordTests
    {
        [Test]
        public void ChangePassword_ForUsernameMatchingStoredCredential_ShouldRequestsPasswordChangeForMatchingCredential()
        {
            var passwordChangeHandler = A.Fake<IPasswordChangeHandler>();
            var credentialStore = A.Fake<ICredentialStore>();
            var testCredential = new TestCredential("username");
            A.CallTo(() => credentialStore.ReadCredentials())
                .Returns(new List<ICredential>
                {
                    testCredential
                });

            var sut = new PasswordChanger(passwordChangeHandler, credentialStore);
            sut.ChangePasswordForUsername("username", "new_password");

            A.CallTo(() => passwordChangeHandler.RequestPasswordChange(testCredential, "new_password"))
                .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ChangePassword_DoesNotRequestAnyChanges_WhenCredentialStoreIsEmpty()
        {
            var passwordChangeHandler = A.Fake<IPasswordChangeHandler>();
            var credentialStore = A.Fake<ICredentialStore>();
            A.CallTo(() => credentialStore.ReadCredentials())
                .Returns(new List<ICredential>());

            var sut = new PasswordChanger(passwordChangeHandler, credentialStore);
            sut.ChangePasswordForUsername("username", "new_password");

            A.CallTo(() => passwordChangeHandler.RequestPasswordChange(A<ICredential>._, A<string>._))
                .MustNotHaveHappened();
        }

        [Test]
        public void ChangePassword_RequestsPasswordChange_OnlyForMatchedCredentialInStore()
        {
            var passwordChangeHandler = A.Fake<IPasswordChangeHandler>(x => x.Strict());
            var credentialStore = A.Fake<ICredentialStore>();
            var testCredential = new TestCredential("match");
            A.CallTo(() => credentialStore.ReadCredentials())
                .Returns(new List<ICredential>()
                {
                    testCredential,
                    new TestCredential("not matched")
                });

            var sut = new PasswordChanger(passwordChangeHandler, credentialStore);

            A.CallTo(() => passwordChangeHandler.RequestPasswordChange(testCredential, "new_password")).DoesNothing();

            sut.ChangePasswordForUsername("match", "new_password");

            A.CallTo(() => passwordChangeHandler.RequestPasswordChange(testCredential, "new_password"))
                .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ChangePassword_RequestsPasswordChange_ForAllMatchedCredentialsInStore()
        {
            var passwordChangeHandler = A.Fake<IPasswordChangeHandler>(x => x.Strict());
            var credentialStore = A.Fake<ICredentialStore>();
            var testCredential1 = new TestCredential("match");
            var testCredential2 = new TestCredential("match");
            A.CallTo(() => credentialStore.ReadCredentials())
                .Returns(new List<ICredential>()
                {
                    testCredential1,
                    testCredential2,
                    new TestCredential("not matched")
                });

            var sut = new PasswordChanger(passwordChangeHandler, credentialStore);

            A.CallTo(() => passwordChangeHandler.RequestPasswordChange(testCredential1, "new_password")).DoesNothing();
            A.CallTo(() => passwordChangeHandler.RequestPasswordChange(testCredential2, "new_password")).DoesNothing();

            sut.ChangePasswordForUsername("match", "new_password");

            A.CallTo(() => passwordChangeHandler.RequestPasswordChange(testCredential1, "new_password")).MustHaveHappenedOnceExactly();
            A.CallTo(() => passwordChangeHandler.RequestPasswordChange(testCredential2, "new_password")).MustHaveHappenedOnceExactly();
        }
    }
}
