using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Linq;

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
        public PersistentHeader _persistentHeader;

        [TestInitialize]
        public void Setup()
        {
            _webDriver = new FirefoxDriver();
            _loginPage = new LoginPage(_webDriver);
            _dashboardPage = new DashboardPage(_webDriver);
            _adminPage = new AdminPage(_webDriver);
            _seleniumHelpers = new SeleniumHelpers(_webDriver);
            _addUserPage = new AddUserPage(_webDriver);
            _persistentHeader = new PersistentHeader(_webDriver);
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
            string[] newUserDetails = _addUserPage.AddUser();
            string[] names = newUserDetails[0].Split(' '); //takes the employee's full name and splits it up
            var firstName = names.First();
            var lastName = names.Last();
            string expectedEmployeeName = firstName + " " + lastName;
            _persistentHeader.userOptionsDropdown.Click();
            _persistentHeader.userOptionsDropdownLogout.Click();
            _loginPage.LoginAddedUser(newUserDetails[1], newUserDetails[2]);
            _webDriver.WaitAndClick(() => _persistentHeader.userOptionsDropdown);
            string actualEmployeeName = _webDriver.FindElement(By.XPath("//p[@class='oxd-userdropdown-name']")).Text;
            Assert.AreEqual(expectedEmployeeName, actualEmployeeName);
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
