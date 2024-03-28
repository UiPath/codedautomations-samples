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
    public class Excel4_full : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            //Create excel process scope using default options
            using (var defaultProcess = excel.ExcelProcessScope())
            //UseExcelFile using more options, including the excel process scope handle
            using (var w = excel.UseExcelFile( new UseOptions(){ExcelProcess = defaultProcess, Path = "codedExcel3.xlsx", SaveChanges = true, CreateIfNotExist = true} ))
            {
                //Read range
                var table = w.Sheet["Sheet1"].ReadRange(true, true);
                
                //Write range and then append sheet3
                w.Sheet["Sheet4"].WriteRange(table, true, false);
                w.Sheet["Sheet4"].AppendRange(w.Sheet["Sheet3"]);
            }
            
            
            //Create process excel process using custom options (if Excel file is already open, warning will be displayed to the user)
            using (var excelProcess = excel.ExcelProcessScope(new ScopeOptions() {FileConflictResolution = ExcelFileConflictResolution.PromptUser, ProcessMode = ExcelProcessMode.AttendedUser}))
            using (var handle = excel.UseExcelFile(new UseOptions() {ExcelProcess = excelProcess, Path = "newFile2l.xlsx", KeepExcelOpen = true, SaveChanges = true, CreateIfNotExist = true}))
            {
                var table = handle.Sheet["Sheet1"].ReadRange(true, true);
                                
                handle.Sheet["Sheet2_new"].WriteRange(table, true, false);
                handle.Sheet["Sheet4_new"].AppendRange(handle.Sheet["Sheet1"], true, CopyPasteRangeOptions.All, false );
            }
        }
    }
}