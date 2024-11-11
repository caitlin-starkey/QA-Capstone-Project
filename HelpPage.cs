using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA_Capstone_Project
{
    public class HelpPage
    {
        public IWebDriver _webDriver;

        public HelpPage(IWebDriver driver)
        {
            _webDriver = driver;
        }

        public string helpPageUrl = "https://starterhelp.orangehrm.com/hc/en-us";

        public IWebElement signInButton => _webDriver.FindElement(By.XPath("//a[@class='sign-in']"));
        public IWebElement searchBar => _webDriver.FindElement(By.XPath("//input[@type='search']"));

        public IWebElement adminUserGuideButton => _webDriver.FindElement(By.XPath("//li[@class='blocks-item' and contains(., 'Admin User Guide')]"));
        public IWebElement employeeUserGuideButton => _webDriver.FindElement(By.XPath("//li[@class='blocks-item' and contains(., 'Employee User Guide')]"));
        public IWebElement mobileAppButton => _webDriver.FindElement(By.XPath("//li[@class='blocks-item' and contains(., 'Mobile App')]"));
        public IWebElement AWSGuideButton => _webDriver.FindElement(By.XPath("//li[@class='blocks-item' and contains(., 'AWS Guide')]"));
        public IWebElement FAQsButton => _webDriver.FindElement(By.XPath("//li[@class='blocks-item' and contains(., 'FAQs')]"));
    }
}
