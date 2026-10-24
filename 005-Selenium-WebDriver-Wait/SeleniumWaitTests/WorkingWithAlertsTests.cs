using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumWaitsExercise
{
    public class WorkingWithAlertsTests
    {
        IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = Browser.Create();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test, Order(1)]
        public void HandleBasicAlert()
        {
            // Click the Alert button
            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Alert')]")).Click();

            // Switch to the alert window
            IAlert alert = driver.SwitchTo().Alert();

            // Verify the alert text
            Assert.That(alert.Text, Is.EqualTo("I am a JS Alert"), "The Basic Alert text is not as expected.");

            // Accept the alert
            alert.Accept();

            IWebElement resultElement = driver.FindElement(By.Id("result"));

            Assert.That(resultElement.Text, Is.EqualTo("You successfully clicked an alert"), "Result message was not as expected.");
        }

        [Test, Order(2)]
        public void HandleConfirmAlert()
        {
            // Handling alert with Ok
            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Confirm')]")).Click();

            IAlert alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "The Confirm Alert text is not as expected.");

            alert.Accept();

            IWebElement resultElementOk = driver.FindElement(By.Id("result"));
            Assert.That(resultElementOk.Text, Is.EqualTo("You clicked: Ok"), "The result message after clicking 'Ok' was not as expected");

            // Handling alert with Cancel
            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Confirm')]")).Click();

            alert = driver.SwitchTo().Alert();

            alert.Dismiss();

            IWebElement resultElementCancel = driver.FindElement(By.Id("result"));
            Assert.That(resultElementCancel.Text, Is.EqualTo("You clicked: Cancel"), "The result message after clicking 'Cancel' was not as expected");
        }

        [Test, Order(3)]
        public void HandlePromptAlert()
        {
            // Handling alert with Ok
            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Prompt')]")).Click();

            IAlert alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"), "The Prompt Alert text is not as expected.");

            string inputText = "alabala";
            alert.SendKeys(inputText);
            alert.Accept();

            IWebElement resultElementOk = driver.FindElement(By.Id("result"));
            Assert.That(resultElementOk.Text, Is.EqualTo($"You entered: {inputText}"), "The result message after clicking 'Ok' was not as expected");

            // Handling alert with Cancel
            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Prompt')]")).Click();

            alert = driver.SwitchTo().Alert();

            alert.SendKeys(inputText);
            alert.Dismiss();

            IWebElement resultElementCancel = driver.FindElement(By.Id("result"));
            Assert.That(resultElementCancel.Text, Is.EqualTo("You entered: null"), "The result message after clicking 'Cancel' was not as expected");
            Assert.That(resultElementCancel.Text, Does.Not.Contain(inputText), $"The result message after clicking 'Cancel' should not contain {inputText}");
        }
    }
}
