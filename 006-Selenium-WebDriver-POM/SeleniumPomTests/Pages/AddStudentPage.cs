namespace SeleniumWebDriverPOMLab.Pages
{
    public class AddStudentPage : BasePage
    {
        private const string ErrorMsgXPath = "//div[@style='background:red;color:white; margin:20px 10px 10px 10px;padding:10px']";

        public AddStudentPage(IWebDriver driver) : base(driver)
        {
        }
        public override string PageUrl => "http://localhost:8080/add-student";

        public IWebElement ElementErrorMsg => driver.FindElement(By.XPath(ErrorMsgXPath));

        public IWebElement FieldStudentName => driver.FindElement(By.XPath("//input[@id='name']"));

        public IWebElement FieldStudentEmail => driver.FindElement(By.XPath("//input[@id='email']"));

        public IWebElement AddButton => driver.FindElement(By.XPath("//button[@type='submit']"));

        public void AddStudent(string name, string email)
        {
            this.FieldStudentName.SendKeys(name);
            this.FieldStudentEmail.SendKeys(email);
            var addButton = this.AddButton;
            addButton.Click();
            // The form posts and reloads the page; wait for that before callers read the URL or the new page.
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(addButton));
        }

        public string GetErrorMsg()
        {
            return ElementErrorMsg.Text;
        }

        public void WaitForErrorMessageToBeVisible()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(ErrorMsgXPath)));
        }
    }
}