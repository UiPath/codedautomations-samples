using CodedDemo.ObjectRepository;
using System;
using System.Collections.Generic;
using System.Data;
using UiPath.CodedWorkflows;
using UiPath.Core;
using UiPath.Core.Activities.Storage;
using UiPath.Excel;
using UiPath.Excel.Activities;
using UiPath.Excel.Activities.API;
using UiPath.Excel.Activities.API.Models;
using UiPath.Orchestrator.Client.Models;
using UiPath.UIAutomationNext.API.Contracts;
using UiPath.UIAutomationNext.API.Models;
using UiPath.UIAutomationNext.Enums;

namespace CodedDemo
{
    public class UseExcel_ExecuteMacro : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            using (var w = excel.UseExcelFile("withMacros.xlsm"))
            {
                //the macro sets the integer value provided as argument in Shee2 Cell C11
                w.ExecuteMacro("SetC11", new object[]{DateTime.Now.Millisecond});
            }
        }
    }
}