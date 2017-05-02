using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace MobileApp.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void ListViewHasThreeItems()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("Item1"));
            AppResult[] results2 = app.WaitForElement(c => c.Marked("Item2"));
            AppResult[] results3 = app.WaitForElement(c => c.Marked("Item3"));
            app.Screenshot("Main Page");

            Assert.IsTrue(results.Any());
            Assert.IsTrue(results2.Any());
            Assert.IsTrue(results3.Any());
        }
    }
}
