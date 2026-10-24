using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumWaits
{
    public class SeleniumWaitsTests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = Browser.Create();
            driver.Navigate().GoToUrl("https://www.selenium.dev/selenium/web/dynamic.html");
            driver.Manage().Window.Maximize();
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        // Creating test without waits
        [Test, Order(1)]
        public void TestWithoutWaits()
        {
            driver.FindElement(By.Id("adder")).Click();

            // The box is added after a delay, so without a wait it is not there yet.
            Assert.Throws<NoSuchElementException>(() => driver.FindElement(By.Id("box0")));
        }

        // Creating test without waits
        [Test, Order(2)]
        public void RevealInputWithoutWaitsFail()
        {
            driver.FindElement(By.Id("reveal")).Click();

            var field = driver.FindElement(By.Id("revealed"));

            // Trying to interact with the field before it is revealed fails.
            Assert.Throws<ElementNotInteractableException>(() => field.SendKeys("Hello"));
        }

        // Creating test with implicit wait
        [Test, Order(3)]
        public void AddBoxWithImplicitWait()
        {
            driver.FindElement(By.XPath("//body//input[@id='adder']")).Click();

            // Using implict wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            var redBox = driver.FindElement(By.XPath("//div[@id='box0']"));

            ClassicAssert.True(redBox.Displayed);
        }

        [Test, Order(4)]
        public void RevealInputWithImplicitWaits()
        {
            // Using implict wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.FindElement(By.Id("reveal")).Click();

            IWebElement revealed = driver.FindElement(By.Id("revealed"));

            Assert.That(revealed.TagName, Is.EqualTo("input"));
        }

        // Ceating test with explicit wait
        [Test, Order(5)]
        public void RevealInputWithExplicitWaits()
        {
            driver.FindElement(By.XPath("//input[@value='Reveal a new input']")).Click();

            // Using explicit wait
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var revealedField = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='revealed']")));

            revealedField.SendKeys("Hello");

            Assert.That(revealedField.GetAttribute("value"), Is.EqualTo("Hello"));
        }

        [Test, Order(6)]
        public void AddBoxWithFluentWaitExpectedConditionsAndIgnoredExceptions()
        {
            driver.FindElement(By.XPath("//input[@id='reveal']")).Click();

            // Using fluent wait
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);

            var revealedField = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='revealed']")));

            ClassicAssert.IsTrue(revealedField.Displayed);
        }

        [Test, Order(7)]
        public void RevealInputWithCustomFluentWait()
        {
            driver.FindElement(By.XPath("//input[@id='reveal']")).Click();

            //using custom fluent wait
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);

            fluentWait.Timeout = TimeSpan.FromSeconds(10);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(500);

            var revealedField = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='revealed']")));

            ClassicAssert.IsTrue(revealedField.Displayed);
        }
    }
}