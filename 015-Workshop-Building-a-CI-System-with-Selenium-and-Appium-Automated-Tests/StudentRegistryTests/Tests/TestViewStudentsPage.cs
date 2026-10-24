using NUnit.Framework;
using Students_Registry_Automated_Tests.PageObjects;

namespace Students_Registry_Automated_Tests.Tests
{
    public class TestViewStudentsPage : BaseTest
    {
        [Test]
        public void Test_ViewStudentsPage_Content()
        {
            var page = new ViewStudentsPage(driver);
            page.Open();
            ClassicAssert.AreEqual("Students", page.GetPageTitle());
            ClassicAssert.AreEqual("Registered Students", page.GetPageHeadingText());
            
            var students = page.GetRegisteredStudents();
            foreach (string st in students)
            {
                ClassicAssert.IsTrue(st.IndexOf("(") > 0);
                ClassicAssert.IsTrue(st.LastIndexOf(")") == st.Length-1);
            }
        }

        [Test]
        public void Test_ViewStudentsPage_Links()
        {
            var viewStudentsPage = new ViewStudentsPage(driver);

            viewStudentsPage.Open();
            viewStudentsPage.LinkHomePage.Click();
            ClassicAssert.IsTrue(new HomePage(driver).IsOpen());

            viewStudentsPage.Open();
            viewStudentsPage.LinkAddStudentPage.Click();
            ClassicAssert.IsTrue(new AddStudentPage(driver).IsOpen());

            viewStudentsPage.Open();
            viewStudentsPage.LinkViewStudentsPage.Click();
            ClassicAssert.IsTrue(new ViewStudentsPage(driver).IsOpen());
        }
    }
}