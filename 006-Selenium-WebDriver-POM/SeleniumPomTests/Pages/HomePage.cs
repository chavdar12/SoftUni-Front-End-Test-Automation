namespace SeleniumWebDriverPOMLab.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public override string PageUrl => "http://localhost:8080/";

        public IWebElement ElementStudentsCount => driver.FindElement(By.XPath("//p//b"));

        public string GetStudentsCount()
        {
            return ElementStudentsCount.Text;
        }
    }
}
