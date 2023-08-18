using UiPath.CodedWorkflows;
using System.Collections.Generic;

namespace CodedWorkflowInteroperability
{
  public class CodedResetAssetValue : CodedWorkflow
  {
    [Workflow]
    public (bool assetValueWasChanged, string assetValue) Execute(string assetName, string assetValue)
    {
      // Equivalent implementation for ResetAssetValue.xaml in code in order to highlight how parameters work in coded automations.
      // Resets an asset to a specific value if needed. If an asset value is changed, we will also return the previous value.
      var previousAssetValue = system.GetAsset(assetName).ToString();

      if (previousAssetValue.Equals(assetValue))
      {
        return (assetValueWasChanged: false, assetValue: assetValue);
      }
      else
      {
        system.SetAsset(assetValue, assetName);
        return (assetValueWasChanged: true, assetValue: previousAssetValue);
      }

    }
  }
}