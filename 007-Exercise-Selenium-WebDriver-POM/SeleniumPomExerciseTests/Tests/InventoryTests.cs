namespace SeleniumWebDriverPOMExercise.Tests
{
    public class InventoryTests : BaseTest
    {
        [SetUp]
        public void InventoryTestsSetUp()
        {
            Login("standard_user", "secret_sauce");
        }

        [Test, Order(1)]
        public void TestInventoryDisplay()
        {
            ClassicAssert.IsTrue(inventoryPage.IsInventoryDisplayed(), "The items are not displayed as expected.");

            ClassicAssert.IsTrue(inventoryPage.IsPageLoaded(), "The inventory page is not loaded as expected.");
        }

        [Test, Order(2)]
        public void TestAddToCartByIndex()
        {
            inventoryPage.AddToCartByIndex(1);

            inventoryPage.ClickCartLink();

            Assert.That(driver.PageSource.Contains("Sauce Labs Backpack"), "The page does not contains the title of the added item");

            ClassicAssert.IsTrue(cartPage.IsCartItemDisplayed(), "The cart items section is empty after adding item by index");
        }

        [Test, Order(3)]
        public void TestAddToCartByName()
        {
            inventoryPage.AddToCartByName("Sauce Labs Bike Light");

            inventoryPage.ClickCartLink();

            Assert.That(driver.PageSource.Contains("Sauce Labs Bike Light"), "The page does not contains the title of the added item.");

            ClassicAssert.IsTrue(cartPage.IsCartItemDisplayed(), "The cart items section is empty after adding item by name.");
        }

        [Test, Order(4)]
        public void TestInventoryPageTitle()
        {
            Assert.That(inventoryPage.IsPageLoaded(), Is.True, "Inventory page is not loaded as expected.");
        }
    }
}
