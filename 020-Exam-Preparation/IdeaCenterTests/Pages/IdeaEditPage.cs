namespace IdeaCenterTestProject.Pages
{
    public class IdeaEditPage : BasePage
    {
        public IdeaEditPage(IWebDriver driver) :base(driver)
        {
        }

        public readonly By editTitleInput = By.XPath("//input[@name='Title']");

        public readonly By editPictureInput = By.XPath("//input[@name='Url']");

        public readonly By editDescriptionInput = By.XPath("//textarea[@name='Description']");

        public readonly By editButton = By.XPath("//button[@type='submit']");
    }
}
