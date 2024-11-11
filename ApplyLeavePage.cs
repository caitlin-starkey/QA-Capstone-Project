using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography.X509Certificates;

namespace QA_Capstone_Project
{
    public class ApplyLeavePage
    {
        public IWebDriver _webDriver;

        public ApplyLeavePage(IWebDriver driver)
        {
            _webDriver = driver;
        }
        public bool CheckForLeave()
        {
            applyForLeaveHeaderButton.Click();
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.Until(d => leaveBalance.Displayed);
            Thread.Sleep(500); //can't figure out how else to get around the loader form obscuring my elements
            leaveTypeDropdown.Click();
            leaveTypeFMLAChoice.Click();
            Thread.Sleep(500); //need time for the leave balance to update
            string fmlaLeaveBalance = leaveBalance.Text;
            string[] fmlaBalance = fmlaLeaveBalance.Split(' '); //allows me to remove " Days" from the number I need to parse
            decimal balance = decimal.Parse(fmlaBalance[0]);
            if (balance >= 1.00m) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ApplyForLeave()
        {
            applyForLeaveHeaderButton.Click();
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.Until(d => leaveBalance.Displayed);
            Thread.Sleep(1000); //can't figure out how else to get around the loader form obscuring my elements
            leaveTypeDropdown.Click();
            leaveTypeFMLAChoice.Click();
            Thread.Sleep(500); //need time for the leave balance to update
            fromOpenCalendarButton.Click();
            wait.Until(d => todaysDateCalendar.Displayed);
            nextNormalDateCalendar.Click(); //functionality I'd love to implement but don't have time to- making nextNormalDateCalendar into a list instead and using an if statement to click the next month and select a day there if there isn't an open date in the current month after the current date
            //to Calendar autopopulates starting on the day selected in from Calendar
            commentBox.SendKeys("I will not be available on my selected day(s).");
            submitLeaveApplicationButton.Click();
            wait.Until(d => applyForLeaveSuccess.Displayed);

        }
        public string applyForLeaveUrl = "https://opensource-demo.orangehrmlive.com/web/index.php/leave/applyLeave";
        //public IWebElement formLoader => _webDriver.FindElement(By.XPath("//div[@class='oxd-form-loader']"));
        public IWebElement applyForLeaveHeaderButton => _webDriver.FindElement(By.XPath("//a[@class='oxd-topbar-body-nav-tab-item' and contains(., 'Apply')]"));
        public IWebElement leaveBalance => _webDriver.FindElement(By.XPath("//p[@class='oxd-text oxd-text--p orangehrm-leave-balance-text']"));
        public IWebElement leaveTypeDropdown => _webDriver.FindElement(By.XPath("//div[@class='oxd-select-text-input' and contains(./ancestor::*, 'Leave Type')]"));
        public IWebElement leaveTypeFMLAChoice => _webDriver.FindElement(By.XPath("//div[@class='oxd-select-option' and contains(./*, 'CAN - FMLA')]"));
        public IWebElement fromOpenCalendarButton => _webDriver.FindElement(By.XPath("//i[@class='oxd-icon bi-calendar oxd-date-input-icon' and contains(./ancestor::*, 'From Date')]"));
        public IWebElement toOpenCalendarButton => _webDriver.FindElement(By.XPath("//i[@class='oxd-icon bi-calendar oxd-date-input-icon' and contains(./ancestor::*, 'To Date')]"));
        public IWebElement nextMonthOnCalendarButton => _webDriver.FindElement(By.XPath("//i[@class='oxd-icon bi-chevron-right']"));
        public IWebElement todaysDateCalendar => _webDriver.FindElement(By.XPath("//div[@class='oxd-date-input-link --today']"));
        public IWebElement todaysDateCalendarSelected => _webDriver.FindElement(By.XPath("//div[contains(@class, '--selected --today')]"));
        //public IWebElement normalDateCalendar => _webDriver.FindElement(By.XPath("//div[@class='oxd-calendar-date' and not(.., @class='--non-working-day oxd-calendar-date-wrapper')]"));
        //public IWebElement normalDateCalendarSelected => _webDriver.FindElement(By.XPath("//div[@class='oxd-calendar-date --selected' and not(.., @class='--non-working-day oxd-calendar-date-wrapper')]"));
        public IWebElement nextNormalDateCalendar => todaysDateCalendarSelected.FindElement(By.XPath("../following-sibling::div[@class='oxd-calendar-date-wrapper']//child::div[@class='oxd-calendar-date']"));
        public IWebElement commentBox => _webDriver.FindElement(By.XPath("//textarea[@class='oxd-textarea oxd-textarea--active oxd-textarea--resize-vertical']"));
        public IWebElement submitLeaveApplicationButton => _webDriver.FindElement(By.XPath("//button[@type='submit']"));
        public IWebElement applyForLeaveSuccess => _webDriver.FindElement(By.XPath("//p[@class='oxd-text oxd-text--p oxd-text--toast-message oxd-toast-content-text' and contains(., 'Successfully Saved')]"));
        //public IWebElement nextNormalDateCalendarNotFull => nextNormalDateCalendar.FindElement(By.XPath("../following-sibling::div[@class='oxd-calendar-date-wrapper')]"));
        //public IWebElement holidayFullDateCalendar => _webDriver.FindElement(By.XPath("//div[@class='--holiday-full oxd-calendar-date']"));
        //public IWebElement nonWorkingDateCalendar => _webDriver.FindElement(By.XPath("//div[@class='--non-working-day oxd-calendar-date-wrapper']"));



        //The currently selected date appends " --selected" to the end of the date XPath and additionally " --today" after selected if it is today's date, otherwise it adds " --today" after date
        //a day that can't be selected for holiday puts "--holiday-full " in front of the calendar date.
        //Non-working days don't add anything special to the calendar date- they're indicated in the wrapper for the date instead.
        //Date wrapper usually reads "oxd-calendar-date-wrapper" but on non-working days "--non-working-day " is put in front of that
    }
}
