using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA_Capstone_Project
{
    public class DashboardPage
    {
        public IWebDriver _webDriver;

        public DashboardPage(IWebDriver driver)
        {
            _webDriver = driver;
        }
        public string dashboardPageUrl = "https://opensource-demo.orangehrmlive.com/web/index.php/dashboard/index";
        public IWebElement dashboardHeader => _webDriver.FindElement(By.XPath("//h6[text()='Dashboard']"));
        public IWebElement helpIcon => _webDriver.FindElement(By.XPath("//i[@class='oxd-icon bi-question-lg']"));

    }
}
