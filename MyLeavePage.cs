using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA_Capstone_Project
{
    public class MyLeavePage
    {
        public IWebDriver _webDriver;

        public MyLeavePage(IWebDriver driver)
        {
            _webDriver = driver;
        }

        public string myLeavePageUrl = "https://opensource-demo.orangehrmlive.com/web/index.php/leave/viewMyLeaveList";
        public IWebElement myLeaveHeaderButton => _webDriver.FindElement(By.XPath("//a[@class='oxd-topbar-body-nav-tab-item' and contains(., 'My Leave')]"));
        public IWebElement myLeaveListTitle => _webDriver.FindElement(By.XPath("//h5[@class='oxd-text oxd-text--h5 oxd-table-filter-title' and contains(., 'My Leave List')]"));
        public IWebElement cancelLeaveButton => _webDriver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--label-warn oxd-table-cell-action-space' and contains(./ancestor::*, 'I will not be available on my selected day(s).')]"));
        public IWebElement myRequestedLeaveComment => _webDriver.FindElement(By.XPath("//div[@class='oxd-table-cell oxd-padding-cell' and contains(./*, 'I will not be available on my selected day(s).')]"));
        public IWebElement cancelLeaveSuccess => _webDriver.FindElement(By.XPath("//p[@class='oxd-text oxd-text--p oxd-text--toast-message oxd-toast-content-text' and contains(., 'Successfully Updated')]"));
        public IWebElement myLeaveStatusCancelled => myRequestedLeaveComment.FindElement(By.XPath("//preceding-sibling::div[@class='oxd-table-cell oxd-padding-cell' and contains(./*, 'Cancelled')]"));
    }
}
