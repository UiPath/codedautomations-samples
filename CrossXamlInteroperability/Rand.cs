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

namespace CrossXamlInteroperability
{
    public class Rand : CodedWorkflow
    {
        [Workflow]
        public int Execute(int min, int max)
        {
            // Get a random value between min and max
            var rand = new Random().Next(min, max);
            
            // Return it to the caller
            return rand;
        }
    }
}