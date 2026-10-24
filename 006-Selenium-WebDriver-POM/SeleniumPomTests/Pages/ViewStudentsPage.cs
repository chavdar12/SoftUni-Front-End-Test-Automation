namespace SeleniumWebDriverPOMLab.Pages
{
    public class ViewStudentsPage : BasePage
    {
        public ViewStudentsPage(IWebDriver driver) : base(driver)
        {
        }

        public override string PageUrl => "http://localhost:8080/students";

        public ReadOnlyCollection<IWebElement> ListItemStudents => driver.FindElements(By.XPath("//body//ul//li"));

        public string[] GetStudentsList()
        {
            var elementsStudents = this.ListItemStudents.Select(s => s.Text).ToArray();
            return elementsStudents;
        }
    }
}
