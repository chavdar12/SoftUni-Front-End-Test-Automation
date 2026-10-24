using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace FirstLabWikipediaUITesting
{
    public class WaitingTest
    {
        public IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);

            driver = Browser.Create(chromeOptions);

            driver.Manage().Window.Maximize();
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }


        [Test]
        public void ImplicitWaitExampleTest()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(16);

            driver.Navigate().GoToUrl("http://www.uitestingplayground.com/ajax");

            driver.FindElement(By.XPath("//button[@class='btn btn-primary']")).Click();

            var sucessMessage = driver.FindElement(By.XPath("//div[@id='content']//p"));

            Assert.That(sucessMessage.Text, Is.EqualTo("Data loaded with AJAX get request."));
        }

        [Test]
        public void ExplicitWaitExampleTest()
        {
            driver.Navigate().GoToUrl("http://www.uitestingplayground.com/ajax");

            driver.FindElement(By.XPath("//button[@class='btn btn-primary']")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            var sucessMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@id='content']//p")));

            Assert.That(sucessMessage.Text, Is.EqualTo("Data loaded with AJAX get request."));
        }
    }
}
