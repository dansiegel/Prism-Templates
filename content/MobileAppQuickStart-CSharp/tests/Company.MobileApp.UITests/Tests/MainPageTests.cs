using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Company.MobileApp.UITests.Tests
{
    public class MainPageTests : AbstractSetup
    {
        public Tests(Platform platform)
            : base(platform)
        {
        }

        [Test]
        public void ListViewHasThreeItems()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("Item1"));
            AppResult[] results2 = app.WaitForElement(c => c.Marked("Item2"));
            AppResult[] results3 = app.WaitForElement(c => c.Marked("Item3"));
            app.Screenshot("Main Page");
        }
    }
}
