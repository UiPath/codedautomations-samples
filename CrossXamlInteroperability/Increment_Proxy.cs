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
    /// <summary>
    /// A proxy for calling Increment.xaml from xaml
    /// </summary>
    public class Workflow : CodedWorkflow
    {
        [Workflow]
        public int Execute(int rand)
        {
            // Receive rand from the xaml and increment it 
            var out_arg  = workflows.Increment(rand);   
            
            // Log the result and return it to the caller
            Log(out_arg.ToString());
            
            // Return the result to the caller
            return out_arg;
        }
    }
}