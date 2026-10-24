using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWaitsExercise
{
    public class WorkingWithExplicitWaitTests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = Browser.Create();
            driver.Navigate().GoToUrl("https://practice.bpbonline.com/");
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
        public void SearchProductWithExplicitWait()
        {
            driver.FindElement(By.XPath("//form[@name='quick_find']//input[@name='keywords']")).SendKeys("keyboard");

            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotInteractableException));

                var buyNowLink = wait.Until(d => d.FindElement(By.LinkText("Buy Now")));

                // Reset the implicit wait - back to 10 sec
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                buyNowLink.Click();

                // Verifying [Checkout] button
                var checkoutButton = driver.FindElement(By.LinkText("Checkout"));

                ClassicAssert.IsTrue(checkoutButton.Displayed, "The [Checkout] button is not displayed on the page");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Unexpected exception: {ex.Message}");
            }
        }

        [Test, Order(2)]
        public void SeachProduct_Junk_ShouldThrowNoSuchElementException()
        {
            driver.FindElement(By.XPath("//form[@name='quick_find']//input[@name='keywords']")).SendKeys("junk");

            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            try
            {
                // Create WebDriverWait obj with  timeout set to 10 seconds
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Wait to identify the [Buy Now] button using the LinkText property
                IWebElement buyNowLink = wait.Until(e => e.FindElement(By.LinkText("Buy Now")));

                // if found, fail the test as it shoud not exists
                buyNowLink.Click();
                Assert.Fail("The [Buy Now] button was found for a non-existing product.");
            }
            catch (WebDriverTimeoutException)
            {
                // Expected exception for non-existing product
                Assert.Pass("Expected WebDriverTimeoutException was thrown.");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception:" + ex.Message);
            }
            finally
            {
                // Reset the implicit wait
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
        }
    }
}
