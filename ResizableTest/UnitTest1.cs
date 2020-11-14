using NUnit.Framework;
using OpenQA;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System;
using AutoFixture;
using System.IO;
using OpenQA.Selenium.Interactions;

namespace ResizableTest
{
    public class Tests
    {
        public IWebDriver _driver;
        private WebDriverWait _wait;
        private Actions _builder;
        private Fixture _fiture;
        private IJavaScriptExecutor _js;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _driver.Navigate().GoToUrl("http://demoqa.com/");
            _driver.Manage().Window.Maximize();
            _builder = new Actions(_driver);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            _js = _driver as IJavaScriptExecutor;
        }

        [Test]
        public void ResizableTestOne ()
        {
            var clickOnIteractions = _driver.FindElement(By.XPath("//body//div[5]"));
            clickOnIteractions.Click();

            _js.ExecuteScript("window.scrollBy(0,900);");
            var clickOnResizable = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//span[contains(text(),'Resizable')]")));
            clickOnResizable.Click();

            var resizableBoxSize = _driver.FindElement(By.XPath("//div[@id='resizableBoxWithRestriction']")).Size;
            var resizableHandle = _driver.FindElement(By.XPath("//div[@id='resizableBoxWithRestriction']//span[@class='react-resizable-handle react-resizable-handle-se']"));
            var containerAreaMaxSize = _driver.FindElement(By.XPath("//div[@class='constraint-area']")).Size;
            _builder
                .MoveToElement(resizableHandle)
                .ClickAndHold()
                .MoveByOffset(300, 100)
                .Release()
                .Perform();

            var resizableBoxAfterResizing = _driver.FindElement(By.XPath("//div[@id='resizableBoxWithRestriction']")).Size;
            Assert.AreEqual(resizableBoxAfterResizing, containerAreaMaxSize);
        }
        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }




    }
}