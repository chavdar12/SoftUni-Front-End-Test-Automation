namespace SeleniumWebDriverPOMExercise.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        protected readonly By usernameField = By.XPath("//input[@data-test='username']");

        protected readonly By passwordField = By.XPath("//input[@data-test='password']");

        protected readonly By loginButton = By.XPath("//input[@data-test='login-button']");

        protected readonly By errorMessage = By.XPath("//h3[@data-test='error']");

        protected readonly By errorMessageCloseBtn = By.XPath("//button[@data-test='error-button']");

        public void InputUsername(string username)
        {
            Type(usernameField, username);
        }

        public void InputPassword(string password)
        {
            Type(passwordField, password);
        }

        public void ClickLoginButton()
        {
            Click(loginButton);
        }

        public string GetErrorMessage()
        {
            return GetText(errorMessage);
        }
    }
}
