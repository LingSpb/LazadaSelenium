using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestAutomation.UIMap
{
    public class RegisterPage
    {
        IWebDriver _driver;
        
        public RegisterPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        #region Methods
        public bool IsErrorMessageDisplayed_PasswordConfirm()
        {
            return (PasswordConfirmMessage.GetAttribute("class").Contains("error-display"));
        }
        public bool IsErrorMessage_PasswordConfirm(string message)
        {
            return PasswordConfirmMessage.Text.Equals(message);
        }
        #endregion

        #region Properties
        public IWebElement CurrentNode 
        {
            get 
            {
                return _driver.FindElement(By.ClassName("nyroModalCont"));
            }
        }
        public IWebElement FirstNameTextBox
        {
            get 
            {
                return CurrentNode.FindElement(By.Id("RegistrationForm_first_name"));
            }
        }
        public IWebElement EmailTextBox
        {
            get 
            {
                return CurrentNode.FindElement(By.Id("RegistrationForm_email"));
            }
        }
        public IWebElement PasswordTextBox
        {
            get
            {
                return CurrentNode.FindElement(By.Id("RegistrationForm_password"));
            }
        }
        public IWebElement PasswordConfirmTextBox
        {
            get 
            {
                return CurrentNode.FindElement(By.Id("RegistrationForm_password2"));
            }
        }
        public IWebElement SendButton
        {
            get 
            {
                return CurrentNode.FindElement(By.Id("send"));
            }
        }
        public IWebElement PasswordConfirmMessage
        {
            get 
            {
                return PasswordConfirmTextBox.FindParent("class", "collection ui-validate-required").FindElement(By.TagName("span"));
            }
        }
        #endregion
    }
}
