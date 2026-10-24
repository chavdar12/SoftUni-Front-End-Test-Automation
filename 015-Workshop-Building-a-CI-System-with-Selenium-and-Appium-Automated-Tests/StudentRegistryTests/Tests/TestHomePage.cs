using NUnit.Framework;
using Students_Registry_Automated_Tests.PageObjects;

namespace Students_Registry_Automated_Tests.Tests
{
    public class TestHomePage : BaseTest
    {
        [Test]
        public void Test_HomePage_Content()
        {
            var page = new HomePage(driver);
            page.Open();
            ClassicAssert.AreEqual("MVC Example", page.GetPageTitle());
            ClassicAssert.AreEqual("Students Registry", page.GetPageHeadingText());
            page.GetStudentsCount();
            Assert.Pass();
        }

        [Test]
        public void Test_HomePage_Links()
        {
            var homePage = new HomePage(driver);

            homePage.Open();
            homePage.LinkHomePage.Click();
            ClassicAssert.IsTrue(new HomePage(driver).IsOpen());

            homePage.Open();
            homePage.LinkAddStudentPage.Click();
            ClassicAssert.IsTrue(new AddStudentPage(driver).IsOpen());

            homePage.Open();
            homePage.LinkViewStudentsPage.Click();
            ClassicAssert.IsTrue(new ViewStudentsPage(driver).IsOpen());
        }
    }
}