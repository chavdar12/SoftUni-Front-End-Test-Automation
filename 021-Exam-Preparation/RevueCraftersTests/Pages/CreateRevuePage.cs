namespace RevueCraftersTestProject.Pages
{
    public class CreateRevuePage : BasePage
    {
        public CreateRevuePage(IWebDriver driver) :base(driver)
        {
        }

        public readonly string PageUrl = BaseUrl + "/Revue/Create#createRevue";

        // Locating Page Elements
        protected readonly By createRevueCard = By.XPath("//form[@class='mx-1 mx-md-4']");
        public IWebElement CreateRevueCardElement => FindElement(createRevueCard);

        protected readonly By titleInput = By.XPath("//input[@name='Title']");

        protected readonly By pictureInput = By.XPath("//input[@name='Url']");

        protected readonly By descriptionInput = By.XPath("//textarea[@name='Description']");

        protected readonly By createButton = By.XPath("//button[@type='submit']");

        protected readonly By mainErrorMessage = By.XPath("//div[@style='text-align: center']/ul/li");

        protected readonly By titleErrorMessage = By.XPath("//span[@data-valmsg-for='Title']");

        protected readonly By descriptionErrorMessage = By.XPath("//span[@data-valmsg-for='Description']");

        // Methods
        public void OpenPage()
        {
            driver.Navigate().GoToUrl(PageUrl);
        }

        public void CreateNewRevue(string title, string pictureUrl, string description)
        {
            Type(titleInput, title);
            Type(pictureInput, pictureUrl);
            Type(descriptionInput, description);
            Click(createButton);
        }

        public bool IsMainErroMessageCorrect()
        {
            return GetText(mainErrorMessage) == "Unable to create new Revue!";
        }

        public bool IsTitleErroMessageCorrect()
        {
            return GetText(titleErrorMessage) == "The Title field is required.";
        }

        public bool IsDescriptionErroMessageCorrect()
        {
            return GetText(descriptionErrorMessage) == "The Description field is required.";
        }
    }
}
