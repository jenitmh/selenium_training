using System;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace selenium_training
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
            baseUrl = "http://localhost/litecart";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
        }

        [Test]
        public void MenuItem()
        {
            Login();
            FindMenuItem();
            
            //ClickMenuItem();

            /*Appearance();
            HeaderAvailable();

            Catalog();
            HeaderAvailable();

            Countries();
            HeaderAvailable();

            Currencies();
            HeaderAvailable();

            Customers();
            HeaderAvailable();

            Geo_zones();
            HeaderAvailable();

            Languages();
            HeaderAvailable();
            
            Modules();
            HeaderAvailable();

            Orders();
            HeaderAvailable();

            Pages();
            HeaderAvailable();

            Reports();
            HeaderAvailable();

            Slides();
            HeaderAvailable();

            Settings();
            HeaderAvailable();
            
            Tax();
            HeaderAvailable();

            Translations();
            HeaderAvailable();

            Users();
            HeaderAvailable();

            Vqmods();
            HeaderAvailable();*/
        }

        private void ClickMenuItem()
        {
            var MenuItemsList = driver.FindElements(By.CssSelector("li.app"));

            for (int i = 0; i < MenuItemsList.Count; i++)
            {
                MenuItemsList[i].Click();
            }
        }

        private void Login()
        {
            driver.Url = baseUrl + "/admin/login.php";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin" + Keys.Enter);
        }

        private void Appearance()
        {
            string[] section =
            {
                "[data-code=appearance]",
                "[data-code=template]",
                "[data-code=logotype]"
            };

            foreach (string i in section)
            {
                driver.FindElement(By.CssSelector(i)).Click();
            }
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
        }

        private void FindMenuItem()
        {
            int sizeMenu = driver.FindElements(By.CssSelector("li.app")).Count;
            IWebElement[] sectionsMenu = new IWebElement[sizeMenu];

            for (int m = 0; m < sectionsMenu.Length; m++)
            {
                sectionsMenu[m] = driver.FindElement(By.CssSelector(".app:nth-child(" + (m + 1) + ")"));
                sectionsMenu[m].Click();

                int sizeSub = driver.FindElements(By.CssSelector("li.doc")).Count;
                IWebElement[] sectionsSub = new IWebElement[sizeSub];

                if (IsElementPresent(By.CssSelector("li.doc")))
                {
                    for (int s = 0; s < sectionsSub.Length; s++)
                    {
                        sectionsSub[s] = driver.FindElement(By.CssSelector(".doc:nth-child(" + (s + 1) + ")"));
                        sectionsSub[s].Click();
                    }
                }
            }
        }

        private void Countries()
        {
            driver.FindElement(By.CssSelector("[data-code=countries]")).Click();
        }

        private void Currencies()
        {
            driver.FindElement(By.CssSelector("[data-code=currencies]")).Click();
        }

        private void Customers()
        {
            string[] section =
            {
                "[data-code=customers]",
                "[data-code=csv]",
                "[data-code=newsletter]"
            };

            foreach (string i in section)
            {
                driver.FindElement(By.CssSelector(i)).Click();
            }
        }

        private void Geo_zones()
        {
            driver.FindElement(By.CssSelector("[data-code=geo_zones]")).Click();
        }

        private void Languages()
        {
            string[] section =
            {
                "[data-code=languages]",
                "[data-code=storage_encoding]"
            };

            foreach ( string i in section)
            {
                driver.FindElement(By.CssSelector(i)).Click();
            }
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

        private void Orders()
        {
            driver.FindElement(By.CssSelector("[data-code=orders]")).Click();
            driver.FindElement(By.CssSelector("[data-code=order_statuses]")).Click();
        }

        private void Pages()
        {
            driver.FindElement(By.CssSelector("[data-code=pages]")).Click();
            driver.FindElement(By.CssSelector("[data-code=csv]")).Click();
        }

        private void Reports()
        {
            driver.FindElement(By.CssSelector("[data-code=reports]")).Click();
            driver.FindElement(By.CssSelector("[data-code=monthly_sales]")).Click();
            driver.FindElement(By.CssSelector("[data-code=most_sold_products]")).Click();
            driver.FindElement(By.CssSelector("[data-code=most_shopping_customers]")).Click();
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

        private void Tax()
        {
            driver.FindElement(By.CssSelector("[data-code=tax]")).Click();
            driver.FindElement(By.CssSelector("[data-code=tax_rates]")).Click();
            driver.FindElement(By.CssSelector("[data-code=tax_classes]")).Click();
        }

        private void Translations()
        {
            driver.FindElement(By.CssSelector("[data-code=translations]")).Click();
            driver.FindElement(By.CssSelector("[data-code=search]")).Click();
            driver.FindElement(By.CssSelector("[data-code=scan]")).Click();
            driver.FindElement(By.CssSelector("[data-code=csv]")).Click();
        }

        private void Users()
        {
            driver.FindElement(By.CssSelector("[data-code=users]")).Click();
        }

        private void Vqmods()
        {
            driver.FindElement(By.CssSelector("[data-code=vqmods]")).Click();
        }

        private void HeaderAvailable()
        {
            IsElementPresent(By.CssSelector("div.panel-heading"));
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