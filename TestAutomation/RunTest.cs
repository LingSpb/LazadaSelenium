using System.Runtime.InteropServices;
using System.Diagnostics;
using TestAutomation.TestPlan;
using System;

namespace TestAutomation
{
    public class RunTest
    {
        public static void Main(string[] args)
        {
            TestSuite1 test1 = new TestSuite1();

            //Run testcase1
            Utils.WriteLog("START::TC_Validate_error_message_in_register_new_account_form");
            test1.TestInit();
            try
            {
                test1.TC_Validate_error_message_in_register_new_account_form();
            }
            catch (Exception e)
            {
                Utils.WriteLog(e.ToString());
            }
            test1.TestCleanup();
            Utils.WriteLog("END::TC_Validate_error_message_in_register_new_account_form");

            //Run testcase2
            test1.TestInit();
            Utils.WriteLog("START::TC_Validate_product_information_between_cart_page_and_product_detail_page");
            try
            {
                test1.TC_Validate_product_information_between_cart_page_and_product_detail_page();
            }
            catch (Exception e)
            {
                Utils.WriteLog(e.ToString());
            }

            test1.TestCleanup();
            Utils.WriteLog("END::TC_Validate_product_information_between_cart_page_and_product_detail_page");

            //Run testcase3
            Utils.WriteLog("START::TC_Validate_subtotal_price_when_change_qty_in_cart_popup");
            test1.TestInit();
            try
            {
                test1.TC_Validate_subtotal_price_when_change_qty_in_cart_popup();
            }
            catch (Exception e)
            {
                Utils.WriteLog(e.ToString());
            }
            test1.TestCleanup();
            Utils.WriteLog("END::TC_Validate_subtotal_price_when_change_qty_in_cart_popup");

        }
    }
}
