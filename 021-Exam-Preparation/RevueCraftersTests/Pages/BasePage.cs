namespace RevueCraftersTestProject.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;

        protected WebDriverWait wait;

        public static readonly string BaseUrl = "https://d3s5nxhwblsjbi.cloudfront.net";

        // Locating Page Elements
        protected readonly By homeLink = By.XPath("//a[@class='nav-link active']");

        protected readonly By aboutLink = By.XPath("///li[@class='nav-item']/a[@class='nav-link active']");

        protected readonly By servicesLink = By.XPath("//a[@href='/#servicePage']");

        protected readonly By registerLink = By.XPath("//a[@href='/#registerForm']");

        protected readonly By loginLink = By.XPath("//li/a[@href='/Users/Login#loginForm']");

        protected readonly By myRevuesLink = By.XPath("//a[@class='nav-link active']");

        protected readonly By createRevueLink = By.XPath("/Revue/Create#createRevue");

        protected readonly By profileLink = By.XPath("//a[@href='/Profile#profilePage']");

        protected readonly By logoutLink = By.XPath("//a[@href='/Users/Logout']");

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Methods
        public IWebElement FindElement(By by)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(by));
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return driver.FindElements(by);
        }

        public void Type(By by, string text)
        {
            var element = FindElement(by);
            element.Clear();
            element.SendKeys(text);
        }

        public void Click(By by)
        {
            FindElement(by).Click();
        }

        public string GetText(By by)
        {
            return FindElement(by).Text.Trim();
        }
    }
}
