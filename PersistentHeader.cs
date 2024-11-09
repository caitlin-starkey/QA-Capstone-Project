using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA_Capstone_Project
{
    public class PersistentHeader
    {
        public IWebDriver _webDriver;

        public PersistentHeader(IWebDriver driver)
        {
            _webDriver = driver;
        }
        public IWebElement userOptionsDropdown => _webDriver.FindElement(By.XPath("//p[@class='oxd-userdropdown-name']"));
        public IWebElement userOptionsDropdownLogout => _webDriver.FindElement(By.XPath("//a[@role='menuitem' and contains(., 'Logout')]"));
    }
}
