using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA_Capstone_Project
{
    public class ApplyLeavePage
    {
        public IWebDriver _webDriver;

        public ApplyLeavePage(IWebDriver driver)
        {
            _webDriver = driver;
        }
        public IWebElement leaveBalance => _webDriver.FindElement(By.XPath("//p[@class'oxd-text oxd-text--p orangehrm-leave-balance-text']"));
        public IWebElement leaveEntitlementsDropdown => _webDriver.FindElement(By.XPath("//span[@class='oxd-topbar-body-nav-tab-item' and contains(., 'Entitlements')]"));
        public IWebElement leaveEntitlementsDropdownAddEntitlements => _webDriver.FindElement(By.XPath("//a[@role='menuitem' and contains(., 'Add Entitlements')]"));
        public IWebElement fromOpenCalendarButton => _webDriver.FindElement(By.XPath("//i[@class='oxd-icon bi-calendar oxd-date-input-icon' and contains(./ancestor::*, 'From Date')]"));
        public IWebElement toOpenCalendarButton => _webDriver.FindElement(By.XPath("//i[@class='oxd-icon bi-calendar oxd-date-input-icon' and contains(./ancestor::*, 'To Date')]"));
        public IWebElement todaysDateCalendar => _webDriver.FindElement(By.XPath("//div[@class='oxd-date-input-link --today']"));
        public IWebElement normalDateCalendar => _webDriver.FindElement(By.XPath("//div[@class='oxd-calendar-date' and not(contains(.., @class='--non-working-day oxd-calendar-date-wrapper'))]"));
        public IWebElement holidayFullDateCalendar => _webDriver.FindElement(By.XPath("//div[@class='--holiday-full oxd-calendar-date']"));
        public IWebElement nonWorkingDateCalendar => _webDriver.FindElement(By.XPath("//div[@class='--non-working-day oxd-calendar-date-wrapper']"));
        //The currently selected date appends " --selected" to the end of the date XPath, a day that can't be selected for holiday puts "--holiday-full " in front of the calendar date.
        //Non-working days don't add anything special to the calendar date- they're indicated in the wrapper for the date instead.
        //Date wrapper usually reads "oxd-calendar-date-wrapper" but on non-working days "--non-working-day " is put in front of that
    }
}
