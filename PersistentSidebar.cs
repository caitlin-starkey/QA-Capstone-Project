using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA_Capstone_Project
{
    public class PersistentSidebar
    {
        public IWebDriver _webDriver;

        public PersistentSidebar(IWebDriver driver)
        {
            _webDriver = driver;
        }
        public IWebElement sidebarButtonAdmin => _webDriver.FindElement(By.XPath("//a[@href='/web/index.php/admin/viewAdminModule']"));
        public IWebElement sidebarButtonMyInfo => _webDriver.FindElement(By.XPath("//a[@href='/web/index.php/pim/viewMyDetails']"));
    }
}
