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
        public MyInfoPage _myInfoPage;
        public LeaveListPage _leaveListPage;
        public ApplyLeavePage _applyLeavePage;
        public AddLeaveEntitlementsPage _addLeaveEntitlementsPage;

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
            _myInfoPage = new MyInfoPage(_webDriver);
            _leaveListPage = new LeaveListPage(_webDriver);
            _applyLeavePage = new ApplyLeavePage(_webDriver);
            _addLeaveEntitlementsPage = new AddLeaveEntitlementsPage(_webDriver);
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
            _adminPage.searchSystemUsersViaUsernameTextbox.SendKeys(updatedUserDetails[1]);
            _adminPage.searchSystemUsersViaUsernameTextboxFocused.SendKeys(Keys.Return);
            _webDriver.WaitAndClick(() => _adminPage.editUserButton);
            wait.Until(d => _addOrEditUserPage.usernameTextbox.Displayed);
            string currentUserRole = _addOrEditUserPage.userRoleDropdown.Text;
            Assert.AreEqual($"User Role{Environment.NewLine}ESS", currentUserRole); //this and the following three asserts confirm that the user information was successfully updated
            string currentStatus = _addOrEditUserPage.statusDropdown.Text;
            Assert.AreEqual($"Status{Environment.NewLine}Disabled", currentStatus);
            string currentEmployeeName = _addOrEditUserPage.employeeNameTextbox.GetAttribute("value");
            Assert.AreEqual(updatedUserDetails[0], currentEmployeeName);
            _addOrEditUserPage.usernameTextbox.Click();
            string currentUsername = _addOrEditUserPage.usernameTextbox.GetAttribute("value");
            Assert.AreEqual(updatedUserDetails[1], currentUsername);
            _addOrEditUserPage.cancelChangesButton.Click();
            wait.Until(d => _adminPage.searchSystemUsersViaUsernameTextbox.Displayed);
            _adminPage.searchSystemUsersViaUsernameTextbox.SendKeys(updatedUserDetails[1]);
            _adminPage.searchSystemUsersViaUsernameTextboxFocused.SendKeys(Keys.Return);
            _webDriver.WaitAndClick(() => _adminPage.deleteUserButton);
            _webDriver.WaitAndClick(() => _adminPage.confirmDeleteUserButton);
            wait.Until(d => _adminPage.deleteUserSuccess.Displayed); //if message displays, user has been successfully deleted
        }
        [TestMethod]
        public void ChangeEmployeeName() //assignment test case is listed as "Change User Name" but asks to target the employee name, my method is named for clarity of function
        {
            _loginPage.Login();
            _persistentSidebar.sidebarButtonMyInfo.Click();
            string[] updatedEmployeeName = _myInfoPage.EditEmployeeName();
            _webDriver.Navigate().Refresh();
            string expectedEmployeeName = updatedEmployeeName[0] + " " + updatedEmployeeName[2];
            _webDriver.WaitAndClick(() => _persistentHeader.userOptionsDropdown);
            string actualEmployeeName = _webDriver.FindElement(By.XPath("//p[@class='oxd-userdropdown-name']")).Text;
            Assert.AreEqual(expectedEmployeeName, actualEmployeeName); //assert passing means the employee name is successfully displaying in the top right, completing this test case
        }
        [TestMethod]
        public void ApplyForLeave()
        {
            _loginPage.Login();
            _persistentSidebar.sidebarButtonLeave.Click();
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.Until(d => _leaveListPage.employeeNameTextbox.Displayed);
            bool isThereLeave = _applyLeavePage.CheckForLeave();
            if (isThereLeave == true)
            {
                _applyLeavePage.ApplyForLeave();
            }
            else
            {
                _addLeaveEntitlementsPage.AddLeaveEntitlement();
                _applyLeavePage.ApplyForLeave();
            }

        }
        [TestMethod]
        public void TestingMyMethods()
        {
            _loginPage.Login();
            _persistentSidebar.sidebarButtonLeave.Click();
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.Until(d => _leaveListPage.employeeNameTextbox.Displayed);
            _applyLeavePage.ApplyForLeave();
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
