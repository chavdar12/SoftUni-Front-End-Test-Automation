using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Xml.Linq;

namespace DropDownPractice
{
    public class DropDownPractice
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

        [Test]
        public void DropDownPracticeTest()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            string path = System.IO.Directory.GetCurrentDirectory() + "/manufacturer.csv";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            SelectElement manufacturerDropdown = new SelectElement(driver.FindElement(By.XPath("//form[@name='manufacturers']//select")));

            IList<IWebElement> options = manufacturerDropdown.Options;
            List<string> optionsAsString = new List<string>();

            foreach (IWebElement option in options)
            {
                optionsAsString.Add(option.Text);
            }

            optionsAsString.RemoveAt(0);

            foreach (var option in optionsAsString)
            {
                manufacturerDropdown = new SelectElement(driver.FindElement(By.XPath("//form[@name='manufacturers']//select")));

                manufacturerDropdown.SelectByText(option);

                if (driver.PageSource.Contains("There are no products available in this category."))
                {

                    File.AppendAllText(path, $"The manufacturer {option} has no products available.\n");
                }
                else
                {
                    IWebElement productsTable = wait.Until(ExpectedConditions.ElementExists(By.ClassName("productListingHeader")));

                    File.AppendAllText(path, $"\n\n The manufacturer {option} products are listed below-- \n");

                    IReadOnlyCollection<IWebElement> tableRows = productsTable.FindElements(By.XPath(".//tbody/tr"));

                    foreach (var row in tableRows)
                    {
                        File.AppendAllText(path, row.Text + "\n");
                    }
                }
            }
        }
    }
}