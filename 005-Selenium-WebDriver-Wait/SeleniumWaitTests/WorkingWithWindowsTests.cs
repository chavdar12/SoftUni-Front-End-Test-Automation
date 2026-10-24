using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace SeleniumWaitsExercise
{
    public class WorkingWithWindowsTests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = Browser.Create();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/windows");
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
        public void HandleMultipleWindows()
        {
            driver.FindElement(By.XPath("//div[@class='example']//a[@href='/windows/new']")).Click();

            // Get main window
            string mainWindowHandle = driver.CurrentWindowHandle;

            // Get all window handles
            ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

            // Ensure there are at least two windows opened
            Assert.That(windowHandles.Count, Is.EqualTo(2), "There should be two windows opened.");

            // Switch to the new window
            driver.SwitchTo().Window(windowHandles[1]);

            // Verifying the content of the new window
            var newPageTitle = driver.Title;
            Assert.That(newPageTitle, Is.EqualTo("New Window"));

            string newWindowContent = driver.PageSource;
            ClassicAssert.IsTrue(newWindowContent.Contains("New Window"), "The content of the new window is not as expected.");

            driver.Close();

            // Switch back to the main window
            driver.SwitchTo().Window(mainWindowHandle);

            // Verifying the main window content
            IWebElement pageTextH3 = driver.FindElement(By.XPath("//div[@class='example']//h3"));
            Assert.That(pageTextH3.Text, Is.EqualTo("Opening a new window"));
        }

        [Test, Order(2)]
        public void Handle_NoSuchElementException()
        {
            driver.FindElement(By.LinkText("Click Here")).Click();

            // Get main window
            string mainWindowHandle = driver.CurrentWindowHandle;

            // Get all windows handles
            ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

            // Switch to the new window
            driver.SwitchTo().Window(windowHandles[1]);

            // Close the new window
            driver.Close();

            try
            {
                // Attempt to swich back to the closed (new) window
                driver.SwitchTo().Window(windowHandles[1]);
            }
            catch (NoSuchWindowException ex)
            {
                Assert.Pass($"'NoSuchWindowException' was correctly handled with message: {ex.Message}");
            }
            finally
            {
                // Switch to the main window
                driver.SwitchTo().Window(mainWindowHandle);
            }
        }
    }
}
