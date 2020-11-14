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

namespace Selectable
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
        public void SelectableList()
        {
            var clickOnIteractions = _driver.FindElement(By.XPath("//body//div[5]"));
            clickOnIteractions.Click();

            _js.ExecuteScript("window.scrollBy(0,900);");
            var clickOnSelectable = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='element-list collapse show']//li[@id='item-1']")));
            clickOnSelectable.Click();

            var CrasJustoOdio = _driver.FindElement(By.XPath("//*[@id='verticalListContainer']/li[1]"));
            var DapibusAcFacilisisIn = _driver.FindElement(By.XPath("//*[@id='verticalListContainer']/li[2]"));

            _builder
                .MoveToElement(CrasJustoOdio)
                .Click()
                .MoveToElement(DapibusAcFacilisisIn)
                .Click()
                .Release()
                .Perform();
        }
        [Test]
        public void SelectableGrid()
        {

            var clickOnIteractions = _driver.FindElement(By.XPath("//body//div[5]"));
            clickOnIteractions.Click();

            _js.ExecuteScript("window.scrollBy(0,900);");
            var clickOnSelectable = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='element-list collapse show']//li[@id='item-1']")));
            clickOnSelectable.Click();

            var clickOnGrid = _driver.FindElement(By.XPath("//a[@id='demo-tab-grid']"));
            clickOnGrid.Click();

            var clicnOnNumberOne = _driver.FindElement(By.XPath("//*[@id='row1']/li[1]"));
            var clickOnNumberTwo = _driver.FindElement(By.XPath("//*[@id='row1']/li[2]"));
            var clickOnNumberThree = _driver.FindElement(By.XPath("//*[@id='row1']/li[3]"));

            _builder
                 .MoveToElement(clicnOnNumberOne)
                .Click()
                .MoveToElement(clickOnNumberTwo)
                .Click()
                .MoveToElement(clickOnNumberThree)
                .Click()
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