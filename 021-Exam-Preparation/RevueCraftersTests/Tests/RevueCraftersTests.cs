using RevueCraftersTestProject.Pages;

namespace RevueCraftersTestProject.Tests
{
    public class RevueCraftersTests : BaseTest
    {
        string lastCreatedRevueTitle;
        string lastCreatedRevueDescription;
        string updatedTitle;

        [Test, Order(1)]
        public void Test_CreateRevueWithInvalidData_ShouldThrowErrorMessages()
        {
            createRevuePage.OpenPage();

            actions.ScrollToElement(createRevuePage.CreateRevueCardElement).Perform();

            createRevuePage.CreateNewRevue("", "", "");

            ClassicAssert.True(createRevuePage.IsMainErroMessageCorrect(), "The main error message is not as expected.");
            ClassicAssert.True(createRevuePage.IsTitleErroMessageCorrect(), "The title error message is not as expected.");
            ClassicAssert.True(createRevuePage.IsDescriptionErroMessageCorrect(), "The description error message is not as expected.");
        }

        [Test, Order(2)]
        public void Test_CreateRandomRevue_ShouldSucceed()
        {
            lastCreatedRevueTitle = "Revue " + GenerateRandomString(5);
            lastCreatedRevueDescription = "Description " + GenerateRandomString(10);

            createRevuePage.OpenPage();

            actions.ScrollToElement(createRevuePage.CreateRevueCardElement).Perform();

            createRevuePage.CreateNewRevue(lastCreatedRevueTitle, "http://www.pictures.com/picture.jpg", lastCreatedRevueDescription);

            string expectedUrl = "https://d3s5nxhwblsjbi.cloudfront.net/Revue/MyRevues#createRevue";

            Assert.That(driver.Url, Is.EqualTo(expectedUrl), "URL is not correct");

            ClassicAssert.True(myRevuesPage.IsTitleLastRevueTextCorrect(lastCreatedRevueTitle), "The last created revue title is not as expected.");
            ClassicAssert.True(myRevuesPage.IsDescriptionLastRevueTextCorrect(lastCreatedRevueDescription), "The last revue descripton is not as expected.");
        }

        [Test, Order(3)]
        public void Test_SeachForRevueWithTitle_ShouldSucceed()
        {
            myRevuesPage.OpenPage();

            actions.ScrollToElement(myRevuesPage.SearchBarElement).Perform();

            myRevuesPage.SearchBarElement.Clear();
            myRevuesPage.SearchBarElement.SendKeys(lastCreatedRevueTitle);
            myRevuesPage.SearchButtonElement.Click();

            Assert.That(myRevuesPage.SearchedElementTitle.Text, Is.EqualTo(lastCreatedRevueTitle), "The searched keyword title does not match the last created one.");
        }

        [Test, Order(4)]
        public void Test_EditRevueTitle_ShouldSucceed()
        {
            myRevuesPage.OpenPage();

            actions.ScrollToElement(myRevuesPage.AllRevuesSection).Perform();

            Assert.That(myRevuesPage.RevuesCards.Count, Is.GreaterThan(0));

            myRevuesPage.editButtonLastRevue.Click();

            actions.ScrollToElement(editRevuePage.EditRevueCardElement).Perform();

            updatedTitle = "Changed Title: " + lastCreatedRevueTitle;

            basePage.Type(editRevuePage.editTitleInput, updatedTitle);

            basePage.Click(editRevuePage.editButton);

            string expectedUrl = "https://d3s5nxhwblsjbi.cloudfront.net/Revue/MyRevues";

            Assert.That(driver.Url, Is.EqualTo(expectedUrl), "Not correcrly redirected.");

            actions.ScrollToElement(myRevuesPage.AllRevuesSection).Perform();

            ClassicAssert.True(myRevuesPage.RevuesCards.Last().Text.Contains(updatedTitle));
        }

        [Test, Order(5)]
        public void Test_DeleteLastCreatedRevue_ShouldSucceed()
        {
            myRevuesPage.OpenPage();

            actions.ScrollToElement(myRevuesPage.AllRevuesSection).Perform();

            var revuesCount = myRevuesPage.RevuesCards.Count;
            Assert.That(revuesCount, Is.GreaterThan(0));

            actions.ScrollToElement(myRevuesPage.deleteButtonLastRevue).Perform();

            myRevuesPage.deleteButtonLastRevue.Click();

            driver.Navigate().Refresh();

            string expectedUrl = "https://d3s5nxhwblsjbi.cloudfront.net/Revue/MyRevues";

            Assert.That(driver.Url, Is.EqualTo(expectedUrl), "Not correcrly redirected.");

            actions.ScrollToElement(myRevuesPage.AllRevuesSection).Perform();

            // var currentRevues = basePage.FindElements(myRevuesPage.revuesCards);
            var currentRevues = myRevuesPage.RevuesCards;

            Assert.That(currentRevues.Count, Is.LessThan(revuesCount), "The number of Revues did not decrease.");

            bool isRevueDeleted = currentRevues.Any(card => card.Text.Contains(updatedTitle) && card.Text.Contains(lastCreatedRevueDescription));

            ClassicAssert.False(isRevueDeleted, "The last created idea is not deleted as expected.");
        }

        [Test, Order(6)]
        public void Test_SearchForTheDeletedOrNonExistingRevue_ShouldNotFindIt()
        {
            myRevuesPage.OpenPage();

            actions.ScrollToElement(myRevuesPage.SearchBarElement).Perform();

            myRevuesPage.SearchBarElement.Clear();
            myRevuesPage.SearchBarElement.SendKeys(updatedTitle);
            myRevuesPage.SearchButtonElement.Click();

            ClassicAssert.True(myRevuesPage.IsSearchResultEmpty(), "The searched revue was found but should not found it.");
        }
    }
}
