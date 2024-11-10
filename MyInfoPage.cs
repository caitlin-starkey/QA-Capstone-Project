using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace QA_Capstone_Project
{
    public class MyInfoPage
    {
        public IWebDriver _webDriver;

        public MyInfoPage(IWebDriver driver)
        {
            _webDriver = driver;
        }
        public string[] EditEmployeeName()
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.Until(d => firstNameTextbox.Displayed);
            SeleniumHelpers _seleniumHelpers;
            _seleniumHelpers = new SeleniumHelpers(_webDriver);
            string[] updatedEmployeeName = new string[3];
            updatedEmployeeName[0] = _seleniumHelpers.CreateUsername(); //new first name
            updatedEmployeeName[1] = _seleniumHelpers.CreateUsername(); //new middle name
            updatedEmployeeName[2] = _seleniumHelpers.CreateUsername(); //new last name
            Thread.Sleep(1000); //there is some kind of temporary element blocking the web elements I want to target on the page if the browser doesn't wait a little. haven't been able to find it to do anything about it other than a hard stop for 1 second.
            firstNameTextbox.Click();
            firstNameTextboxFocused.SendKeys(Keys.Control + "a");
            firstNameTextboxFocused.SendKeys(Keys.Delete);
            firstNameTextboxFocused.SendKeys(updatedEmployeeName[0]);
            middleNameTextbox.Click();
            middleNameTextboxFocused.SendKeys(Keys.Control + "a");
            middleNameTextboxFocused.SendKeys(Keys.Delete);
            middleNameTextboxFocused.SendKeys(updatedEmployeeName[1]);
            lastNameTextbox.Click();
            lastNameTextboxFocused.SendKeys(Keys.Control + "a");
            lastNameTextboxFocused.SendKeys(Keys.Delete);
            lastNameTextboxFocused.SendKeys(updatedEmployeeName[2]);
            savePersonalDetailsButton.Click();
            wait.Until(d => personalDetailsUpdatedSuccess.Displayed);
            return updatedEmployeeName;
        }
        public IWebElement firstNameTextbox => _webDriver.FindElement(By.XPath("//input[@class='oxd-input oxd-input--active orangehrm-firstname']"));
        public IWebElement firstNameTextboxFocused => _webDriver.FindElement(By.XPath("//input[@name='firstName']"));
        public IWebElement middleNameTextbox => _webDriver.FindElement(By.XPath("//input[@class='oxd-input oxd-input--active orangehrm-middlename']"));
        public IWebElement middleNameTextboxFocused => _webDriver.FindElement(By.XPath("//input[@name='middleName']"));
        public IWebElement lastNameTextbox => _webDriver.FindElement(By.XPath("//input[@class='oxd-input oxd-input--active orangehrm-lastname']"));
        public IWebElement lastNameTextboxFocused => _webDriver.FindElement(By.XPath("//input[@name='lastName']"));
        public IWebElement savePersonalDetailsButton => _webDriver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary orangehrm-left-space' and contains(./ancestor::*, '* Required')]"));
        public IWebElement personalDetailsUpdatedSuccess => _webDriver.FindElement(By.XPath("//p[@class='oxd-text oxd-text--p oxd-text--toast-message oxd-toast-content-text' and contains(., 'Successfully Updated')]"));
    }
}
