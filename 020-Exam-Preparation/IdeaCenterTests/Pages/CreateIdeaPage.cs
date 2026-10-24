namespace IdeaCenterTestProject.Pages
{
    public class CreateIdeaPage : BasePage
    {
        public CreateIdeaPage(IWebDriver driver) : base(driver)
        {
        }

        protected static readonly string PageUrl = BaseUrl + "/Ideas/Create";

        // Locating Create Idea Page Elements
        public readonly By titleInput = By.XPath("//input[@name='Title']");

        public readonly By addPicture = By.XPath("//input[@name='Url']");

        public readonly By descriptionInput = By.XPath("//textarea[@name='Description']");

        public readonly By createButton = By.XPath("//button[@class='btn btn-primary btn-lg']");

        public readonly By mainErrorMessage = By.XPath("//div[@class='text-danger validation-summary-errors']/ul/li");

        public readonly By titleErrorMesage = By.XPath("//div[@style='text-align:center']/span[@data-valmsg-for='Title']");

        public readonly By descriptionErrorMessage = By.XPath("//div[@style='text-align:center']/span[@data-valmsg-for='Description']");

        // Methods
        public void OpenPage()
        {
            driver.Navigate().GoToUrl(PageUrl);
        }

        public void CreateIdea(string title, string ImageUrl, string description)
        {
            Type(titleInput, title);
            Type(addPicture, ImageUrl);
            Type(descriptionInput, description);
            Click(createButton);
        }

        public bool IsMainErrorMessageCorrect()
        {
            return GetText(mainErrorMessage) == "Unable to create new Idea!"; 
        }

        public bool IsTitleErrorMessageCorrect()
        {
            return GetText(titleErrorMesage) == "The Title field is required.";
        }

        public bool IsDescriptionErrorMessageCorrect()
        {
            return GetText(descriptionErrorMessage) == "The Description field is required.";
        }
    }
}
