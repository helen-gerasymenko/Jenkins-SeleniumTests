using System.Configuration;
using NUnit.Framework;

namespace MySeleniumTests
{
    public class SmashingMagTests : TestBase
    {
        [Test]
        public void Can_go_to_smashing_mag_website()
        {
           
            Assert.IsTrue(Driver.Url == ConfigurationManager.AppSettings["BaseUrl"]);
        }

    }
}
