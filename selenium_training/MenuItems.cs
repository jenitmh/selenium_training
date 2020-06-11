using System;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTraining
{
    [TestFixture]
    public class MenuItems
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
        public void MenuItem()
        {
            Login();
            FindMenuItem();
        }

        private void Login()
        {
            driver.Url = baseUrl + "admin/login.php";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin" + Keys.Enter);
        }

        /// <summary>
        /// Прокликивает все пункты и подпункты меню
        /// </summary>
        private void FindMenuItem()
        {
            // Пемеренная с количеством пунктов меню на странице
            int quantityMenu = driver.FindElements(By.CssSelector("li.app")).Count;

            // Перебор всех пунктов меню
            for (int m = 1; m <= quantityMenu; m++)
            {
                // Клик по пункту меню
                driver.FindElement(By.CssSelector(".app:nth-child(" + m + ")")).Click();
                HeaderAvailable();

                // Пемеренная с количеством пунктов подменю
                int quantitySub = driver.FindElements(By.CssSelector("li.doc")).Count;

                // Определение наличия пунктов подменю
                if (IsElementPresent(By.CssSelector("li.doc")))
                {
                    // Перебор всех пунктов подменю
                    for (int s = 1; s <= quantitySub; s++)
                    {
                        // Клик по пункту меню
                        driver.FindElement(By.CssSelector(".doc:nth-child(" + s + ")")).Click();
                        HeaderAvailable();
                    }
                }
            }
            Assert.True(quantityMenu > 0, "Нет пунктов меню");
        }

        /// <summary>
        /// Проверяет наличие заголовка на странице
        /// </summary>
        private void HeaderAvailable()
        {
            Assert.True(IsElementPresent(By.CssSelector("div.panel-heading")), "У пункта меню нет заголовка"); // Проверка пункта меню на наличие заголовка
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