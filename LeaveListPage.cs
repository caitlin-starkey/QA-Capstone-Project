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
        public IWebElement employeeNameTextbox => _webDriver.FindElement(By.XPath("//input[@placeholder='Type for hints...']"));
        public IWebElement myRequestedLeaveComment => _webDriver.FindElement(By.XPath("//div[@class='oxd-table-cell oxd-padding-cell' and contains(./*, 'I will not be available on my selected day(s).')]"));
        public IWebElement noRecordsFound => _webDriver.FindElement(By.XPath("//p[@class='oxd-text oxd-text--p oxd-text--toast-message oxd-toast-content-text' and contains(., 'No Records Found')]"));
    }
}
