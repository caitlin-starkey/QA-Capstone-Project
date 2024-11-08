using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace QA_Capstone_Project
{
    public class LoginPage
    {
        public IWebDriver _webDriver;

        public LoginPage(IWebDriver driver)
        {
            _webDriver = driver;
        }
        public void Login()
        {
            _webDriver.Navigate().GoToUrl(loginPageUrl);
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.Until(d => usernameTextbox.Displayed);
            usernameTextbox.SendKeys("Admin");
            passwordTextbox.SendKeys("admin123");
            submitButton.Click();
            DashboardPage _dashboardPage;
            _dashboardPage = new DashboardPage(_webDriver);
            WebDriverWait wait1 = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait1.Until(d => _dashboardPage.dashboardHeader.Displayed);
            string expectedUrl = _dashboardPage.dashboardPageUrl;
            string actualUrl = _webDriver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }
        public string loginPageUrl = "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login";
        public IWebElement usernameTextbox => _webDriver.FindElement(By.XPath("//input[@name='username']"));
        public IWebElement passwordTextbox => _webDriver.FindElement(By.XPath("//input[@name='password']"));
        public IWebElement submitButton => _webDriver.FindElement(By.XPath("//button[@type='submit']"));
    }
    
}
