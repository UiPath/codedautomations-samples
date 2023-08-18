using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using UiPath.Activities.Contracts;
using UiPath.CodedWorkflows;

namespace PythonSeleniumTestsInteroperability
{
    public class TestCase_SeleniumTest : PythonCodedWorkflowWrapper
    {
		// This is a wrapped Selenium test in Python, which we additionally augmented with some UiPath Platform services.
		// The approach showcases how you can use the coded automations framework in order to either:
		//
		// 1) Gradually transition your Selenium tests to UiPath test cases.
		// 2) Run Selenium test cases but have reporting, monitoring, parallelization and verification handled through UiPath's platform.
		// 3) Leverage UiPath activity packages within Selenium tests (e.g. UI Automation for scenarios which Selenium can't handle) 
		//    or run custom UiPath workflows within them.
        [TestCase]
		[WorkflowDependency("Sequence.xaml")]
        public void Execute(System.String UserName="CodedAutomations",System.String Email="coded@uipath.com",System.String VerificationText="CodedAutomations")		
		{
			var vars = new Dictionary<string, object>()
			{
				{ "userName", UserName },
				{ "email", Email },
				{ "verificationText", VerificationText },
				
				// We can pass any of our services, e.g. system, testing or uiAutomation directly into Python when using this approach
				{ "testingService", testing }
			};
			
			var result = RunPythonScript("SeleniumTest.py", vars, new string[] { "windowTitle" } );
			
			Log("Window Title: " + result["windowTitle"].ToString());
        }
    }
}