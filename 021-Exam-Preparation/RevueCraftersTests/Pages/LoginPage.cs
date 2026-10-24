namespace RevueCraftersTestProject.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) :base(driver)
        {
        }

        public readonly string PageUrl = BaseUrl + "/Users/Login#loginForm";

        // Locating Page Elements
        protected readonly By emailInput = By.XPath("//input[@type='email']");

        protected readonly By passwordInput = By.XPath("//input[@type='password']");

        protected readonly By logInButton = By.XPath("//button[@type='submit']");

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(PageUrl);
        }

        public void Login(string email, string password)
        {
            Type(emailInput, email);
            Type(passwordInput, password);
            Click(logInButton);
        }
    }
}
