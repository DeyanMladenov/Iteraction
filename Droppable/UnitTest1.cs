using NUnit.Framework;
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


namespace Droppable
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
        public void DroppableAccepTest()
        {
            var clickOnIteractions = _driver.FindElement(By.XPath("//body//div[5]"));
            clickOnIteractions.Click();

            _js.ExecuteScript("window.scrollBy(0,900);");
            var clickOnDroppable = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='element-list collapse show']//li[@id='item-3']")));
            clickOnDroppable.Click();

            var clickOnAccept = _driver.FindElement(By.XPath("//a[@id='droppableExample-tab-accept']"));
            clickOnAccept.Click();

            var acceptableBox = _driver.FindElement(By.XPath("//div[@id='acceptable']"));
            var targetBox = _driver.FindElement(By.XPath("//div[@id='acceptDropContainer']//div[@id='droppable']"));
            var targetBoxColor = targetBox.GetCssValue("color");
            _builder
                .MoveToElement(acceptableBox)
                .ClickAndHold()
                .MoveToElement(targetBox)
                .Release()
                .Perform();
            var targetBoxColorAfterMove = targetBox.GetCssValue("background-color");
            Assert.AreNotEqual(targetBoxColor, targetBoxColorAfterMove);
            
        }
        [Test]
        public void DroppablePreventPropogation()
        {
            var clickOnIteractions = _driver.FindElement(By.XPath("//body//div[5]"));
            clickOnIteractions.Click();

            _js.ExecuteScript("window.scrollBy(0,900);");
            var clickOnDroppable = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='element-list collapse show']//li[@id='item-3']")));
            clickOnDroppable.Click();

            var clickOnPreventPropogation = _driver.FindElement(By.XPath("//a[@id='droppableExample-tab-preventPropogation']"));
            clickOnPreventPropogation.Click();

            var selectDragMeBox = _driver.FindElement(By.Id("dragBox"));
            var outerDroppableBox = _driver.FindElement(By.XPath("//div[@id='notGreedyDropBox']"));
            var outerDroppableBoxColor = outerDroppableBox.GetCssValue("color");
            _builder
                .MoveToElement(selectDragMeBox)
                .ClickAndHold()
                .MoveByOffset(64, 17)
                .Perform();
            var outerDroppableBoxColorAfter = outerDroppableBox.GetCssValue("background-color");
            Assert.AreNotEqual(outerDroppableBoxColor, outerDroppableBoxColorAfter);

        }
        [Test]
        public void DropabbleRevertDragabble()
        {
            var clickOnIteractions = _driver.FindElement(By.XPath("//body//div[5]"));
            clickOnIteractions.Click();

            _js.ExecuteScript("window.scrollBy(0,900);");
            var clickOnDroppable = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='element-list collapse show']//li[@id='item-3']")));
            clickOnDroppable.Click();

            var clickOnRevertDragabbleButton = _driver.FindElement(By.XPath("//a[@id='droppableExample-tab-revertable']"));
            clickOnRevertDragabbleButton.Click();

            var willRevertBox = _driver.FindElement(By.XPath("//div[@id='revertable']"));
            var targetBox = _driver.FindElement(By.XPath("//div[@id='revertableDropContainer']//div[@id='droppable']"));
            var targetBoxColor = _driver.FindElement(By.XPath("//div[@id='revertableDropContainer']//div[@id='droppable']")).GetCssValue("background-color");
            _builder
                .MoveToElement(willRevertBox)
                .ClickAndHold()
                .MoveToElement(targetBox,100,40)
                .Perform();

            var targetBoxColorAfterMove = _driver.FindElement(By.XPath("//div[@id='revertableDropContainer']//div[@id='droppable']")).GetCssValue("background-color");
            Assert.AreNotEqual(targetBoxColor, targetBoxColorAfterMove);

        }


        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }



    }

}