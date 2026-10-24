using Appium.Window;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace Appium.Tests
{
    public class AppiumSummatorTests
    {
        // The Summator desktop app: https://github.com/nakov/WindowsFormsApp-Summator/releases/download/v.1.0/WindowsFormsApp.exe
        private static readonly string AppPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "WindowsFormsApp.exe");

        private WindowsDriver driver;

        [SetUp]
        public void OpenApp()
        {
            AppiumServer.RequireRunning();

            var options = new AppiumOptions { PlatformName = "Windows", AutomationName = "Windows", App = AppPath };
            driver = new WindowsDriver(AppiumServer.Url, options);
        }

        [TearDown]
        public void ShutDownApp()
        {
            driver?.Quit();
        }

        [TestCase("5", "15", "20")]
        [TestCase("-5", "-15", "-20")]
        [TestCase("5", "error", "error")]
        public void Test_Data_Driven(string f1, string f2, string res)
        {
            var window = new SummatorWindow(driver);
            string result = window.Calculate(f1, f2);
            Assert.That(result, Is.EqualTo(res));
        }
    }
}
