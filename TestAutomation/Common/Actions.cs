using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using TestAutomation.UIMap;

namespace TestAutomation.Common
{
    public static class Actions
    {
        public static void AddToCart(IWebDriver driver, string productDetailLink)
        {
            driver.Navigate().GoToUrl(productDetailLink);

            ProductDetailsPage productDetailsPage = new ProductDetailsPage(driver);
            productDetailsPage.AddToCartButton.Click();

            CartPage cartPage = new CartPage(driver);
            cartPage.ContinueShoppingLink.Click();
        }
    }
}
