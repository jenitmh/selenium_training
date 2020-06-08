using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class UnitTest3
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void FirstTest()
        {
            Login();

            Appearance();
            Catalog();
            Countries();
            Currencies();
            Customers();
            Geo_zones();
            Languages();
            Modules();
            Orders();
            Pages();
            Reports();
            Slides();
            Settings();
            Tax();
            Translations();
            Users();
            Vqmods();
        }

        private void Vqmods()
        {
            driver.FindElement(By.CssSelector("[data-code=vqmods]")).Click();
        }

        private void Users()
        {
            driver.FindElement(By.CssSelector("[data-code=users]")).Click();
        }

        private void Translations()
        {
            driver.FindElement(By.CssSelector("[data-code=translations]")).Click();
            driver.FindElement(By.CssSelector("[data-code=search]")).Click();
            driver.FindElement(By.CssSelector("[data-code=scan]")).Click();
            driver.FindElement(By.CssSelector("[data-code=csv]")).Click();
        }

        private void Tax()
        {
            driver.FindElement(By.CssSelector("[data-code=tax]")).Click();
            driver.FindElement(By.CssSelector("[data-code=tax_rates]")).Click();
            driver.FindElement(By.CssSelector("[data-code=tax_classes]")).Click();
        }

        private void Slides()
        {
            driver.FindElement(By.CssSelector("[data-code=slides]")).Click();
        }

        private void Settings()
        {
            driver.FindElement(By.CssSelector("[data-code=settings]")).Click();
            driver.FindElement(By.CssSelector("[data-code=store_info]")).Click();
            driver.FindElement(By.CssSelector("[data-code=defaults]")).Click();
            driver.FindElement(By.CssSelector("[data-code=email]")).Click();
            driver.FindElement(By.CssSelector("[data-code=listings]")).Click();
            driver.FindElement(By.CssSelector("[data-code=legal]")).Click();
            driver.FindElement(By.CssSelector("[data-code=images]")).Click();
            driver.FindElement(By.CssSelector("[data-code=checkout]")).Click();
            driver.FindElement(By.CssSelector("[data-code=advanced]")).Click();
            driver.FindElement(By.CssSelector("[data-code=security]")).Click();
        }

        private void Reports()
        {
            driver.FindElement(By.CssSelector("[data-code=reports]")).Click();
            driver.FindElement(By.CssSelector("[data-code=monthly_sales]")).Click();
            driver.FindElement(By.CssSelector("[data-code=most_sold_products]")).Click();
            driver.FindElement(By.CssSelector("[data-code=most_shopping_customers]")).Click();
        }

        private void Pages()
        {
            driver.FindElement(By.CssSelector("[data-code=pages]")).Click();
            driver.FindElement(By.CssSelector("[data-code=csv]")).Click();
        }

        private void Orders()
        {
            driver.FindElement(By.CssSelector("[data-code=orders]")).Click();
            driver.FindElement(By.CssSelector("[data-code=order_statuses]")).Click();
        }

        private void Modules()
        {
            driver.FindElement(By.CssSelector("[data-code=modules]")).Click();
            driver.FindElement(By.CssSelector("[data-code=customer]")).Click();
            driver.FindElement(By.CssSelector("[data-code=shipping]")).Click();
            driver.FindElement(By.CssSelector("[data-code=payment]")).Click();
            driver.FindElement(By.CssSelector("[data-code=order]")).Click();
            driver.FindElement(By.CssSelector("[data-code=order_total]")).Click();
            driver.FindElement(By.CssSelector("[data-code=jobs]")).Click();
        }

        private void Languages()
        {
            driver.FindElement(By.CssSelector("[data-code=languages]")).Click();
            driver.FindElement(By.CssSelector("[data-code=storage_encoding]")).Click();
        }

        private void Geo_zones()
        {
            driver.FindElement(By.CssSelector("[data-code=geo_zones]")).Click();
        }

        private void Customers()
        {
            driver.FindElement(By.CssSelector("[data-code=customers]")).Click();
            driver.FindElement(By.CssSelector("[data-code=csv]")).Click();
            driver.FindElement(By.CssSelector("[data-code=newsletter]")).Click();
        }

        private void Currencies()
        {
            driver.FindElement(By.CssSelector("[data-code=currencies]")).Click();
        }

        private void Countries()
        {
            driver.FindElement(By.CssSelector("[data-code=countries]")).Click();
        }

        private void Appearance()
        {
            driver.FindElement(By.CssSelector("[data-code=appearance]")).Click();
            driver.FindElement(By.CssSelector("[data-code=template]")).Click();
            driver.FindElement(By.CssSelector("[data-code=logotype]")).Click();
        }

        private void Catalog()
        {
            string[] section =
            {
                "[data-code=catalog]",
                "[data-code=attribute_groups]",
                "[data-code=option_groups]",
                "[data-code=manufacturers]",
                "[data-code=suppliers]",
                "[data-code=delivery_statuses]",
                "[data-code=sold_out_statuses]",
                "[data-code=quantity_units]",
                "[data-code=csv]"
            };

            foreach (string i in section)
            {
                driver.FindElement(By.CssSelector(i)).Click();
            }

            /*for (int i = 0; i < section.Length; i++)
            {
                driver.FindElement(By.CssSelector(section[i])).Click();
            }*/
        }

        private void Login()
        {
            driver.Url = "http://localhost/litecart/admin/login.php";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin" + Keys.Enter);
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}