using System;
using System.Collections.Generic;
using System.Data;
using System.Security;
using Form.CrossBrowser.Tests;
using UiPath.CodedWorkflows;
using UiPath.Core;
using UiPath.Core.Activities;
using UiPath.Core.Activities.Storage;
using UiPath.Orchestrator.Client.Models;
using UiPath.Testing;
using UiPath.Testing.Activities.TestData;
using UiPath.Testing.Activities.TestDataQueues.Enums;
using UiPath.Testing.Enums;
using UiPath.UIAutomationNext.API.Contracts;
using UiPath.UIAutomationNext.API.Models;
using UiPath.UIAutomationNext.Enums;

// For more information, please visit the documentation over at https://docs.uipath.com/studio/standalone/2023.10/user-guide/coded-automations-introduction.
namespace Form.CrossBrowser.Tests
{
    public class TestCase : CodedWorkflow
    {
        // Pre-requisite: Create the FormCredential credential in Orchestrator
        [TestCase]
        public void Execute(System.String browserName = "msedge.exe")
        {
            // Arrange
            
            var screen = uiAutomation.Open("FormScreen", Options.AppOpen().WithVariable("browserName", browserName));
            screen.TypeInto("Name", "Alvin");

            var credential = system.GetCredential("FormCredential", null, out var password, CacheStrategyEnum.None, 30000);
            screen.TypeInto("Email", credential);

            var actualPassword = new System.Net.NetworkCredential(string.Empty, password).Password;
            screen.TypeInto("Password", actualPassword);
            screen.TypeInto("ConfirmPassword", actualPassword);

            // Act
            screen.Click("Submit");

            // Assert
            testing.VerifyExpression(screen.GetText("Verification") == "Alvin");
        }
    }
}