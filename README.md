# README #

1) Program is written in C# using the Selenium .NET connector. The default browser of Selenium is used (Firefox). It requires .NET Framework v4.0 to run.

2) It is created using the free Express version of Microsoft Visual Studio 2010 instead of Professional which requires licensing. Unfortunately, the limitations of Express does not allow creation of a proper Test Project with the MSTest runner. Instead, to make the test run, I created my own assertion method, which is located in Utils.cs (method public static void AssertIsTrue(bool result, string message))

3) I realized that Selenium doesn’t support .NET quite as well as Java, but because I’m more comfortable with C# and because of the limited time, I chose to write in C# instead of Java or any other language.

4) Because my current testcases are not marked as [TestMethod] (as the MSTest test runner expects), to run them, I created a RunTest class to execute the test cases.

The test suite, you can find in TestPlan/TestSuite1.cs

If any URLs, prices or other data of a test case needs to be changed, edit the TestData.xml file which resides in the same folder as the executable file.

The result is written in the file Logs.txt in the same location of TestAutomation.exe
It looks like:

1/20/2015 9:16:36 PM: START::TC_Validate_error_message_in_register_new_account_form
1/20/2015 9:18:13 PM: START::TC_Validate_product_information_between_cart_page_and_product_detail_page
1/20/2015 9:19:15 PM: Check price. Cart: 989.000 VND. Detail page: 1.899.000 VND
1/20/2015 9:19:15 PM: Assert failed
1/20/2015 9:19:15 PM: START::TC_Validate_subtotal_price_when_change_qty_in_cart_popup