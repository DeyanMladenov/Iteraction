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

namespace Draggable
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
        public void DragabbleTestWithAxis()
        {
            var clickOnIteractions = _driver.FindElement(By.XPath("//body//div[5]"));
            clickOnIteractions.Click();

            
            _js.ExecuteScript("window.scrollBy(0,900);");
            var clickOnDragabble = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='element-list collapse show']//li[@id='item-4']")));
            clickOnDragabble.Click();

            var clickOnAxisRestricted = _driver.FindElement(By.Id("draggableExample-tab-axisRestriction"));
            clickOnAxisRestricted.Click();

            var selectElementX = _driver.FindElement(By.XPath("//div[@id='restrictedX']"));
            var selectElementY = _driver.FindElement(By.XPath("//div[@id='restrictedY']"));

            _builder
                .ClickAndHold(selectElementX)
                .MoveByOffset(67,0)
                .Release()
                .MoveToElement(selectElementY)
                .ClickAndHold()
                .MoveByOffset(0,210)
                .Release()
                .Perform();
        }

        [Test]
        public void DragabbleTestWithContainerRestrited()
        {
            var clickOnIteractions = _driver.FindElement(By.XPath("//body//div[5]"));
            clickOnIteractions.Click();

            
            _js.ExecuteScript("window.scrollBy(0,900);");
            var clickOnDragabble = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='element-list collapse show']//li[@id='item-4']")));
            clickOnDragabble.Click();

            var clickOnContainerRestrited = _driver.FindElement(By.XPath("//a[@id='draggableExample-tab-containerRestriction']"));
            clickOnContainerRestrited.Click();

            var selectWithInBox = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='draggable ui-widget-content ui-draggable ui-draggable-handle']")));
            _builder
                .MoveToElement(selectWithInBox)
                .ClickAndHold()
                .MoveByOffset(374,104)
                .Release()
                .Perform();

           
            _js.ExecuteScript("window.scrollBy(0,250);");
            var selectParentText = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//span[@class='ui-widget-header ui-draggable ui-draggable-handle']")));
            _builder
                .MoveToElement(selectParentText)
                .ClickAndHold()
                .MoveByOffset(13, 58)
                .Release()
                .Perform();
        }
        [Test]
        public void DragabbleTestWithCursorStyle()
        {
            var clickOnIteractions = _driver.FindElement(By.XPath("//body//div[5]"));
            clickOnIteractions.Click();

            _js.ExecuteScript("window.scrollBy(0,900);");
            var clickOnDragabble = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='element-list collapse show']//li[@id='item-4']")));
            clickOnDragabble.Click();

            
            var clickOnCursorStyle = _wait.Until(ExpectedConditions.ElementExists(By.Id("draggableExample-tab-cursorStyle")));
            clickOnCursorStyle.Click();

            var centerCursor = _driver.FindElement(By.XPath("//*[@id='cursorCenter']"));
            var topLeftCursor = _driver.FindElement(By.XPath("//*[@id='cursorTopLeft']"));
            var bottomCursor = _driver.FindElement(By.XPath("//*[@id='cursorBottom']"));

            _builder
                .MoveToElement(centerCursor)
                .ClickAndHold()
                .MoveByOffset(104, -15)
                .Release()
                .MoveToElement(topLeftCursor)
                .ClickAndHold()
                .MoveByOffset(201, 3)
                .Release()
                .MoveToElement(bottomCursor)
                .ClickAndHold()
                .MoveByOffset(315, 12)
                .Release()
                .Perform();

        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }










    }
    
    

}