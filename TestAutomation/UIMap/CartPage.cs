using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestAutomation.UIMap
{
    public class ProductDetailsInCart
    {
        public string ProductName;
        public string Price;
        public string Quantity;
        public string Subtotal;
    }
    
    public class CartPage
    {
        IWebDriver _driver;
        public CartPage(IWebDriver driver)
        {
            this._driver = driver;
        }
        
        public IWebElement GetProduct(string productName)
        {
            return CurrentNode.FindElements(By.XPath(".//div[contains(@class, 'productdescription')]")).First(p => p.Text == productName);
        }
        public IWebElement GetProduct(int index)
        {
            return CurrentNode.FindElements(By.XPath(".//div[contains(@class, 'productdescription')]")).ElementAt(index); 
        }
        public IWebElement GetQuantityDropdownBox(int index)
        {
            return CurrentNode.FindElements(By.XPath(".//select[contains(@class, 'cart-product-item-cell-qty-select')]"))[index];
        }
        
        protected ProductDetailsInCart GetProductDetails(IWebElement product)
        {
            if (product == null) return null;

            var productRow = product.FindParent("TagName", "tr");
            
            ProductDetailsInCart details = new ProductDetailsInCart();

            details.ProductName = productRow.FindElement(By.ClassName("productdescription")).Text;
            var price = details.Price = productRow.FindElements(By.XPath(".//td[contains(@class, 'price')]")).First().Text;
            details.Price = price.Split(new string[] { "\r\n" }, StringSplitOptions.None).ElementAt(0);

            details.Quantity = productRow.FindElements(By.XPath(".//option")).Single(p => p.Selected).Text;
            details.Subtotal = productRow.FindElements(By.XPath(".//td[contains(@class, 'price')]")).Last().Text
                .Split(new string[] { "\r\n" }, StringSplitOptions.None).ElementAt(0);

            return details;
        }
        public ProductDetailsInCart GetProductDetails(string productName)
        {
            return GetProductDetails( GetProduct(productName));
        }
        public ProductDetailsInCart GetProductDetails(int index)
        {
            return GetProductDetails(GetProduct(index));
        }
        
        public IWebElement CurrentNode
        {
            get 
            {
                return _driver.FindElement(By.ClassName("nyroModalCont"));
            }
        }
        public IWebElement ContinueShoppingLink
        {
            get { return CurrentNode.FindElement(By.Id("cartContinueShopping")); }
        }

        public bool WaitForRenew()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            return wait.Until(drv => CurrentNode.FindElement(By.XPath(".//div[contains(@class, 's-success')]"))) != null;
        }
    }
}
