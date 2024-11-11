using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class AddLeaveEntitlementsPage
    {
        public IWebDriver _webDriver;

        public AddLeaveEntitlementsPage(IWebDriver driver)
        {
            _webDriver = driver;
        }
        public void AddLeaveEntitlement()
        {
            leaveEntitlementsHeaderDropdown.Click();
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            _webDriver.WaitAndClick(() => leaveEntitlementsHeaderDropdownAddEntitlements);
            wait.Until(d => addToIndividualEmployeeBubble.Displayed);
            string expectedUrl = addLeaveEntitlementsUrl;
            string actualUrl = _webDriver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
            string employeeName = _webDriver.FindElement(By.XPath("//p[@class='oxd-userdropdown-name']")).Text;
            employeeNameTextbox.SendKeys(employeeName);
            Thread.Sleep(3000); //waiting for options to populate
            employeeNameTextbox.SendKeys(Keys.ArrowDown);
            employeeNameTextbox.SendKeys(Keys.Return);
            leaveTypeDropdown.Click();
            leaveTypeFMLAChoice.Click();
            entitlementTextbox.SendKeys("50.00");
            saveButton.Click();
            wait.Until(d => confirmSaveButton.Displayed);
            confirmSaveButton.Click();
            wait.Until(d => addEntitlementSuccess.Displayed);
        }

        public string addLeaveEntitlementsUrl = "https://opensource-demo.orangehrmlive.com/web/index.php/leave/addLeaveEntitlement";
        public IWebElement leaveEntitlementsHeaderDropdown => _webDriver.FindElement(By.XPath("//span[@class='oxd-topbar-body-nav-tab-item' and contains(., 'Entitlements')]"));
        public IWebElement leaveEntitlementsHeaderDropdownAddEntitlements => _webDriver.FindElement(By.XPath("//a[@role='menuitem' and contains(., 'Add Entitlements')]"));
        public IWebElement addToIndividualEmployeeBubble => _webDriver.FindElement(By.XPath("//span[@class='oxd-radio-input oxd-radio-input--active --label-right oxd-radio-input' and contains(.., 'Individual Employee')]"));
        public IWebElement employeeNameTextbox => _webDriver.FindElement(By.XPath("//input[@placeholder='Type for hints...']"));
        public IWebElement leaveTypeDropdown => _webDriver.FindElement(By.XPath("//div[@class='oxd-select-text-input' and contains(./ancestor::*, 'Leave Type')]"));
        public IWebElement leaveTypeFMLAChoice => _webDriver.FindElement(By.XPath("//div[@class='oxd-select-option' and contains(./*, 'CAN - FMLA')]"));
        public IWebElement entitlementTextbox => _webDriver.FindElement(By.XPath("//input[@class='oxd-input oxd-input--active' and not(@placeholder='Search')]"));
        //public IWebElement entitlementTextboxFocused => _webDriver.FindElement(By.XPath("//input[@class='oxd-input oxd-input--focus' and not(., @placeholder='Search')]")); unneeded helper element
        public IWebElement saveButton => _webDriver.FindElement(By.XPath("//button[@type='submit']"));
        public IWebElement confirmSaveButton => _webDriver.FindElement(By.XPath("//button[@type='button' and contains(., 'Confirm')]"));
        public IWebElement addEntitlementSuccess => _webDriver.FindElement(By.XPath("//p[@class='oxd-text oxd-text--p oxd-text--toast-message oxd-toast-content-text' and contains(., 'Successfully Saved')]"));
    }
}
