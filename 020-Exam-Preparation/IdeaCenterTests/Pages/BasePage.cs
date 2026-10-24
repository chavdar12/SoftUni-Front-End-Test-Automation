namespace IdeaCenterTestProject.Pages
{
    public class BasePage
    {
        protected readonly IWebDriver driver;

        protected readonly WebDriverWait wait;

        protected static readonly string BaseUrl = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:83";

        // Locating Base Page Elements
        public readonly By loginButton = By.XPath("//a[@class='btn btn-outline-info px-3 me-2']");

        public readonly By signUpButton = By.XPath("//a[@class='btn btn-primary me-3']");

        public readonly By homeLink = By.XPath("//img[@class='rounded-circle']");

        public readonly By myProfileLink = By.XPath("//a[text()='My Profile']");

        public readonly By myIdeasLink = By.XPath("//a[text()='My Ideas']");

        public readonly By createIdeaLink = By.XPath("//a[text()='Create Idea']");

        public readonly By logOutButton = By.XPath("//a[@href='/Users/Logout']");


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
