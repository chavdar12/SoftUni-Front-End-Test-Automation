namespace IdeaCenterTestProject.Tests
{
    public class IdeaCenterTests : BaseTest
    {
        private static string lastCreatedIdeaTitle;
        private static string lastCreatedIdeaDescription;

        [Test, Order(1)]
        public void Test_CreateIdeaWithInvalidData_ShouldThrowErrorMessages()
        {
            createIdeaPage.OpenPage();

            createIdeaPage.CreateIdea("", "", "");

            ClassicAssert.True(createIdeaPage.IsMainErrorMessageCorrect(), "The main error message is not as expected.");

            ClassicAssert.True(createIdeaPage.IsTitleErrorMessageCorrect(), "The title error message is not as expected.");

            ClassicAssert.True(createIdeaPage.IsDescriptionErrorMessageCorrect(), "The description error message is not as expected.");
        }

        [Test, Order(2)]
        public void Test_CreateRandomIdea_ShouldSucceed()
        {
            lastCreatedIdeaTitle = "Idea " + GenerateRandomString(5);
            lastCreatedIdeaDescription = "Description " + GenerateRandomString(10);

            createIdeaPage.OpenPage();

            createIdeaPage.CreateIdea(lastCreatedIdeaTitle, "http://www.pictures.com/picture.jpg", lastCreatedIdeaDescription);

            Assert.That(driver.Url, Is.EqualTo(myIdeasPage.PageUrl), "URL is not correct");

            ClassicAssert.True(myIdeasPage.IsDescriptionLastIdeaTextCorrect(lastCreatedIdeaDescription), "The last created idea description is not as expected");
        }

        [Test, Order(3)]
        public void Test_ViewLastCreatedIdea_ShouldHaveCorrectTitleAndDescription()
        {
            myIdeasPage.OpenPage();

            myIdeasPage.viewButtonLastIdea.Click();

            ClassicAssert.True(ideasReadPage.IsIdeaTitleCorrect(lastCreatedIdeaTitle), "The title of the last created idea is not as expected in read page.");

            ClassicAssert.True(ideasReadPage.IsIdeaDescriptionCorrect(lastCreatedIdeaDescription), "The description of the last created idea is not as expected in read page.");
        }

        [Test, Order(4)]
        public void Test_EditLastCreatedIdeaTitle_ShouldSucceed()
        {
            myIdeasPage.OpenPage();

            myIdeasPage.editButtonLastIdea.Click();

            string updatedTitle = "Changed Title: " + lastCreatedIdeaTitle;

            basePage.Type(ideaEditPage.editTitleInput, updatedTitle);
            basePage.Click(ideaEditPage.editButton);

            Assert.That(driver.Url, Is.EqualTo(myIdeasPage.PageUrl), "Not correcrly redirected.");

            myIdeasPage.viewButtonLastIdea.Click();

            Assert.That(basePage.GetText(ideasReadPage.ideaTitle), Is.EqualTo(updatedTitle), "Title does not match");
        }

        [Test, Order(5)]
        public void Test_EditLastCreatedIdeaDescription_ShouldSucceed()
        {
            myIdeasPage.OpenPage();

            myIdeasPage.editButtonLastIdea.Click();

            string updatedDescription = "Changed Description: " + lastCreatedIdeaDescription;

            basePage.Type(ideaEditPage.editDescriptionInput, updatedDescription);
            basePage.Click(ideaEditPage.editButton);

            Assert.That(driver.Url, Is.EqualTo(myIdeasPage.PageUrl), "Not correcrly redirected.");

            myIdeasPage.viewButtonLastIdea.Click();

            Assert.That(basePage.GetText(ideasReadPage.ideaDescription), Is.EqualTo(updatedDescription), "Description does not match");
        }

        [Test, Order(6)]
        public void Test_DeleteLastCreatedIdea_ShouldSucceed()
        {
            myIdeasPage.OpenPage();

            myIdeasPage.deleteButtonLastIdea.Click();

            Assert.That(driver.Url, Is.EqualTo(myIdeasPage.PageUrl), "Not correcrly redirected.");

            bool isIdeaStillListed = myIdeasPage.IdeaCards.Any(card => card.Text.Contains(lastCreatedIdeaDescription));

            ClassicAssert.False(isIdeaStillListed, "The last created idea is not deleted as expected.");
        }
    }
}
