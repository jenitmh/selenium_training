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
        public void AddProductsToCartTest()
        {
            for (int i = 0; i < 4; i++)
            {
                OpenHomePage();
                OpenProductPage(0);
                SelectSize('m');
                AddToCart();
            }

            OpenHomePage();
            OpenProductPage(1);
            SelectSize('l');
            AddToCart();

            OpenHomePage();
            OpenProductPage(4);
            SelectSize('s');
            AddToCart();

            GoToCart();
            RemoveProducts();
        }

        /// <summary>
        /// Открывает главную страницу.
        /// </summary>
        private void OpenHomePage()
        {
            driver.Url = baseUrl;
        }

        /// <summary>
        /// Удаляет последовательно все товары из корзины.
        /// </summary>
        private void RemoveProducts()
        {
            // Условие, что текущая страница является страницей корзины
            if (IsElementPresent(By.ClassName("box")) && driver.Url == "http://localhost/litecart/checkout")
            {
                // Переменная с количеством товаров в корзине
                int quantityProducts = driver.FindElements(By.ClassName("item")).Count;

                // Перебор товаров в корзине
                for (int i = 0; i < quantityProducts; i++)
                {
                    // Удаление товара из корзины
                    driver.FindElement(By.Name("remove_cart_item")).Click();
                    IsElementPresent(By.Id("box - checkout - summary"));
                }
            }
        }

        /// <summary>
        /// Удаляет один товар из корзины (в качестве аргумента указывается порядковый номер товара в корзине).
        /// </summary>
        /// <param name="index"></param>
        private void RemoveProducts(int index)
        {
            // Условие, что текущая страница является страницей корзины
            if (IsElementPresent(By.ClassName("box")) && driver.Url == "http://localhost/litecart/checkout")
            {
                // Переменная со списком товаров в корзине
                var productList = driver.FindElements(By.ClassName("item"));
                // Переменная с количеством товаров в корзине до удаления
                int quantityProductsOld = driver.FindElements(By.ClassName("item")).Count;

                // Удаление товара из корзины с порядковым номером переданным методу аргументом
                productList[index].FindElement(By.Name("remove_cart_item")).Click(); // -- Добавить ожидание обновления таблицы после удаления товара

                // Переменная с количеством товаров в корзине после удаления
                int quantityProductsNew = driver.FindElements(By.ClassName("item")).Count;

                // Проверка того, что в корзине стало на 1 товар меньше
                quantityProductsOld -= 1;
                Assert.AreEqual(quantityProductsNew, quantityProductsOld);
            }
        }

        /// <summary>
        /// Переходит в корзину.
        /// </summary>
        private void GoToCart()
        {
            driver.FindElement(By.Id("cart")).Click();
        }

        /// <summary>
        /// Добавляет продукт в корзину.
        /// </summary>
        [Obsolete]
        private void AddToCart()
        {
            // Переменная с текстом элемента (значек с количеством товаров в корзине)
            string badgeOld = driver.FindElement(By.ClassName("badge")).Text;

            // Условие, что количество товаров в корзине больше нуля
            if (badgeOld != "")
            {
                // Переменная с количеством товаров в корзине до добавления
                int badgeQuantityOld = int.Parse(badgeOld);

                // Добавление товара в корзину
                driver.FindElement(By.Name("add_cart_product")).Click();
                // Ожидание добавления товара в корзину
                Thread.Sleep(1000);
                // Переменная с количеством товаров в корзине после добавления
                int badgeQuantityNew = int.Parse(driver.FindElement(By.ClassName("badge")).Text);

                // Проверка того, что в корзине стало на 1 товар больше
                badgeQuantityOld += 1;
                Assert.AreEqual(badgeQuantityOld, badgeQuantityNew);
            }
            // Условие, что в корзине нет товаров
            if (badgeOld == "")
            {
                // Добавление товара в корзину
                driver.FindElement(By.Name("add_cart_product")).Click();

                // Ожидание добавления товара в корзину (появление значка на иконке корзины)
                wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("badge")));
                // Переменная с количеством товаров в корзине после добавления
                string badgeNew = driver.FindElement(By.ClassName("badge")).Text;

                // Проверка того, что в корзине стало на 1 товар больше
                badgeOld += 1;
                Assert.AreEqual(badgeNew, badgeOld);
            }
        }

        /// <summary>
        /// Выбирает размер продукта из выпадающего списка если у продукта есть такой параметр, если параметра нет - ничего не делает (аргументы: 's' - Small, 'm' - Medium, 'l' - Large).
        /// </summary>
        /// <param name="size"></param>
        private void SelectSize(char size)
        {
            if (IsElementPresent(By.Name("options[Size]")))
            {
                driver.FindElement(By.Name("options[Size]")).Click();
                if (size == 's')
                {
                    new SelectElement(driver.FindElement(By.Name("options[Size]"))).SelectByValue("Small");
                }
                else if (size == 'm')
                {
                    new SelectElement(driver.FindElement(By.Name("options[Size]"))).SelectByValue("Medium");
                }
                else if (size == 'l')
                {
                    new SelectElement(driver.FindElement(By.Name("options[Size]"))).SelectByValue("Large");
                }
            }
        }

        /// <summary>
        /// Открывает страницу продукта.
        /// </summary>
        /// <param name="index"></param>
        private void OpenProductPage(int index)
        {
            driver.FindElements(By.CssSelector("article.product-column"))[index].Click();
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
