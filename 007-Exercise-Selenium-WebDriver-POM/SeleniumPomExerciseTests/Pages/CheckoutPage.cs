namespace SeleniumWebDriverPOMExercise.Pages
{
    public class CheckoutPage : BasePage
    {
        public readonly By firstNameField = By.XPath("//input[@data-test='firstName']");

        public readonly By lastNameField = By.XPath("//input[@data-test='lastName']");

        public readonly By postalCodeField = By.XPath("//input[@data-test='postalCode']");

        public readonly By continueButton = By.XPath("//input[@data-test='continue']");

        public readonly By finishButton = By.XPath("//button[@data-test='finish']");

        public readonly By completeHeader = By.XPath("//h2[@data-test='complete-header']");

        public CheckoutPage(IWebDriver driver) : base(driver)
        {
        }

        public void EnterFirstName(string firstName)
        {
            Type(firstNameField, firstName);
        }

        public void EnterLastName(string lastName)
        {
            Type(lastNameField, lastName);
        }

        public void EnterPostalCode(string postalCode)
        {
            Type(postalCodeField, postalCode);
        }

        public void ClickContinueButton()
        {
            Click(continueButton);
        }

        public void ClickFinishButton()
        {
            Click(finishButton);
        }

        public bool IsPageLoaded()
        {
            return driver.Url.Contains("checkout-step-one.html") ||
            driver.Url.Contains("checkout-step-two.html");
        }

        public bool IsPageComplete()
        {
            return GetText(completeHeader) == "Thank you for your order!";
        }
    }
}
