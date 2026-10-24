using SeleniumWebDriverPOMExercise.Pages;

namespace SeleniumWebDriverPOMExercise.Tests
{
    public class LoginTests : BaseTest
    {
        [Test, Order(1)]
        public void TestLoginWithValidCredentials()
        {
            Login("standard_user", "secret_sauce");

            ClassicAssert.IsTrue(inventoryPage.IsPageLoaded(), "The login is not successfully after login with valid credentials.");
        }

        [Test, Order(2)]
        public void TestLoginWithInvalidCredentials()
        {
            Login("invalid_user123", "secret_sauce");

            Assert.That(loginPage.GetErrorMessage().Contains("Username and password do not match any user in this service"), "The error message was not as expected after login with invalid credentials.");
        }

        [Test, Order(3)]
        public void TestLoginWithLockedOutUser()
        {
            Login("locked_out_user", "secret_sauce");

            Assert.That(loginPage.GetErrorMessage().Contains("Sorry, this user has been locked out."), "The error message was not as expected after login with locked user.");
        }
    }
}
