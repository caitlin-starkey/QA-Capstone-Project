using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QA_Capstone_Project
{
    public class MyInfoPage
    {
        public IWebDriver _webDriver;

        public MyInfoPage(IWebDriver driver)
        {
            _webDriver = driver;
        }
        public string[] EditEmployeeName()
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.Until(d => firstNameTextbox.Displayed);
            SeleniumHelpers _seleniumHelpers;
            _seleniumHelpers = new SeleniumHelpers(_webDriver);
            string[] updatedEmployeeName = new string[3];
            updatedEmployeeName[0] = _seleniumHelpers.CreateUsername(); //new first name
            updatedEmployeeName[1] = _seleniumHelpers.CreateUsername(); //new middle name
            updatedEmployeeName[2] = _seleniumHelpers.CreateUsername(); //new last name
            Thread.Sleep(1000); //there is some kind of temporary element blocking the web elements I want to target on the page if the browser doesn't wait a little. haven't been able to find it to do anything about it other than a hard stop for 1 second.
            firstNameTextbox.Click();
            firstNameTextboxFocused.SendKeys(Keys.Control + "a");
            firstNameTextboxFocused.SendKeys(Keys.Delete);
            firstNameTextboxFocused.SendKeys(updatedEmployeeName[0]);
            middleNameTextbox.Click();
            middleNameTextboxFocused.SendKeys(Keys.Control + "a");
            middleNameTextboxFocused.SendKeys(Keys.Delete);
            middleNameTextboxFocused.SendKeys(updatedEmployeeName[1]);
            lastNameTextbox.Click();
            lastNameTextboxFocused.SendKeys(Keys.Control + "a");
            lastNameTextboxFocused.SendKeys(Keys.Delete);
            lastNameTextboxFocused.SendKeys(updatedEmployeeName[2]);
            savePersonalDetailsButton.Click();
            wait.Until(d => personalDetailsUpdatedSuccess.Displayed);
            return updatedEmployeeName;
        }
        public string[] AttachAFileToPersonalDetails(string fileName)
        {
            string[] attachmentDetails = new string[7];
            string baseDirectory = AppContext.BaseDirectory;
            string relativePath = "../../Files/" + fileName;
            string fullPath = Path.GetFullPath(Path.Combine(baseDirectory, relativePath));
            attachmentDetails[2] = "Definitely not the bee movie script."; //attachment description
            attachmentDetails[4] = fileName.Split('.').Last(); //attachment file type
            switch (attachmentDetails[4])
            {
                case "txt":
                    attachmentDetails[4] = "text/plain";
                    break;
                case "png":
                    attachmentDetails[4] = "image/png";
                    break;
            }
            _webDriver.WaitAndClick(() => addAttachmentsButton);
            _webDriver.WaitUntilEnabled(() => uploadFileInput);
            uploadFileInput.SendKeys(fullPath);
            attachmentCommentBox.SendKeys(attachmentDetails[2]);
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.Until(d => attachmentSaveButton.Displayed);
            attachmentCommentBoxFocused.SendKeys(Keys.Tab);
            IWebElement cancelButton = _webDriver.SwitchTo().ActiveElement();
            cancelButton.SendKeys(Keys.Tab);
            IWebElement saveButton = _webDriver.SwitchTo().ActiveElement();
            saveButton.SendKeys(Keys.Enter);
            wait.Until(d => addedAttachmentSuccess.Displayed);
            return attachmentDetails;
        }
        public void VerifyAttachedFile(string fileName, string[] attachmentDetails)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.Until(d => attachmentContainer.Displayed);
            List<IWebElement> attachments = attachmentsList;
            List<IWebElement> attachmentCells;
            string attachmentFileName;
            string attachmentDescription;
            string attachmentType;
            string attachmentDateAdded;
            string attachmentAddedBy;
            bool foundMatchingAttachment = false;

            foreach (IWebElement attachment in attachments)
            {
                attachmentCells = attachment.FindElements(By.XPath("descendant::div[@role='cell']")).ToList();
                attachmentFileName = attachmentCells[1].Text;
                attachmentDescription = attachmentCells[2].Text;
                attachmentType = attachmentCells[4].Text;
                attachmentDateAdded = attachmentCells[5].Text;
                attachmentAddedBy = attachmentCells[6].Text;
                if (
                    attachmentFileName == fileName &&
                    attachmentDescription == attachmentDetails[2] &&
                    attachmentType == attachmentDetails[4] &&
                    attachmentDateAdded == DateTime.Now.ToString("yyyy-d-M") &&
                    attachmentAddedBy == "Admin"
                )
                {
                    foundMatchingAttachment = true;
                    break;
                }
            }
            Assert.IsTrue(foundMatchingAttachment);
        }
    
        public IWebElement firstNameTextbox => _webDriver.FindElement(By.XPath("//input[@class='oxd-input oxd-input--active orangehrm-firstname']"));
        public IWebElement firstNameTextboxFocused => _webDriver.FindElement(By.XPath("//input[@name='firstName']"));
        public IWebElement middleNameTextbox => _webDriver.FindElement(By.XPath("//input[@class='oxd-input oxd-input--active orangehrm-middlename']"));
        public IWebElement middleNameTextboxFocused => _webDriver.FindElement(By.XPath("//input[@name='middleName']"));
        public IWebElement lastNameTextbox => _webDriver.FindElement(By.XPath("//input[@class='oxd-input oxd-input--active orangehrm-lastname']"));
        public IWebElement lastNameTextboxFocused => _webDriver.FindElement(By.XPath("//input[@name='lastName']"));
        public IWebElement savePersonalDetailsButton => _webDriver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary orangehrm-left-space' and contains(./ancestor::*, '* Required')]"));
        public IWebElement personalDetailsUpdatedSuccess => _webDriver.FindElement(By.XPath("//p[@class='oxd-text oxd-text--p oxd-text--toast-message oxd-toast-content-text' and contains(., 'Successfully Updated')]"));
        public IWebElement addAttachmentsButton => _webDriver.FindElement(By.XPath("//button[@type='button' and contains(., 'Add')]"));
        public IWebElement uploadFileInput => _webDriver.FindElement(By.XPath("//input[@type='file']"));
        public IWebElement attachmentCommentBox => _webDriver.FindElement(By.XPath("//textarea[@class='oxd-textarea oxd-textarea--active oxd-textarea--resize-vertical']"));
        public IWebElement attachmentCommentBoxFocused => _webDriver.FindElement(By.XPath("//textarea[@class='oxd-textarea oxd-textarea--focus oxd-textarea--resize-vertical']"));
        //public IWebElement attachmentSaveButton => _webDriver.FindElement(By.XPath("//button[@type='submit' and contains(./ancestor::*, 'Add Attachment')]")); can't get this targeting to work

        public IWebElement attachmentContainer => _webDriver.FindElement(By.XPath("//div[@class='oxd-table-body' and @role='rowgroup']"));
        public List<IWebElement> attachmentsList => attachmentContainer.FindElements(By.XPath("descendant::div[@role='row']")).ToList();
        public IWebElement addedAttachmentSuccess => _webDriver.FindElement(By.XPath("//p[@class='oxd-text oxd-text--p oxd-text--toast-message oxd-toast-content-text' and contains(., 'Successfully Saved')]"));
    }
}
