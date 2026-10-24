using OpenQA.Selenium.Interactions;

namespace RevueCraftersTestProject.Pages
{
    public class MyRevuesPage : BasePage
    {
        public MyRevuesPage(IWebDriver driver) : base(driver)
        {
        }

        public readonly string PageUrl = BaseUrl + "/Revue/MyRevues#myRevues";

        // Locating Page Elements
        public readonly By revuesCards = By.XPath("//div[@class='card mb-4 box-shadow']");

        public ReadOnlyCollection<IWebElement> RevuesCards => FindElements(revuesCards);

        public IWebElement viewButtonLastRevue => RevuesCards.Last().FindElement(By.XPath(".//a[text()='View']"));
        public IWebElement editButtonLastRevue => RevuesCards.Last().FindElement(By.XPath(".//a[contains(@href, '/Revue/Edit')]"));
        public IWebElement deleteButtonLastRevue => RevuesCards.Last().FindElement(By.XPath(".//a[contains(@href, '/Revue/Delete')]"));
        public IWebElement titleLastRevue => RevuesCards.Last().FindElement(By.XPath(".//div[@class='text-muted text-center']"));
        public IWebElement descriptionLastRevue => RevuesCards.Last().FindElement(By.XPath(".//p[@class='card-text']"));

        protected readonly By searchBar = By.XPath("//input[@type='search']");

        public IWebElement SearchBarElement => FindElement(searchBar);

        protected readonly By searchButton = By.XPath("//button[@id='search-button']");

        public IWebElement SearchButtonElement => FindElement(searchButton);

        protected readonly By searchedElementTitle = By.XPath("//div[@class='text-muted text-center']");

        public IWebElement SearchedElementTitle => FindElement(searchedElementTitle);

        protected readonly By allRevuesSection = By.XPath("//div[@id='myRevues']");

        public IWebElement AllRevuesSection => FindElement(allRevuesSection);

        protected readonly By emptySearchResultMessage = By.XPath("//span[@class='col-12 text-muted']");
        
        public void OpenPage()
        {
            driver.Navigate().GoToUrl(PageUrl);
        }

        public bool IsTitleLastRevueTextCorrect(string lastCreatedRevueTitle)
        {
            return titleLastRevue.Text.Trim() == lastCreatedRevueTitle;
        }

        public bool IsDescriptionLastRevueTextCorrect(string lastCreatedRevueDescription)
        {
            return descriptionLastRevue.Text.Trim() == lastCreatedRevueDescription;
        }

        public bool IsSearchResultEmpty()
        {
            return GetText(emptySearchResultMessage) == "No Revues yet!";
        }
    }
}
