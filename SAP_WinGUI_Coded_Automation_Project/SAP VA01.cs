using System;
using System.Collections.Generic;
using UiPath.CodedWorkflows;
using System;
using System.Collections.Generic;
using System.Data;
using UiPath.CodedWorkflows;
using UiPath.Core;
using UiPath.Core.Activities.Storage;
using UiPath.Orchestrator.Client.Models;
using UiPath.Testing;
using UiPath.Testing.Activities.TestData;
using UiPath.Testing.Activities.TestDataQueues.Enums;
using UiPath.Testing.Enums;
using UiPath.UIAutomationNext.API.Contracts;
using UiPath.UIAutomationNext.API.Models;
using UiPath.UIAutomationNext.Enums;
using SAP_WinGUI_Coded_Automation;

// NOTICE: The Coded Automations feature is available as a preview feature and APIs may be subject to change. 
//         No warranty or technical support is provided for this preview feature.
//         Missing features or encountering bugs? Please click the feedback button in the top-right corner and let us know!
// Please delete these comments after you've read and acknowledged them. For more information, please visit the documentation over at https://docs.uipath.com/studio/lang-en/v2023.4/docs/coded-automations.
namespace SAP_CodedWorkFlow
{
    public class SAP_VA01 : CodedWorkflow
    {
        [TestCase]
        public void Execute()
        {
            // Arrange
            //Log("Test run started for SAP_VA01.");
			
			
            // Act
            // To invoke any workflow (XAML or coded), you can use ;helper methods in CodedWorkflow, e.g. RunWorkflow(...).
			
			// Run the existing SAP Login XAML file
			RunWorkflow("SetupEnv_LoginUser.xaml");	
		
			var SAPEasyAccessApp = uiAutomation.Attach("SAP Easy Access");
			SAPEasyAccessApp.TypeInto("OkCodeField okcd", "/nva01[k(Enter)]");
			
            // Create Sale Order Document
			var CreateSalesDoc = uiAutomation.Attach("Create Sales Documents", "SAPWinGui", "1.0.0");
			CreateSalesDoc.TypeInto("Order Type","OR");
			CreateSalesDoc.TypeInto("Distribution Channel","10");
			CreateSalesDoc.TypeInto("Division","00");
			CreateSalesDoc.Click("Continue");
			
			// Create Sales Order
			var createSalesOrder = uiAutomation.Attach("Create Standard Order: Overview","SAPWinGui", "1.0.0");
			createSalesOrder.TypeInto("Sold-To Party","EWM17-CU01");
			createSalesOrder.TypeInto("Ship-To Party","EWM17-CU01");
			createSalesOrder.TypeInto("Cust. Reference","SAP Coded Automation Demo");
			createSalesOrder.TypeInto("Cust. Ref. Date","05/05/2023");
			createSalesOrder.TypeInto("CTextField Material","TG11");
			createSalesOrder.TypeInto("TextField Order Quantity","1");
			createSalesOrder.Click("Save");
			
			// Store the value from the SAP Status bar
			var statusbar = createSalesOrder.GetText("Status Bar");
			
			Log(statusbar);

            // Assert
            // To start using activities, use IntelliSense (CTRL + Space) to discover the available services, e.g. testing.VerifyExpression(...).

			// Assert the value in the SAP Status bar
            testing.VerifyExpression(statusbar == "has not been");
			
			// Run the existing SAP Log off XAML file
			RunWorkflow("SAP_Logoff.xaml");
				
			}
    }
}