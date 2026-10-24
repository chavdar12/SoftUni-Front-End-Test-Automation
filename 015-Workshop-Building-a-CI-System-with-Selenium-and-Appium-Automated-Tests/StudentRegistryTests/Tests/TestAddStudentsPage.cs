using NUnit.Framework;
using Students_Registry_Automated_Tests.PageObjects;
using System;

namespace Students_Registry_Automated_Tests.Tests
{
    public class TestAddStudentPage : BaseTest
    {
        [Test]
        public void Test_TestAddStudentPage_Content()
        {
            var page = new AddStudentPage(driver);
            page.Open();
            ClassicAssert.AreEqual("Add Student", page.GetPageTitle());
            ClassicAssert.AreEqual("Register New Student", page.GetPageHeadingText());
            ClassicAssert.AreEqual("", page.FieldName.Text);
            ClassicAssert.AreEqual("", page.FieldEmail.Text);
            ClassicAssert.AreEqual("Add", page.ButtonSubmit.Text);
        }

        [Test]
        public void Test_TestAddStudentPage_Links()
        {
            var addStudentPage = new AddStudentPage(driver);

            addStudentPage.Open();
            addStudentPage.LinkHomePage.Click();
            ClassicAssert.IsTrue(new HomePage(driver).IsOpen());

            addStudentPage.Open();
            addStudentPage.LinkAddStudentPage.Click();
            ClassicAssert.IsTrue(new AddStudentPage(driver).IsOpen());

            addStudentPage.Open();
            addStudentPage.LinkViewStudentsPage.Click();
            ClassicAssert.IsTrue(new ViewStudentsPage(driver).IsOpen());
        }

        [Test]
        public void Test_TestAddStudentPage_AddValidStudent()
        {
            var page = new AddStudentPage(driver);
            page.Open();
            string name = "Pesho" + DateTime.Now.Ticks;
            string email = "pesho" + DateTime.Now.Ticks + "@gmail.com";
            page.AddStudent(name, email);
            var viewStudentsPage = new ViewStudentsPage(driver);
            ClassicAssert.IsTrue(viewStudentsPage.IsOpen());
            var students = viewStudentsPage.GetRegisteredStudents();
            string newStudent = name + " (" + email + ")";
            ClassicAssert.Contains(newStudent, students);
        }

        [Test]
        public void Test_TestAddStudentPage_AddInvalidStudent()
        {
            var page = new AddStudentPage(driver);
            page.Open();
            string name = "";
            string email = "pesho" + DateTime.Now.Ticks + "@gmail.com";
            page.AddStudent(name, email);
            ClassicAssert.IsTrue(page.IsOpen());
            ClassicAssert.IsTrue(page.ElementErrorMsg.Text.Contains("Cannot add student"));
        }

    }
}