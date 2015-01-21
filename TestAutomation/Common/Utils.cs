using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using System.Xml;

namespace TestAutomation
{
    public static class Utils
    {
        public static string LocalGetAttribute(this IWebElement item, string attribute)
        {
            try
            {
                switch (attribute)
                {
                    case "TagName":
                    case "tag":
                        return item.TagName;
                    case "Text":
                    case "text":
                    case "LinkText":
                        return item.Text;
                    case "Enabled":
                        return item.Enabled.ToString();
                    case "Displayed":
                        return item.Displayed.ToString();
                    case "Selected":
                        return item.Selected.ToString();
                    default:
                        return item.GetAttribute(attribute);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static IWebElement FindParent(this IWebElement childNode, params string[] match)
        {
            int count = match.Length;
            bool matched = false;
            IWebElement parent = childNode.FindElement(By.XPath(".."));
            if (count == 0) return parent;
            
            while (!matched)
            {
                for (int i = 0; i < count; i += 2)
                {
                    string value = parent.LocalGetAttribute(match[i]);
                   
                    if (value == match[i + 1]) matched = true;
                    else { matched = false; break; }
                }

                parent = parent.FindElement(By.XPath(".."));
                if (parent.TagName == "html") return null;
            }
            if (matched) return parent;
            return null;
        }

        public static Dictionary<string, string> ReadTestData(string testcase)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("TestData.xml");
            XmlNode testcaseNode = doc.SelectSingleNode(String.Format("//testcase[@test='{0}']", testcase));
            
            Dictionary<string, string> list = new Dictionary<string, string>();
            foreach (XmlNode node in testcaseNode.ChildNodes)
            {
                list.Add(node.Name, node.InnerText);
            }

            return list;
        }
        public static void WriteLog(string message)
        {
            System.IO.File.AppendAllText(@"Logs.txt", String.Format("{0}: {1}", DateTime.Now, message) + Environment.NewLine);
        }
        public static void AssertIsTrue(bool result, string message)
        {
            if (!result)
            {
                WriteLog(message);
                throw new System.Exception("Assert failed");
            }
        }
    }
}
