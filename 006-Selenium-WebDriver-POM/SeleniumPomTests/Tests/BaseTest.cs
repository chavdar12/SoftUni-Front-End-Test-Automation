namespace SeleniumWebDriverPOMLab.Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Start the Student Registry app first: clone nakov/MVC-app-integration-tests-example-mocha, then npm install && npm start (port 8080).
            Browser.RequireReachable("http://localhost:8080/");
            driver = Browser.Create();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}
