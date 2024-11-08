using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QA_Capstone_Project
{
    public class AddUserPage
    {
        public IWebDriver _webDriver;

        public AddUserPage(IWebDriver driver)
        {
            _webDriver = driver;
        }
        public void AddUser()
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.Until(d => userRoleDropdown.Displayed);
            userRoleDropdown.Click();
            userRoleDropdownOptionAdmin.Click();
            statusDropdown.Click();
            statusDropdownOptionEnabled.Click();
            employeeNameTextbox.SendKeys("m");
            Thread.Sleep(3000);
            employeeNameTextbox.SendKeys(Keys.ArrowDown);
            employeeNameTextbox.SendKeys(Keys.Return);
            usernameTextbox.Click();
            usernameTextbox.SendKeys("iamanewuser");
            passwordTextbox.Click();
            passwordTextbox.SendKeys("AaNn3eWw^^^^");
            passwordTextbox.SendKeys(Keys.Tab);
            IWebElement currentElement = _webDriver.SwitchTo().ActiveElement();
            currentElement.SendKeys("AaNn3eWw^^^^");
            saveUserButton.Click();
        }
        public string addUserPageUrl = "https://opensource-demo.orangehrmlive.com/web/index.php/admin/saveSystemUser";
        public IWebElement userRoleDropdown => _webDriver.FindElement(By.XPath("//div[@class='oxd-input-group oxd-input-field-bottom-space' and contains(./descendant::*, 'User Role')]"));
        public IWebElement userRoleDropdownOptionAdmin => userRoleDropdown.FindElement(By.XPath("./descendant::*[@role='option' and contains(./*, 'Admin')]"));
        public IWebElement userRoleDropdownOptionESS => userRoleDropdown.FindElement(By.XPath("./descendant::*[@role='option' and contains(./*, 'ESS')]"));
        public IWebElement statusDropdown => _webDriver.FindElement(By.XPath("//div[@class='oxd-input-group oxd-input-field-bottom-space' and contains(./descendant::*, 'Status')]"));
        public IWebElement statusDropdownOptionEnabled => statusDropdown.FindElement(By.XPath("./descendant::*[@role='option' and contains(./*, 'Enabled')]"));
        public IWebElement statusDropdownOptionDisabled => statusDropdown.FindElement(By.XPath("./descendant::*[@role='option' and contains(./*, 'Disabled')]"));
        public IWebElement employeeNameTextbox => _webDriver.FindElement(By.XPath("//input[@placeholder='Type for hints...']"));
        //public IWebElement employeeNameTextboxAutocompleteOption => _webDriver.FindElement(By.XPath("//div[@role='option']")); couldn't get this element to work
        public IWebElement usernameTextbox => _webDriver.FindElement(By.XPath("//input[@class='oxd-input oxd-input--active' and contains(./ancestor::*, 'Username')]"));
        public IWebElement passwordTextbox => _webDriver.FindElement(By.XPath("//input[@type='password' and contains(./ancestor::*, 'Password')]"));
        //public IWebElement confirmPasswordTextbox => _webDriver.FindElement(By.XPath("//input[@type='password' and contains(./ancestor::*, 'Confirm Password')]")); could not figure out how to accurately target this and NOT put the password in the first box twice, approached differently
        public IWebElement saveUserButton => _webDriver.FindElement(By.XPath("//button[@type='submit']"));
    }
   
}
