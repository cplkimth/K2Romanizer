#region
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endregion

namespace K2Romanizer.Tests
{
    [TestClass]
    public class RomanizerTests
    {
        private const string Source = "가처분";

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
        }

        [TestMethod]
        public void Pascal()
        {
            var target = Romanizer.Romanize(Source, Casing.Pascal);
            Assert.AreEqual("GaCheoBun", target);
        }

        [TestMethod]
        public void Camel()
        {
            var target = Romanizer.Romanize(Source, Casing.Camel);
            Assert.AreEqual("gaCheoBun", target);
        }

        [TestMethod]
        public void Snake()
        {
            var target = Romanizer.Romanize(Source, Casing.Snake);
            Assert.AreEqual("ga_cheo_bun", target);
        }

        [TestMethod]
        public void Noun()
        {
            var target = Romanizer.Romanize(Source, Casing.Noun);
            Assert.AreEqual("Gacheobun", target);
        }

        [TestMethod]
        public void Upper()
        {
            var target = Romanizer.Romanize(Source, Casing.Upper);
            Assert.AreEqual("GACHEOBUN", target);
        }

        [TestMethod]
        public void Lower()
        {
            var target = Romanizer.Romanize(Source, Casing.Lower);
            Assert.AreEqual("gacheobun", target);
        }

        // [TestMethod]
        // public void UserData()
        // {
        //     var target = Romanizer.Romanize("가뚬똙나", Casing.Lower);
        //
        //     var text = File.ReadAllText(UserDataPath);
        //
        //     Assert.IsTrue(text.Contains("ddom"));
        //     Assert.IsTrue(text.Contains("ddolg"));
        // }
    }
}