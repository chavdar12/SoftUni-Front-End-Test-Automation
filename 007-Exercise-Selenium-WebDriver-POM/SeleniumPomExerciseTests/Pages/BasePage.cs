using SeleniumExtras.WaitHelpers;

namespace SeleniumWebDriverPOMExercise.Pages
{
    public class BasePage
    {
        protected readonly IWebDriver driver;

        protected readonly WebDriverWait wait;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        protected IWebElement FindElement(By by)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(by));
        }

        protected ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return driver.FindElements(by);
        }

        // saucedemo renders each page again right after navigating, so an element can go stale between finding and using it; retry when it does.
        protected T WhenStable<T>(Func<T> use)
        {
            var stableWait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            stableWait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            return stableWait.Until(_ => use());
        }

        protected void Type(By by, string text)
        {
            WhenStable(() =>
            {
                var element = FindElement(by);
                element.Clear();
                element.SendKeys(text);
                return true;
            });
        }

        protected void Click(By by)
        {
            WhenStable(() =>
            {
                FindElement(by).Click();
                return true;
            });
        }

        protected string GetText(By by)
        {
            return WhenStable(() => FindElement(by).Text);
        }
    }
}
