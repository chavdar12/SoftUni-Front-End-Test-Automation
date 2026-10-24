using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Students_Registry_Automated_Tests.Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            // Start the Student Registry app first: clone nakov/MVC-app-integration-tests-example-mocha, then npm install && npm start (port 8080).
            Browser.RequireReachable("http://localhost:8080/");
            driver = Browser.Create();
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver?.Quit();
        }
    }
}
