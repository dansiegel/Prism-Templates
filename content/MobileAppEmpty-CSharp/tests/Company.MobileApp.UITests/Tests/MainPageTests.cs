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
            app.Screenshot("Main Page");
        }
    }
}
