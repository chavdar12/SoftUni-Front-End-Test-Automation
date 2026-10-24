namespace SeleniumWebDriverPOMLab.Pages
{
    public class BasePage
    {
        protected readonly IWebDriver driver;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }

        public virtual string PageUrl { get; }

        public IWebElement LinkHomePage => driver.FindElement(By.LinkText("Home"));

        public IWebElement LinkViewStudentsPage => driver.FindElement(By.LinkText("View Students"));

        public IWebElement LinkAddStudentsPage => driver.FindElement(By.LinkText("Add Student"));

        public IWebElement ElementTextHeading => driver.FindElement(By.XPath("//body//h1"));

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(this.PageUrl);
        }

        public bool IsOpen()
        {
            return driver.Url == this.PageUrl;
        }

        public string GetPageTitle()
        {
            return driver.Title;
        }

        public string GetPageHeadingText()
        {
            return ElementTextHeading.Text;
        }

        public string GenerateRandomName()
        {
            var names = new[] { "John Doe", "Jane Smith", "Alice Cooper", "Bob Down", "Charlie Johnson", "Billy Smith" };
            Random random = new Random();
            return names[random.Next(names.Length)];
        }

        public string GenerateRandomEmail()
        {
            Random random = new Random();
            var randomNumber = random.Next(1, 9999);
            var testRandomEmail = $"testemail{randomNumber}@example.com";
            return testRandomEmail;
        }
    }
}
