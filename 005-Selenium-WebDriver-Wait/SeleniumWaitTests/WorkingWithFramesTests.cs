using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumWaitsExercise
{
    public class WorkingWithFramesTests
    {
        IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            // CodePen sits behind a bot check that rejects automated clients; skip instead of failing when it blocks us.
            Browser.RequireReachable("https://codepen.io/pervillalva/full/abPoNLd");

            driver = Browser.Create();
            driver.Navigate().GoToUrl("https://codepen.io/pervillalva/full/abPoNLd");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }

        [Test, Order(1)]
        public void HandlingTestFrameByIndex()
        {
            // Wait until the iFrame is available and switch to it by finding the first iFrame
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.TagName("iframe")));

            var dropDownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='dropbtn']")));
            dropDownButton.Click();

            var dropDownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='dropdown-content']/a")));
            Assert.That(dropDownLinks.Count, Is.EqualTo(3), "The count of the elements in the dropdown is not as expected.");

            foreach (var link in dropDownLinks)
            {
                Console.WriteLine(link.Text);
                ClassicAssert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected.");
            }

            driver.SwitchTo().DefaultContent();
        }

        [Test, Order(2)]
        public void HandlingTestFrameById()
        {
            // Wait until the iFrame is available and switch to it by ID
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt("result"));

            var dropDownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='dropbtn']")));
            dropDownButton.Click();

            var dropDownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='dropdown-content']/a")));
            Assert.That(dropDownLinks.Count, Is.EqualTo(3), "The count of the elements in the dropdown is not as expected.");

            foreach (var link in dropDownLinks)
            {
                Console.WriteLine(link.Text);
                ClassicAssert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected.");
            }

            driver.SwitchTo().DefaultContent();
        }

        [Test, Order(3)]
        public void HandlingTestFrameByWebElement()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var frameElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//iframe[@id='result']")));

            driver.SwitchTo().Frame(frameElement);

            var dropDownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='dropbtn']")));
            dropDownButton.Click();

            var dropDownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='dropdown-content']/a")));
            Assert.That(dropDownLinks.Count, Is.EqualTo(3), "The count of the elements in the dropdown is not as expected.");

            foreach (var link in dropDownLinks)
            {
                Console.WriteLine(link.Text);
                ClassicAssert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected.");
            }

            driver.SwitchTo().DefaultContent();
        }
    }
}
