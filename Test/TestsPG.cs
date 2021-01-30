using System;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Safari;

namespace Test
{
    public class TestsPG
    {
        [Test]
        public void TestConfirmSegment()
        {

            var driver = new ChromeDriver
            {
                Url = "https://localhost:5001/Segments"
            };
            
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            
            //Create a new segment
            driver
                .FindElementById("create")
                .Click();
            driver
                .FindElementById("points")
                .SendKeys("42069");
            driver
                .FindElementById("length")
                .SendKeys("3");
            driver
                .FindElementById("points_dir1")
                .SendKeys("3");
            driver
                .FindElementById("points_dir2")
                .SendKeys("4");
            driver
                .FindElementById("creator")
                .SendKeys("1");
            driver
                .FindElementById("submit")
                .Click();
            driver
                .Navigate()
                .Refresh();
            driver
                .Navigate()
                .GoToUrl("https://localhost:5001/Segments/Index");
            driver
                .Navigate()
                .Refresh();
            driver
                .FindElementById("finish")
                .Click();
            driver
                .Navigate()
                .GoToUrl("https://localhost:5001/Segments/FinishedSegments");
            driver
                .PageSource
                .Should()
                .Contain("3");
        }
    }
}