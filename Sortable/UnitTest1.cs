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
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace Sortable
{
    public class Tests
    {
        public IWebDriver _driver;
        private WebDriverWait _wait;
        private Actions _builder;
        private Fixture _fiture;
        private IJavaScriptExecutor _js;
        private Actions _action;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _driver.Navigate().GoToUrl("http://demoqa.com/");
            _driver.Manage().Window.Maximize();
            _builder = new Actions(_driver);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));
            _js = _driver as IJavaScriptExecutor;
            _action = new Actions(_driver);
        }

        [Test]
        public void SortableList()
        {
            var clickOnIteractions = _driver.FindElement(By.XPath("//body//div[5]"));
            clickOnIteractions.Click();

            _js.ExecuteScript("window.scrollBy(0,900);");
            var clickOnSortable = _wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='element-list collapse show']//li[@id='item-0']")));
            clickOnSortable.Click();

            var listItems = _driver.FindElements(By.XPath("//*[@id='demo-tabpane-list']/div/div"));

            Sort(listItems, "One", "Three");
            Sort(listItems, "Three", "Six");

            _driver.Dispose();
        }


        private void Sort(ReadOnlyCollection<IWebElement> list, string itemToMove, string itemToReplace)
        {
            var elToMove = list.First(a => a.Text == itemToMove);
            var elToReplace = list.First(a => a.Text == itemToReplace);

            var actions = new Actions(_driver);
            actions
                 .ClickAndHold(elToMove)
                 .MoveToElement(elToReplace)
                 .MoveByOffset(0, 5)
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