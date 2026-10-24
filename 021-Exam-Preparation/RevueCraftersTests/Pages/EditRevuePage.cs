namespace RevueCraftersTestProject.Pages
{
    public class EditRevuePage : BasePage
    {
        public EditRevuePage(IWebDriver driver) :base(driver)
        {
        }

        protected readonly By editRevueCard = By.XPath("//div[@class='card-body p-md-5']");
        public IWebElement EditRevueCardElement => FindElement(editRevueCard);

        public readonly By editTitleInput = By.XPath("//input[@name='Title']");

        public readonly By editPictureInput = By.XPath("//input[@name='Url']");

        public readonly By editDescriptionInput = By.XPath("//textarea[@name='Description']");

        public readonly By editButton = By.XPath("//button[@type='submit']");
    }
}
