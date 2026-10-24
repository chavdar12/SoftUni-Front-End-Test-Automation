using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace Appium.Window
{
    public class SummatorWindow
    {
        private readonly WindowsDriver driver;

        public SummatorWindow(WindowsDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement num1 => driver.FindElement(MobileBy.AccessibilityId("textBoxFirstNum"));
        public IWebElement num2 => driver.FindElement(MobileBy.AccessibilityId("textBoxSecondNum"));
        public IWebElement button => driver.FindElement(MobileBy.AccessibilityId("buttonCalc"));
        public IWebElement result => driver.FindElement(MobileBy.AccessibilityId("textBoxSum"));

        public string Calculate(string f1, string f2)
        {
            num1.Click();
            num1.SendKeys(f1);
            num2.Click();
            num2.SendKeys(f2);
            button.Click();
            return result.Text;
        }
    }
}
