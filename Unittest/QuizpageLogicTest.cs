using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schulprojekt.Data;
using Schulprojekt.Logic;

namespace Unittest
{
    [TestClass]
    public class QuizpageLogicTests
    {
        // -------------------------
        // IsPlayerMissing
        // -------------------------

        [TestMethod]
        public void IsPlayerMissing_ReturnsTrue_WhenNull()
        {
            string? player = null;
            Assert.IsTrue( QuizpageLogic.IsPlayerMissing(player));
        }

        [TestMethod]
        public void IsPlayerMissing_ReturnsTrue_WhenEmpty()
        {
            string? player = "";
            Assert.IsTrue(HomeGameLogic.IsPlayerMissing(player));
        }

        [TestMethod]
        public void IsPlayerMissing_ReturnsTrue_WhenWhitespace()
        {
            string? player = " ";
            Assert.IsTrue(HomeGameLogic.IsPlayerMissing(player));
        }

        [TestMethod]
        public void IsPlayerMissing_ReturnsFalse_WhenHasText()
        {
            string? player = "Testspieler";
            Assert.IsFalse(HomeGameLogic.IsPlayerMissing(player));
        }

        // -------------------------
        // IsQuestionSetMissing
        // -------------------------

        [TestMethod]
        public void IsQuestionSetMissing_ReturnsTrue_WhenQuestionSetIsNull()
        {
            QuestionSet? questionSet = null;
            Assert.IsTrue(HomeGameLogic.IsQuestionSetMissing(questionSet));
        }

        [TestMethod]
        public void IsQuestionSetMissing_ReturnsTrue_WhenTitleIsEmpty()
        {
            var questionSet = new QuestionSet { Title = "" };
            Assert.IsTrue(HomeGameLogic.IsQuestionSetMissing(questionSet));
        }

        [TestMethod]
        public void IsQuestionSetMissing_ReturnsTrue_WhenTitleIsWhitespace()
        {
            var questionSet = new QuestionSet { Title = " " };
            Assert.IsTrue(HomeGameLogic.IsQuestionSetMissing(questionSet));
        }

        [TestMethod]
        public void IsQuestionSetMissing_ReturnsFalse_WhenTitleExists()
        {
            var questionSet = new QuestionSet { Title = "Testquiz" };
            Assert.IsFalse(HomeGameLogic.IsQuestionSetMissing(questionSet));
        }

        // -------------------------
        // IsThemeMissing
        // -------------------------

        [TestMethod]
        public void IsThemeMissing_ReturnsTrue_WhenThemaIsNull()
        {
            Thema? thema = null;
            Assert.IsTrue(HomeGameLogic.IsThemeMissing(thema));
        }

        [TestMethod]
        public void IsThemeMissing_ReturnsTrue_WhenNameIsEmpty()
        {
            var thema = new Thema { Name = "" };
            Assert.IsTrue(HomeGameLogic.IsThemeMissing(thema));
        }

        [TestMethod]
        public void IsThemeMissing_ReturnsTrue_WhenNameIsWhitespace()
        {
            var thema = new Thema { Name = " " };
            Assert.IsTrue(HomeGameLogic.IsThemeMissing(thema));
        }

        [TestMethod]
        public void IsThemeMissing_ReturnsFalse_WhenNameExists()
        {
            var thema = new Thema { Name = "Hunter x Hunter" };
            Assert.IsFalse(HomeGameLogic.IsThemeMissing(thema));
        }

        // -------------------------
        // BuildQuizRoute
        // -------------------------

        [TestMethod]
        public void BuildQuizRoute_ReturnsCorrectUrl()
        {
            var spielerId = 5;
            var questionSetId = 10;

            var url = HomeGameLogic.BuildQuizRoute(spielerId, questionSetId);

            Assert.AreEqual("/Quiz/5/10", url);
        }
    }
}


