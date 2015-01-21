using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TestAutomation.UIMap;
using System.Diagnostics;
using OpenQA.Selenium.Support.UI;
using TestAutomation.Common;
using System.Globalization;

namespace TestAutomation.TestPlan
{
    public class TestSuite1
    {
        IWebDriver _driver;
        
        public void TestInit()
        {
            _driver = new FirefoxDriver();
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
        }
        public void TestCleanup()
        {
            _driver.Close();
        }

        public void TC_Validate_error_message_in_register_new_account_form()
        {
            var testData = Utils.ReadTestData("TC_Validate_error_message_in_register_new_account_form");
            
            //Step1: Open the home page
            string url = testData["url"];
            _driver.Navigate().GoToUrl(url);

            //Step2: Click on Register link
            Header header = new Header(_driver);
            header.RegisterLink.Click();

            //Step3: Enter information for registration
            string username = testData["username"];
            string email = testData["email"];
            string password = testData["password"];
            string message = testData["message"];

            RegisterPage register = new RegisterPage(_driver);
            register.FirstNameTextBox.SendKeys(username);
            register.EmailTextBox.SendKeys(email);
            register.PasswordTextBox.SendKeys(password);

            //Step4: Click on Send button
            register.SendButton.Click();

            //Verify that error message displayed for Password Confirm area
            Utils.AssertIsTrue(register.IsErrorMessageDisplayed_PasswordConfirm(), "Error message displayed: Failed");
            //Verify that error message is corerct for the case of no input confirmation
            Utils.AssertIsTrue(register.IsErrorMessage_PasswordConfirm(message), " - Check message \"Mật khẩu phải có ít nhất 1 chữ số\"");
        }
        public void TC_Validate_product_information_between_cart_page_and_product_detail_page()
        {
            var testData = Utils.ReadTestData("TC_Validate_product_information_between_cart_page_and_product_detail_page");

            //Step1: Navigate to a product detail page
            string product = testData["product"];
            _driver.Navigate().GoToUrl(product);

            ProductDetailsPage productDetailsPage = new ProductDetailsPage(_driver);
            //Get the product details
            ProductDetails productDetails = productDetailsPage.GetProductDetails();

            //Step2: Add the current product to cart
            productDetailsPage.AddToCartButton.Click();

            //Verification: Make sure that the product information between product detail page and cart page are the same
            /**To do it, I verify:
             * 1) The name of product in product detail page is in the cart page
             * 2) The price
             */
            CartPage newCard = new CartPage(_driver);
            ProductDetailsInCart cartDetail = newCard.GetProductDetails(productDetails.ProductName);

            Utils.AssertIsTrue(cartDetail != null, "Check existence of product by name. Not found " + productDetails.ProductName);
            Utils.AssertIsTrue(cartDetail.Price == productDetails.Price, String.Format("Check price. Cart: {0}. Detail page: {1}", cartDetail.Price, productDetails.Price));
        }
        public void TC_Validate_subtotal_price_when_change_qty_in_cart_popup()
        {
            var testData = Utils.ReadTestData("TC_Validate_subtotal_price_when_change_qty_in_cart_popup");

            //Step1: Add 2 products to cart
            string product1 = testData["product1"];
            string product2 = testData["product2"];

            Actions.AddToCart(_driver, product1);
            Actions.AddToCart(_driver, product2);

            //Step2: Open cart page
            (new Header(_driver)).CartIcon.Click();

            //Get the product details in cart of the item which we will change quantity later
            // in this case, I change the second item
            string itemIndex=testData["changingIndex"];

            CartPage cartPage = new CartPage(_driver);
            var productDetails = cartPage.GetProductDetails(itemIndex);

            //Step3: Change the quantity
            // in this case, I change to number 3
            string newQuantity = testData["changingQuantity"];

            new SelectElement(cartPage.GetQuantityDropdownBox(Convert.ToInt32(itemIndex))).SelectByIndex(Convert.ToInt32(newQuantity) - 1);

            cartPage.WaitForRenew();

            //Verification: subtotal price is corresponding to the price and quantity
            string currency = productDetails.Price.Split(' ')[1];

            var numberFormat = new NumberFormatInfo() { 
                CurrencyGroupSeparator = ".", 
                CurrencyDecimalDigits = 0,
                CurrencyGroupSizes = new int[] {3 , 3},
                NumberDecimalSeparator = ",",
                CurrencyDecimalSeparator = ",",
                CurrencySymbol = currency,
                CurrencyPositivePattern = 3,
                CurrencyNegativePattern = 8
            };

            decimal price = decimal.Parse(productDetails.Price, NumberStyles.Currency, numberFormat);
            var expectedSubtotal = (price * Convert.ToInt32(newQuantity)).ToString("C", numberFormat);

            var newDetails = cartPage.GetProductDetails(itemIndex);

            Utils.AssertIsTrue(newDetails.Subtotal == expectedSubtotal, String.Format("Check subtotal. Expect = {0}, actual = {1}", expectedSubtotal, newDetails.Subtotal));
        }
    }
}
