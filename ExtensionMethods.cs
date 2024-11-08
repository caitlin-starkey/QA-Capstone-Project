using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA_Capstone_Project
{
    public static class ExtensionMethods
    {
        public static void WaitAndClick(this IWebDriver _driver, Func<IWebElement> element)

        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => element().Displayed);
            element().Click();
        }
    }
}
