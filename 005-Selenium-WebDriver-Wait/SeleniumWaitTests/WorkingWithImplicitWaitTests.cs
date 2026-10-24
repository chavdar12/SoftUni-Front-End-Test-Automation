using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumWaitsExercise
{
    public class WorkingWithImplicitWaitTests
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
        public void SeachProductWithImplicitWait()
        {
            driver.FindElement(By.XPath("//form[@name='quick_find']//input[@name='keywords']")).SendKeys("keyboard");

            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            try
            {

                var productsTableHeader = driver.FindElement(By.XPath("//a[@title='Sort products descendingly by Product Name']"));

                Assert.That(productsTableHeader.Text.Trim(), Is.EqualTo("Product Name+"), "Products with keyword 'keyboard' are no found");
                ClassicAssert.IsTrue(driver.PageSource.Contains("Displaying"), "Products with keyword 'keyboard' are no found");

                driver.FindElement(By.LinkText("Buy Now")).Click();

                var checkoutButton = driver.FindElement(By.LinkText("Checkout"));

                ClassicAssert.IsTrue(checkoutButton.Displayed);
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception: " + ex.Message);
            }
        }

        [Test, Order(2)]
        public void SearchProductWithImplicitWait2()
        {
            driver.FindElement(By.XPath("//form[@name='quick_find']//input[@name='keywords']")).SendKeys("junk");

            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            try
            {
                driver.FindElement(By.LinkText("Buy Now")).Click();
            }
            catch (NoSuchElementException ex)
            {
                Assert.Pass("Expected 'NoSuchElementException' was thrown.");
                Console.WriteLine("Timeout - " + ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception: " + ex.Message);
            }
        }
    }
}
