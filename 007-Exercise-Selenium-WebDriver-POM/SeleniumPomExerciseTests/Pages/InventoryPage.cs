namespace SeleniumWebDriverPOMExercise.Pages
{
    public class InventoryPage : BasePage
    {
        public InventoryPage(IWebDriver driver) : base(driver)
        {
        }

        protected readonly By cartLink = By.XPath("//a[@data-test='shopping-cart-link']");

        protected readonly By productsTitle = By.XPath("//span[@data-test='title']");

        protected readonly By inventoryItems = By.XPath("//div[@data-test='inventory-item']");

        public void AddToCartByIndex(int itemIndex)
        {
            var itemAddToCartButton = By.XPath($"//div[@class='inventory_list']//div[@class='inventory_item'][{itemIndex}]//button");
            Click(itemAddToCartButton);
        }

        public void AddToCartByName(string productName)
        {
            var itemAddToCartButton = By.XPath($"//div[text()='{productName}']" + "/ancestor::div[@class='inventory_item']//button[contains(@class, 'btn_inventory')]");
            Click(itemAddToCartButton);
        }

        public void ClickCartLink()
        {
            Click(cartLink);
            // The cart page reuses the inventory page's data-test names, so wait for it before callers look for cart items.
            wait.Until(d => d.Url.Contains("cart.html"));
        }

        public bool IsInventoryDisplayed()
        {
            return FindElements(inventoryItems).Count > 0;
        }

        public bool IsPageLoaded()
        {
            return GetText(productsTitle) == "Products" && driver.Url.Contains("inventory.html");
        }
    }
}
