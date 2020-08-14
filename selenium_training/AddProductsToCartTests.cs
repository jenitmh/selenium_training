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
            AddProductsToCart(3);
            GoToCart();
            RemoveProducts();
        }

        /// <summary>
        /// Добавляет указанное количество товаров в корзину.
        /// </summary>
        /// <param name="quantity"></param>
        private void AddProductsToCart(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                OpenHomePage();
                OpenProductPage(0);
                SelectSize("Small");
                AddToCart();
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
            // Переменная с элементом значка количества товаров в корзине
            IWebElement bage = driver.FindElement(By.ClassName("badge"));
            // Переменная с количеством товаров в корзине, отображаемом на значке bage
            int text = int.Parse(driver.FindElement(By.ClassName("badge")).Text);

            // Добавление товара в корзину
            driver.FindElement(By.Name("add_cart_product")).Click();
            // Добавление в переменную к количеству товаров в корзине + 1
            text += 1;
            // Ожидание появления количества товара на значке bage + 1
            wait.Until(ExpectedConditions.TextToBePresentInElement(bage, text.ToString()));
        }

        /// <summary>
        /// Выбирает размер продукта из выпадающего списка если у продукта есть такой параметр, если параметра нет - ничего не делает (аргументы: 's' - Small, 'm' - Medium, 'l' - Large).
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
