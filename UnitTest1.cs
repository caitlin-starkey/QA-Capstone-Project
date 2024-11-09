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
        public AddOrEditUserPage _addOrEditUserPage;
        public PersistentHeader _persistentHeader;
        public PersistentSidebar _persistentSidebar;

        [TestInitialize]
        public void Setup()
        {
            _webDriver = new FirefoxDriver();
            _loginPage = new LoginPage(_webDriver);
            _dashboardPage = new DashboardPage(_webDriver);
            _adminPage = new AdminPage(_webDriver);
            _seleniumHelpers = new SeleniumHelpers(_webDriver);
            _addOrEditUserPage = new AddOrEditUserPage(_webDriver);
            _persistentHeader = new PersistentHeader(_webDriver);
            _persistentSidebar = new PersistentSidebar(_webDriver);
        }


        [TestMethod]
        public void AddNewUserAndLogin() 
        {
            _loginPage.Login();
            _persistentSidebar.sidebarButtonAdmin.Click();
            string expectedUrl = _adminPage.adminPageUrl;
            string actualUrl = _webDriver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
            _webDriver.WaitAndClick(() => _adminPage.addUserButton);
            string[] newUserDetails = _addOrEditUserPage.AddUser();
            string[] names = newUserDetails[0].Split(' '); //takes the employee's full name and splits it up
            var firstName = names.First();
            var lastName = names.Last();
            string expectedEmployeeName = firstName + " " + lastName;
            _persistentHeader.userOptionsDropdown.Click();
            _persistentHeader.userOptionsDropdownLogout.Click();
            _loginPage.LoginAddedUser(newUserDetails[1], newUserDetails[2]);
            _webDriver.WaitAndClick(() => _persistentHeader.userOptionsDropdown);
            string actualEmployeeName = _webDriver.FindElement(By.XPath("//p[@class='oxd-userdropdown-name']")).Text;
            Assert.AreEqual(expectedEmployeeName, actualEmployeeName); //assert passing means user has been successfully added and logged in with, completing this test case
        }
        [TestMethod]
        public void SearchAndEditUser()
        {
            _loginPage.Login(); //from here to second comment are a repeat of the AddNewUserAndLogin method
            _persistentSidebar.sidebarButtonAdmin.Click();
            string expectedUrl = _adminPage.adminPageUrl;
            string actualUrl = _webDriver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
            _webDriver.WaitAndClick(() => _adminPage.addUserButton);
            string[] newUserDetails = _addOrEditUserPage.AddUser();
            string[] names = newUserDetails[0].Split(' '); 
            var firstName = names.First();
            var lastName = names.Last();
            string expectedEmployeeName = firstName + " " + lastName;
            _persistentHeader.userOptionsDropdown.Click();
            _persistentHeader.userOptionsDropdownLogout.Click();
            _loginPage.LoginAddedUser(newUserDetails[1], newUserDetails[2]);
            _webDriver.WaitAndClick(() => _persistentHeader.userOptionsDropdown);
            string actualEmployeeName = _webDriver.FindElement(By.XPath("//p[@class='oxd-userdropdown-name']")).Text;
            Assert.AreEqual(expectedEmployeeName, actualEmployeeName); //end of AddNewUserAndLogin method
            _persistentHeader.userOptionsDropdown.Click();
            _persistentHeader.userOptionsDropdownLogout.Click();
            _loginPage.Login();
            _webDriver.WaitAndClick(() => _persistentSidebar.sidebarButtonAdmin);
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.Until(d => _adminPage.searchSystemUsersViaUsernameTextbox.Displayed);
            _adminPage.searchSystemUsersViaUsernameTextbox.SendKeys(newUserDetails[1]);
            _adminPage.searchSystemUsersViaUsernameTextboxFocused.SendKeys(Keys.Return);
            _webDriver.WaitAndClick(() => _adminPage.editUserButton);
            string[] updatedUserDetails = _addOrEditUserPage.EditUser();



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
