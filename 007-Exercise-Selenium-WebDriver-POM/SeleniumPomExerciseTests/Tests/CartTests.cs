namespace SeleniumWebDriverPOMExercise.Tests
{
    public class CartTests : BaseTest
    {
        [SetUp]
        public void CartTestsSetUp()
        {
            Login("standard_user", "secret_sauce");

            inventoryPage.AddToCartByName("Sauce Labs Bike Light");

            inventoryPage.ClickCartLink();

        }

        [Test, Order(1)]
        public void TestCartItemDisplayed()
        {
            ClassicAssert.IsTrue(cartPage.IsCartItemDisplayed(), "The added item is not displayed in the cart as expected.");
        }

        [Test, Order(2)]
        public void TestClickCheckout()
        {
            cartPage.ClickCheckout();

            ClassicAssert.IsTrue(driver.Url.Contains("checkout-step-one.html"), "The checkout page is not loaded as expected.");
        }
    }
}
