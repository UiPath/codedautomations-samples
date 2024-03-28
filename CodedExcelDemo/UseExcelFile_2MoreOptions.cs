using System;
using System.Collections.Generic;
using System.Data;
using UiPath.CodedWorkflows;
using UiPath.Core;
using UiPath.Core.Activities.Storage;
using UiPath.Excel;
using UiPath.Orchestrator.Client.Models;
using UiPath.UIAutomationNext.API.Contracts;
using UiPath.UIAutomationNext.API.Models;
using UiPath.UIAutomationNext.Enums;
using UiPath.Excel.Activities.API;
using UiPath.Excel.Activities;
using UiPath.Excel.Activities.API.Models;

namespace CodedDemo
{
    public class Excel2_UseFile : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {            
            //Retrieve Excel file handle using "excel" service and some default options
            //Options that are not explicitely set will be read from Project Settings -> Excel Modern (as UseExcelFile activity)
            //Excel will be kept open
            using (var handle = excel.UseExcelFile( new UseOptions() { Path = "newFile2l.xlsx", KeepExcelOpen = true, SaveChanges = true }))
            {
                //Read range
                var table = handle.Sheet["Sheet1"].ReadRange(true, true);
                
                //Display datatable
                PrintDataTable(table);
                
                //Write table in new sheet
                handle.Sheet["Sheet2_new"].WriteRange(table, true, false);
                
                //Append range
                handle.Sheet["Sheet4_new"].AppendRange(handle.Sheet["Sheet1"], true, CopyPasteRangeOptions.All, default );
                
                
                //Read and write to specific range
                {
                    var table2 = handle.Sheet["Sheet1"].Range["A1:B3"].ReadRange(true, true);
                    handle.Sheet["Sheet2_new"].Range["F5:G10"].WriteRange(table2, false, false);
                }
                
            }
        }
        
        private void PrintDataTable(System.Data.DataTable dt)
        {
            Console.WriteLine("start print data table");
            
            for(int i = 0; i < dt.Rows.Count; ++i) 
            {
                string row = string.Empty; 
                foreach (var value in dt.Rows[i].ItemArray)
                {
                    row += value.ToString() + " ";
                }
                Console.WriteLine(row);
            }
            Console.WriteLine("end print data table");
        }
    }
}