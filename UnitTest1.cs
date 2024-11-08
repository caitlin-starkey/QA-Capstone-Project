using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace QA_Capstone_Project
{
    [TestClass]
    public class UnitTest1
    {
        public IWebDriver _webDriver;
        public LoginPage _loginPage;
        public DashboardPage _dashboardPage;
        public AdminPage _adminPage;
        public SeleniumHelpers _seleniumHelpers;
        public AddUserPage _addUserPage;

        [TestInitialize]
        public void Setup()
        {
            _webDriver = new FirefoxDriver();
            _loginPage = new LoginPage(_webDriver);
            _dashboardPage = new DashboardPage(_webDriver);
            _adminPage = new AdminPage(_webDriver);
            _seleniumHelpers = new SeleniumHelpers(_webDriver);
            _addUserPage = new AddUserPage(_webDriver);
        }

        //[TestMethod]
       // public void SearchAndEditUser()
        //{
            //_loginPage.Login();
        //}
        [TestMethod]
        public void AddNewUserAndLogin() 
        {
            _loginPage.Login();
            _dashboardPage.sidebarButtonAdmin.Click();
            string expectedUrl = _adminPage.adminPageUrl;
            string actualUrl = _webDriver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
            _webDriver.WaitAndClick(() => _adminPage.addUserButton);
            _addUserPage.AddUser();


        }

        [TestCleanup]
        public void Cleanup()
        {
            Thread.Sleep(3000);
            _webDriver.Quit();
            _webDriver.Dispose();
        }
    }
}
