using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA_Capstone_Project
{
    public class AdminPage
    {
        public IWebDriver _webDriver;

        public AdminPage(IWebDriver driver)
        {
            _webDriver = driver;
        }

        public string adminPageUrl = "https://opensource-demo.orangehrmlive.com/web/index.php/admin/viewSystemUsers";
        public IWebElement addUserButton => _webDriver.FindElement(By.XPath("//button[contains(., 'Add')]"));
        public IWebElement searchSystemUsersViaUsernameTextbox => _webDriver.FindElement(By.XPath("//input[@class='oxd-input oxd-input--active' and not(contains(@placeholder, 'Search'))]"));
        public IWebElement searchSystemUsersViaUsernameTextboxFocused => _webDriver.FindElement(By.XPath("//input[@class='oxd-input oxd-input--focus' and not(contains(@placeholder, 'Search'))]"));
        public IWebElement editUserButton => _webDriver.FindElement(By.XPath("//i[@class='oxd-icon bi-pencil-fill']"));
        public IWebElement deleteUserButton => _webDriver.FindElement(By.XPath("//i[@class='oxd-icon bi-trash']"));
        public IWebElement confirmDeleteUserButton => _webDriver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--label-danger orangehrm-button-margin' and contains(., 'Yes, Delete')]"));
        public IWebElement deleteUserSuccess => _webDriver.FindElement(By.XPath("//p[@class='oxd-text oxd-text--p oxd-text--toast-message oxd-toast-content-text' and contains(., 'Successfully Deleted')]"));
    }
    
}
