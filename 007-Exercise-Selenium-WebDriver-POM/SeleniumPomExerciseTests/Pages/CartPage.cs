namespace SeleniumWebDriverPOMExercise.Pages
{
    public class CartPage : BasePage
    {
        protected readonly By cartItem = By.XPath("//div[@data-test='inventory-item']");

        protected readonly By checkoutButton = By.XPath("//button[@id='checkout']");

        public CartPage(IWebDriver driver) : base(driver)
        {
        }

        public bool IsCartItemDisplayed()
        {
            return WhenStable(() => FindElement(cartItem).Displayed);
        }

        public void ClickCheckout()
        {
            Click(checkoutButton);
        }
    }
}
