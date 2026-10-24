using SeleniumWebDriverPOMLab.Pages;

namespace SeleniumWebDriverPOMLab.Tests
{
    public class HomePageTests : BaseTest
    {
        [Test]
        public void Test_HomePage_VerifyingContent()
        {
            var page = new HomePage(driver);
            page.OpenPage();

            var currentPageTitle = page.GetPageTitle();
            var currentPageHeadingText = page.GetPageHeadingText();
            var currentStudentsCount = int.Parse(page.GetStudentsCount());

            Assert.That(currentPageTitle, Is.EqualTo("MVC Example"), $"The title was expected to be 'MVC Example' but was {currentPageTitle}");

            Assert.That(currentPageHeadingText, Is.EqualTo("Students Registry"), $"The heading was expected to be 'Students Registry' but was {currentPageHeadingText}");

            ClassicAssert.IsTrue(currentStudentsCount > 1, "The count of the students should be bigger than 1, but was not as expected.");
        }

        [Test]
        public void Test_HomePage_VerifyingLinks()
        {
            var page = new HomePage(driver);
            page.OpenPage();
            ClassicAssert.IsTrue(page.LinkHomePage.Displayed);
            ClassicAssert.IsTrue(page.LinkViewStudentsPage.Displayed);
            ClassicAssert.IsTrue(page.LinkAddStudentsPage.Displayed);
        }
    }
}
