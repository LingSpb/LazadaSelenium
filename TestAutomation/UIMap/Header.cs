using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;

namespace TestAutomation.UIMap
{
    public class Header
    {
        IWebDriver driver;
        public Header(IWebDriver driver)
        {
            this.driver = driver;
        }

        #region Properties
        public IWebElement RegisterLink
        {
            get
            {
                return HeaderNavigation.FindElement(By.XPath(".//*[contains(., 'Đăng ký')]"));
            }
        }
        public IWebElement CartIcon
        {
            get
            {
                return CurrentNode.FindElement(By.ClassName("headCart"));
            }
        }
        public IWebElement CurrentNode
        {
            get
            {
                return driver.FindElement(By.ClassName("header"));
            }
        }
        protected IWebElement HeaderNavigation
        {
            get
            {
                return driver.FindElement(By.ClassName("header__navigation"));
            }
        }
        #endregion
    }
}

