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
        public void ChangePassword_WhenCredentialStoreHasNullTarget_ShouldNotChangePassword()
        {
            var credentialStore = A.Fake<ICredentialStore>();
            var testCredential = new TestCredential(null);
            A.CallTo(() => credentialStore.ReadCredentials())
                .Returns(new List<ICredential>
                {
                    testCredential
                });

            var sut = new PasswordChanger(credentialStore);
            sut.ChangePasswordForUsername("username", "new_password");

            Assert.True(testCredential.PasswordWasNotChanged());
        }
        [Test]
        public void ChangePassword_ForUsernameMatchingStoredCredential_ShouldRequestsPasswordChangeForMatchingCredential()
        {
            var credentialStore = A.Fake<ICredentialStore>();
            var testCredential = new TestCredential("username");
            A.CallTo(() => credentialStore.ReadCredentials())
                .Returns(new List<ICredential>
                {
                    testCredential
                });

            var sut = new PasswordChanger(credentialStore);
            sut.ChangePasswordForUsername("username", "new_password");

            Assert.True(testCredential.PasswordWasChangedTo("new_password"));
        }

        [Test]
        public void ChangePassword_WithCaseInsensitiveMatch_ShouldChangePassword()
        {
            var credentialStore = A.Fake<ICredentialStore>();
            var testCredential = new TestCredential("USERNAME");
            A.CallTo(() => credentialStore.ReadCredentials())
                .Returns(new List<ICredential>
                {
                    testCredential
                });

            var sut = new PasswordChanger(credentialStore);
            sut.ChangePasswordForUsername("username", "new_password");

            Assert.True(testCredential.PasswordWasChangedTo("new_password"));
        }

        [Test]
        public void ChangePassword_RequestsPasswordChange_OnlyForMatchedCredentialInStore()
        {
            var credentialStore = A.Fake<ICredentialStore>();

            var matchedCredential = new TestCredential("match");
            var unmatchedCredentials = new TestCredential("not matched");

            A.CallTo(() => credentialStore.ReadCredentials())
                .Returns(new List<ICredential>()
                {
                    matchedCredential,
                    unmatchedCredentials
                });

            var sut = new PasswordChanger(credentialStore);

            sut.ChangePasswordForUsername("match", "new_password");

            Assert.True(matchedCredential.PasswordWasChangedTo("new_password"));
            Assert.True(unmatchedCredentials.PasswordWasNotChanged());
        }

        [Test]
        public void ChangePassword_RequestsPasswordChange_ForAllMatchedCredentialsInStore()
        {
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

            var sut = new PasswordChanger(credentialStore);

            sut.ChangePasswordForUsername("match", "new_password");

            Assert.True(testCredential1.PasswordWasChangedTo("new_password"));
            Assert.True(testCredential2.PasswordWasChangedTo("new_password"));
        }
    }
}
