namespace IdeaCenterTestProject.Pages
{
    public class IdeasReadPage : BasePage
    {
        public IdeasReadPage(IWebDriver driver) :base(driver)
        {
        }

        public readonly By ideaTitle = By.XPath("//h1[@class='mb-0 h4']");

        public readonly By ideaDescription = By.XPath("//p[@class='offset-lg-3 col-lg-6']");

        public bool IsIdeaTitleCorrect(string lastCreatedIdeaTitle)
        {
            return GetText(ideaTitle) == lastCreatedIdeaTitle.Trim();
        }

        public bool IsIdeaDescriptionCorrect(string lastCreatedIdeaDescription)
        {
            return GetText(ideaDescription) == lastCreatedIdeaDescription.Trim();
        }
    }
}
