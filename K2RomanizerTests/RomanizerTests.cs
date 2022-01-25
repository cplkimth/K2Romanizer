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

        private static string Directory => System.OperatingSystem.IsWindows() switch
        {
            true => @"C:\git\K2Romanizer\K2Romanizer\bin\Debug\net6.0",
            false => "/home/thkim/git/K2Romanizer/K2Romanizer/bin/Debug/net6.0"
        };

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            // CharConverter.Instance.Initialize(Path.Combine(Directory, "SystemData.txt"), Path.Combine(Directory, "UserData.txt"));
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
    }
}