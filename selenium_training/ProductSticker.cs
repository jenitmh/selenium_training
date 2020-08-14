using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTraining
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

        /// <summary>
        /// Проверяет количество стикеров у товара
        /// </summary>
        private void StickerChecker()
        {
            // Переменная списка со всеми карточками товара найденными на странице
            var productList = driver.FindElements(By.CssSelector("article.product-column"));

            Assert.True(productList.Count > 0, "Нет товаров на странице");

            int productIndex = 0;

            // Перебор всех карточек товара
            for (int i = 0; i < productList.Count; i++)
            {
                // Переменная со списком всех стикеров товара
                var productSticker = productList[i].FindElements(By.CssSelector("div.sticker"));

                // Проверка всех товаров на наличие единственного стикера
                if (productSticker.Count == 1)
                {
                    productIndex++;
                }
            }

            Assert.True(productIndex == productList.Count, "У " + (productList.Count - productIndex) + " из " + productList.Count + " товаров нет стикера");
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
