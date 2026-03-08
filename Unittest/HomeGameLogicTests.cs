using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schulprojekt.Data;
using Schulprojekt.Logic;

namespace Unittest
{
    [TestClass]
    public class HomeGameLogicTests
    {
        [TestMethod]
        public void IsPlayerMissing_ReturnsTrue_WhenNullOrWhitespace()
        {
            string? player = null;
            Assert.IsTrue(HomeGameLogic.IsPlayerMissing(player));

            player = "";
            Assert.IsTrue(HomeGameLogic.IsPlayerMissing(player));

            player = "   ";
            Assert.IsTrue(HomeGameLogic.IsPlayerMissing(player));
        }

        [TestMethod]
        public void IsPlayerMissing_ReturnsFalse_WhenHasText()
        {
            string? player = "Testspieler";
            Assert.IsFalse(HomeGameLogic.IsPlayerMissing(player));
        }

        [TestMethod]
        public void IsQuestionSetMissing_ReturnsTrue_WhenNullOrEmptyTitle()
        {
            QuestionSet? questionSet = new QuestionSet();

            // Case 1: Title = ""
            questionSet.Title = "";
            Assert.IsTrue(HomeGameLogic.IsQuestionSetMissing(questionSet));

            // Case 2 whitespace Title
            questionSet.Title = "   ";
            Assert.IsTrue(HomeGameLogic.IsQuestionSetMissing(questionSet));

            // Case 3: questionSet = null
            questionSet = null;
            Assert.IsTrue(HomeGameLogic.IsQuestionSetMissing(questionSet));

            // Case : Valid title (NEUES Objekt, weil Objekt vorher null gesetzt)
            questionSet = new QuestionSet { Title = "test" };
            Assert.IsFalse(HomeGameLogic.IsQuestionSetMissing(questionSet));
        }

        [TestMethod]
        public void IsQuestionSetMissing_ReturnsFalse_WhenTitleExists()
        {
            var questionSet = new QuestionSet { Title = "Testquiz" };
            Assert.IsFalse(HomeGameLogic.IsQuestionSetMissing(questionSet));
        }

        [TestMethod]
        public void IsThemeMissing_ReturnsTrue_WhenNullOrEmptyName()
        {
            Thema? thema = null;
            Assert.IsTrue(HomeGameLogic.IsThemeMissing(thema));

            thema = new Thema { Name = null };
            Assert.IsTrue(HomeGameLogic.IsThemeMissing(thema));

            thema = new Thema { Name = "" };
            Assert.IsTrue(HomeGameLogic.IsThemeMissing(thema));

            thema = new Thema { Name = "   " };
            Assert.IsTrue(HomeGameLogic.IsThemeMissing(thema));
        }

        [TestMethod]
        public void IsThemeMissing_ReturnsFalse_WhenNameExists()
        {
            Thema? thema = new Thema { Name = "Hunter x Hunter" };
            Assert.IsFalse(HomeGameLogic.IsThemeMissing(thema));
        }

        [TestMethod]
        public void BuildQuizRoute_ReturnsCorrectUrl()
        {
            var themaId = 5;
            var questionSetId = 10;

            var url = HomeGameLogic.BuildQuizRoute(themaId, questionSetId);

            Assert.AreEqual("/Quiz/5/10", url);
        }
    }
}