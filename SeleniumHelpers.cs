using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA_Capstone_Project
{
        public class SeleniumHelpers
        {
            public IWebDriver _webDriver;
            public SeleniumHelpers(IWebDriver driver)
            {
                _webDriver = driver;
            }
            public void SelectTextFromDropdown(IWebElement element, string dropdownText)
            {
                SelectElement select = new SelectElement(element);
                select.SelectByText(dropdownText);
            }
            public void SelectValueFromDropdown(IWebElement element, string dropdownValue)
            {
                SelectElement select = new SelectElement(element);
                select.SelectByValue(dropdownValue);
            }
            public void SelectIndexFromDropdown(IWebElement element, int dropdownIndex)
            {
                SelectElement select = new SelectElement(element);
                select.SelectByIndex(dropdownIndex);
            }
            public void ScrollAndClickButton(IWebElement element)
            {
                Actions a = new Actions(_webDriver);
                a.ScrollToElement(element).Click(element).Perform();
            }
            public string CreateUsername()
            {
                var chars = "0123456789abcdefghijklmnoqqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var username = new StringBuilder();
                var random = new Random();
                for (int i = 0; i < 10; i++)
                {
                    username.Append(chars[random.Next(chars.Length)]);
                }
                return username.ToString();
            }
             public string CreatePassword()
             {
                var chars = "0123456789abcdefghijklmnoqqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%";
                var passwordgenerator = new StringBuilder();
                var random = new Random();
                for (int i = 0; i < 10; i++)
                {
                    passwordgenerator.Append(chars[random.Next(chars.Length)]);
                }
                string password = passwordgenerator.ToString();
                if (password.Any(char.IsDigit) && password.Any(char.IsSymbol))
                {
                    return password;
                }
                else
                {
                    password += "1!";
                    return password;
                }
             }
        }
}
