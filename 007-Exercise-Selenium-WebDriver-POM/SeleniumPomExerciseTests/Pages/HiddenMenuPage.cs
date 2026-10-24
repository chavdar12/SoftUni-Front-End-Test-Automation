namespace SeleniumWebDriverPOMExercise.Pages
{
    public class HiddenMenuPage : BasePage
    {
        protected readonly By hamburgerMenuButton = By.XPath("//button[@id='react-burger-menu-btn']");

        protected readonly By logoutButton = By.XPath("//a[@id='logout_sidebar_link']");

        public HiddenMenuPage(IWebDriver driver) : base(driver)
        {
        }

        public void ClickMenuButton()
        {
            Click(hamburgerMenuButton);
        }

        public void ClickLogoutButton()
        {
            Click(logoutButton);
        }

        public bool IsMenuOpen()
        {
            return WhenStable(() => FindElement(logoutButton).Displayed);
        }
    }
}
