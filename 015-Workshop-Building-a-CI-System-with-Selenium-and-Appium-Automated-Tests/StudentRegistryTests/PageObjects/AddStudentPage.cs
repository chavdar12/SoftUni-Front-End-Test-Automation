using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Students_Registry_Automated_Tests.PageObjects
{
    public class AddStudentPage : BasePage
    {
        public AddStudentPage(IWebDriver driver) : base(driver)
        {
        }

        public override string PageUrl =>
            "http://localhost:8080/add-student";

        public IWebElement FieldName =>
            driver.FindElement(By.CssSelector("input#name"));

        public IWebElement FieldEmail =>
            driver.FindElement(By.CssSelector("input#email"));

        public IWebElement ButtonSubmit =>
            driver.FindElement(By.CssSelector("button[type='submit']"));

        public IWebElement ElementErrorMsg =>
            driver.FindElement(By.XPath("//div[contains(@style,'background:red')]"));

        public void AddStudent(string name, string email)
        {
            this.FieldName.SendKeys(name);
            this.FieldEmail.SendKeys(email);
            var buttonSubmit = this.ButtonSubmit;
            buttonSubmit.Click();
            // The form posts and reloads the page; wait for that before callers read the URL or the new page.
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(buttonSubmit));
        }
    }
}
