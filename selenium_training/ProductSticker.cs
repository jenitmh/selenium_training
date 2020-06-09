using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace selenium_training
{
    [TestFixture]
    public class ProductStikers
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private string baseUrl;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            baseUrl = "http://localhost/litecart/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
        }

        [Test]
        public void ProductStiker()
        {
            driver.Url = baseUrl;
            StickerChecker();

        }

        private void StickerChecker()
        {
            var Products = driver.FindElements(By.CssSelector("article.product-column"));
            int index = 0;

            for (int i = 0; i < Products.Count; i++)
            {
                var Sticker = Products[i].FindElements(By.CssSelector("div.sticker"));
                if (Sticker.Count == 1)
                {
                  index++;
                }
            }
            Assert.True(index == Products.Count);
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
