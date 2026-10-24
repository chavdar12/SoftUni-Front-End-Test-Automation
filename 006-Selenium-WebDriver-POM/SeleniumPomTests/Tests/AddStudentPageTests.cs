using SeleniumWebDriverPOMLab.Pages;

namespace SeleniumWebDriverPOMLab.Tests
{
    public class AddStudentPageTests : BaseTest
    {
        [Test, Order(1)]
        public void Test_AddStudentPage_VerifyingContent()
        {
            var page = new AddStudentPage(driver);
            page.OpenPage();

            var currentPageTitle = page.GetPageTitle();
            var currentPageHeadingText = page.GetPageHeadingText();

            Assert.That(currentPageTitle, Is.EqualTo("Add Student"), $"The title was expected to be 'Add Student' but was {currentPageTitle}");

            Assert.That(currentPageHeadingText, Is.EqualTo("Register New Student"), $"The heading was expected to be 'Register New Student' but was {currentPageHeadingText}");

            ClassicAssert.IsTrue(page.FieldStudentName.Displayed);
            ClassicAssert.IsTrue(page.FieldStudentEmail.Displayed);
            ClassicAssert.IsTrue(page.AddButton.Displayed);
        }

        [Test, Order(2)]
        public void Test_AddStudentPage_VerifyingLinks()
        {
            var page = new AddStudentPage(driver);
            page.OpenPage();
            ClassicAssert.IsTrue(page.LinkHomePage.Displayed);
            ClassicAssert.IsTrue(page.LinkViewStudentsPage.Displayed);
            ClassicAssert.IsTrue(page.LinkAddStudentsPage.Displayed);
        }

        [Test, Order(3)]
        public void Test_AddStudentPage_AddValidStudent()
        {
            var page = new AddStudentPage(driver);
            page.OpenPage();

            string studentName = page.GenerateRandomName();
            string studentEmail = page.GenerateRandomEmail();

            page.AddStudent(studentName, studentEmail);

            var currentUrl = driver.Url;
            Assert.That(currentUrl.Contains("/students"), "Expected URL to contain '/students' after adding a student.");

            var studentsData = $"{studentName} ({studentEmail})";
            Assert.That(driver.PageSource.Contains(studentsData), $"Expected the page source to contain the added student: {studentsData}");
        }

        [Test, Order(4)]
        public void Test_AddStudentPage_AddInvalidStudent()
        {
            var page = new AddStudentPage(driver);
            page.OpenPage();

            var studentName = page.GenerateRandomName();
            page.FieldStudentName.SendKeys(studentName);
            page.FieldStudentEmail.SendKeys("");
            page.AddButton.Click();

            page.WaitForErrorMessageToBeVisible();

            ClassicAssert.IsTrue(page.ElementErrorMsg.Displayed, "The error message was not displayed as expected.");
            Assert.That(page.GetErrorMsg(), Is.EqualTo("Cannot add student. Name and email fields are required!"), "The error message text was not as expected.");
        }
    }
}
