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

namespace CodedWorkflowInteroperability
{
  public class CodedAutomationUsingWorkflows : CodedWorkflow
  {
    [Workflow]
    public void Execute()
    {
      // Requirements:
      // - Create an asset of type Text called MyAsset in the current folder.

      var result = RunWorkflow("BusinessProcess\\ResetAssetValue.xaml", new Dictionary<string, object>()
      {
        {"assetName", "MyAsset"},
        {"assetValue", "hello world"}
      });

      if ((bool)result["assetValueWasChanged"])
      {
        Log("Reset asset MyAsset, but it had a different value, previous value was " + result["assetValue"]);
      }
      else
      {
        Log("No reset was required on asset MyAsset, which had the expected value.");
      }
    }
  }
}