namespace IdeaCenterTestProject.Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;

        protected BasePage basePage;

        protected LoginPage loginPage;

        protected CreateIdeaPage createIdeaPage;

        protected MyIdeasPage myIdeasPage;

        protected IdeasReadPage ideasReadPage;

        protected IdeaEditPage ideaEditPage;

        [OneTimeSetUp]
        public void OneTimeSetUp() 
        {
            Browser.RequireReachable(LoginPage.PageUrl);

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOptions.AddArgument("--disable-search-engine-choice-screen");

            driver = Browser.Create(chromeOptions);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            basePage = new BasePage(driver);
            loginPage = new LoginPage(driver);
            createIdeaPage = new CreateIdeaPage(driver);
            myIdeasPage = new MyIdeasPage(driver);
            ideasReadPage = new IdeasReadPage(driver);
            ideaEditPage = new IdeaEditPage(driver);

            // Login to the application
            loginPage.OpenPage();
            loginPage.Login("emailtest@example.com", "123456");
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
