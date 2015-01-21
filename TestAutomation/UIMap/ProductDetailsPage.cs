using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace TestAutomation.UIMap
{
    public class ProductDetails
    {
        public string ProductName;
        public string Price;
    }
    public class ProductDetailsPage
    {
        IWebDriver _driver;
        public ProductDetailsPage(IWebDriver driver)
        {
            this._driver = driver;
        }
        
        public ProductDetails GetProductDetails()
        {
            ProductDetails details = new ProductDetails();
            var currentNode = CurrentNode;
            details.ProductName = currentNode.FindElement(By.Id("prod_title")).Text;
            details.Price = currentNode.FindElement(By.Id("special_price_box")).Text;

            return details;
        }
      
        public IWebElement AddToCartButton
        {
            get 
            {
                return CurrentNode.FindElement(By.Id("AddToCart"));
            }
        }
        protected IWebElement CurrentNode
        {
            get 
            {
                return _driver.FindElement(By.Id("prd-detail-page"));
            }
        }
    }
}
