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
    }
    
}
