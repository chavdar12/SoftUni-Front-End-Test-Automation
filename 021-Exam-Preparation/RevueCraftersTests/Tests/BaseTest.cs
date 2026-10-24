using OpenQA.Selenium.Interactions;
using RevueCraftersTestProject.Pages;

namespace RevueCraftersTestProject.Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;

        protected Actions actions;

        protected BasePage basePage;

        protected LoginPage loginPage;

        protected CreateRevuePage createRevuePage;

        protected MyRevuesPage myRevuesPage;

        protected EditRevuePage editRevuePage;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Browser.RequireReachable(BasePage.BaseUrl);

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOptions.AddArgument("--disable-search-engine-choice-screen");

            driver = Browser.Create(chromeOptions);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            actions = new Actions(driver);
            basePage = new BasePage(driver);
            loginPage = new LoginPage(driver);
            createRevuePage = new CreateRevuePage(driver);
            myRevuesPage = new MyRevuesPage(driver);
            editRevuePage = new EditRevuePage(driver);

            // Performin Login
            loginPage.OpenPage();
            loginPage.Login("userfortesting@example.com", "123456");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }

        public string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
