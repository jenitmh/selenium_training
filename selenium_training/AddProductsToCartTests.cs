using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTraining
{
    [TestFixture]
    public class AddProductsToCartTests
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
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [Test]
        [Obsolete]
        public void AddProductsToCartTest()
        {
            // Переменная с количеством товаров
            int quantityProducts = 3;

            AddProductsToCart(quantityProducts);
            RemoveProductFromCart(quantityProducts);
        }

        /// <summary>
        /// Добавляет указанное количество товаров в корзину.
        /// </summary>
        /// <param name="quantityProducts"></param>
        [Obsolete]
        private void AddProductsToCart(int quantityProducts)
        {
            for (int i = 0; i < quantityProducts; i++)
            {
                OpenHomePage();
                OpenProductPage(i);
                SelectSize("Small");
                ClickAddToCartButton();
                WaitingProductAdd(i + 1);
            }
        }

        /// <summary>
        /// Удаляет указанное количество товаров из корзины.
        /// </summary>
        [Obsolete]
        private void RemoveProductFromCart(int quantityProducts)
        {
            GoToCart();

            for (int i = 0; i < quantityProducts; i++)
            {
                ClickRemoveButton();
                WaitingProductRemove();
            }
        }

        /// <summary>
        /// Открывает главную страницу.
        /// </summary>
        private void OpenHomePage()
        {
            driver.Url = baseUrl;
        }

        /// <summary>
        /// Открывает страницу товара с указанным порядковым номером на странице.
        /// </summary>
        /// <param name="index"></param>
        private void OpenProductPage(int index)
        {
            driver.FindElements(By.CssSelector("article.product-column"))[index].Click();
        }

        /// <summary>
        /// Выбирает размер товара из выпадающего списка если у продукта есть такой параметр, если параметра нет - ничего не делает.
        /// </summary>
        /// <param name="size"></param>
        private void SelectSize(string size)
        {
            if (IsElementPresent(By.Name("options[Size]")))
            {
                driver.FindElement(By.Name("options[Size]")).Click();
                new SelectElement(driver.FindElement(By.Name("options[Size]"))).SelectByValue(size);
            }
        }

        /// <summary>
        /// Нажимает на кнопку добавления товара в корзину на странице товара.
        /// </summary>
        private void ClickAddToCartButton()
        {
            driver.FindElement(By.Name("add_cart_product")).Click();
        }

        /// <summary>
        /// Ждет обновления значка счетчика добавленного в корзину товара.
        /// </summary>
        [Obsolete]
        private void WaitingProductAdd(int quantityProducts)
        {
            // Переменная с элементом счетчика добавленного в корзину товара
            IWebElement productBage = driver.FindElement(By.ClassName("badge"));
            // Ожидание обновления productBage
            wait.Until(ExpectedConditions.TextToBePresentInElement(productBage, quantityProducts.ToString()));
        }

        /// <summary>
        /// Переходит в корзину.
        /// </summary>
        [Obsolete]
        private void GoToCart()
        {
            // Переход в корзину
            driver.FindElement(By.Id("cart")).Click();
            // Ожидание появления содержимого корзины
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("data-table")));
        }

        /// <summary>
        /// Нажимает на кнопку удаления товара из корзины.
        /// </summary>
        private void ClickRemoveButton()
        {
            driver.FindElement(By.Name("remove_cart_item")).Click();
        }

        /// <summary>
        /// Ждет обновления списка товаров.
        /// </summary>
        [Obsolete]
        private void WaitingProductRemove()
        {
            // Переменная с элементом прелоадером
            IWebElement loader = driver.FindElement(By.ClassName("loader"));
            // Ожидание исчезновения loader
            wait.Until(ExpectedConditions.StalenessOf(loader));
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
