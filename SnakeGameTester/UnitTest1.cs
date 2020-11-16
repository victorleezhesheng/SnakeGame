using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakeGame;
using System;

namespace SnakeGameTester
{
    [TestClass]
    public class ScoreName
    {
        [TestMethod]
        public void ScoreNameTest()
        {

            SnakeGame.ScoreName stat = new SnakeGame.ScoreName("");
            stat.Name = "Jon";

            Assert.AreEqual("Jon",stat.Name);
        }

        [TestMethod]
        public void Position()
        {

            SnakeGame.Index stat = new SnakeGame.Index(120,120);
            stat.IndexX = 150;
            Assert.AreEqual(150, stat.IndexX);

            stat.IndexY = 200;
            Assert.AreEqual(200, stat.IndexY);

            Assert.AreNotEqual(150, stat.IndexY);

        }

    }
}
