using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium.Safari;

namespace Test
{
    
    public class SeleniumTestsLD
    {
        [Test]
        public void TestMessageDisplayedAfterTripJoinedAndLeft()
        {
            var driver = new SafariDriver();
            driver.Url = "https://localhost:5001/Trips";
            var joinTripButton = driver.FindElementById("join_link");
            joinTripButton.Click();
            driver.PageSource.Should().Contain("joined");
            driver.Url = "https://localhost:5001/Trips";
            var leaveTripButton = driver.FindElementById("leave_link");
            leaveTripButton.Click();
            driver.PageSource.Should().Contain("left");
            driver.Close();
        }
    }
}