using System;
using System.Collections.Generic;
using System.Data;
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

// NOTICE: The Coded Automations feature is available as a preview feature and APIs may be subject to change. 
//         No warranty or technical support is provided for this preview feature.
//         Missing features or encountering bugs? Please click the feedback button in the top-right corner and let us know!
// Please delete these comments after you've read and acknowledged them. For more information, please visit the documentation over at https://docs.uipath.com/studio/lang-en/v2023.4/docs/coded-automations.
namespace CodedWorkflowInteroperability
{
    public class TimeSpanHelper
    {
		private static Random _random = new Random();
		private static object _lockObj = new object();
		
        public static TimeSpan GetRandomTimeSpanBetween(int lowerBoundMs, int upperBoundMs)
		{
			lock(_lockObj)
			{
				var ms = _random.Next(lowerBoundMs, upperBoundMs);
				return TimeSpan.FromMilliseconds(ms);
			}
		}
    }
}