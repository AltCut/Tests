using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    class ClassTest
    {
        IWebDriver driver;
        private String today;
        private String week;

        [SetUp]
        public void startBrowser()
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;

            var path = Path.GetDirectoryName(location);
            driver = new ChromeDriver(path);
            driver.Url = "https://www.booking.com/index.ru.html";
        }

        [Test]
        public void test()
        {

            IWebElement element = driver.FindElement(By.XPath("/html/body/div[1]/div/div/ul/li[2]/a"));
            element.Click();
            element = driver.FindElement(By.XPath("/html/body/div[1]/div/div/ul/li[2]/div/div[2]/div/div[1]/div/ul[2]/li[1]/a/span[2]"));
            element.Click();

            element = driver.FindElement(By.XPath("/html/body/div[1]/div/div/ul/li[1]/a"));
            element.Click();
            System.Threading.Thread.Sleep(1000);
            element = driver.FindElement(By.CssSelector("#currency_dropdown_top > ul:nth-child(2) > li.currency_GBP > a"));
            element.Click();

            System.Threading.Thread.Sleep(3000);
            driver.Url = "https://booking.kayak.com/";

            System.Threading.Thread.Sleep(3000);
            driver.Url = "https://account.booking.com/sign-in";

            element = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div[1]/div[2]/div/div/div/form/div[1]/div/div/div/input"));
            element.SendKeys("thealtcut@gmail.com");

            element = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div[1]/div[2]/div/div/div/form/div[3]/button"));
            element.Click();

            System.Threading.Thread.Sleep(1000);
            element = driver.FindElement(By.CssSelector("#password"));
            element.SendKeys("rg%jXs7s%/VBzKn");

            element = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div[1]/div[2]/div/div/div/form/button"));
            element.Click();

            System.Threading.Thread.Sleep(1000);
            element = driver.FindElement(By.CssSelector("#current_account > a"));
            element.Click();

            System.Threading.Thread.Sleep(1000);
            element = driver.FindElement(By.CssSelector("#profile-menu > div.profile-menu__item.profile_menu__item--mydashboard > a"));
            element.Click();

            System.Threading.Thread.Sleep(1000);
            element = driver.FindElement(By.CssSelector("#logo_no_globe_new_logo"));
            element.Click();




        }

        [Test]
        public void test1()
        {
            driver.Url = "https://www.booking.com/index.ru.html";
            IWebElement element = driver.FindElement(By.CssSelector("#ss"));
            element.SendKeys("Milan");

            System.Threading.Thread.Sleep(1000);
            element = driver.FindElement(By.CssSelector("#frm > div.xp__fieldset.accommodation > div.xp__input-group.xp__search > div:nth-child(6) > div.c-autocomplete.sb-destination.region_second_line > ul.c-autocomplete__list.sb-autocomplete__list.sb-autocomplete__list-with_photos.-visible > li:nth-child(1)"));
            element.Click();

            today = Convert.ToString(DateTime.Today.Day);

            week = Convert.ToString(int.Parse(today) + 7);

            System.Threading.Thread.Sleep(1000);
            IWebElement dateWidgetFrom = driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/form/div[1]/div[2]/div[2]/div/div/div[3]/div[1]/table"));
            IList<IWebElement> columns = dateWidgetFrom.FindElements(By.TagName("td"));

            foreach (IWebElement cell in columns)
            {
                if (cell.Text.Equals(today))
                {
                    cell.Click();
                    break;
                }
            }

            System.Threading.Thread.Sleep(2000);
            IWebElement dateWidgetTo = driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/form/div[1]/div[2]/div[2]/div/div/div[3]/div[1]/table"));
            IList<IWebElement> columns1 = dateWidgetTo.FindElements(By.TagName("td"));

            foreach (IWebElement cell in columns1)
            {
                if (cell.Text.Equals(week))
                {
                    cell.Click();
                    break;
                }
            }


            element = driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/form/div[1]/div[3]/label/span[2]"));
            element.Click();

            element = driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/form/div[1]/div[3]/div[2]/div/div/div[2]/div/div[2]/button[2]"));
            element.Click();

            element = driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/form/div[1]/div[4]/div[2]/button"));
            element.Click();
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }
}
