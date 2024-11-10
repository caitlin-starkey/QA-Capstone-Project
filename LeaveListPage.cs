using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA_Capstone_Project
{
    public class LeaveListPage
    {
        public IWebDriver _webDriver;

        public LeaveListPage(IWebDriver driver)
        {
            _webDriver = driver;
        }
        public string leaveListPageUrl = "https://opensource-demo.orangehrmlive.com/web/index.php/leave/viewLeaveList";
        public IWebElement applyForLeaveButton => _webDriver.FindElement(By.XPath("//a[@class='oxd-topbar-body-nav-tab-item' and contains(., 'Apply')]"))'

    }
}
