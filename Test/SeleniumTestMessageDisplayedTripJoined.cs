using System;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace Test
{
    
    public class SeleniumTestsLD
    {
        [Test]
        public void TestMessageDisplayedAfterTripJoinedAndLeft()
        {
            var driver = new SafariDriver {Url = "https://localhost:5001/Trips"};
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            var joinTripButton = driver.FindElementById("join_link");
            joinTripButton.Click();
            driver.PageSource.Should().Contain("joined");
            driver.Navigate().GoToUrl("https://localhost:5001/Trips");
            var leaveTripButton = driver.FindElementById("leave_link");
            leaveTripButton.Click();
            driver.PageSource.Should().Contain("left");
            driver.Close();
        }
    }
}