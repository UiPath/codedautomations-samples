using System;
using System.Collections.Generic;
using System.Data;
using UiPath.CodedWorkflows;
using UiPath.Core;
using UiPath.Core.Activities.Storage;
using UiPath.Excel;
using UiPath.Excel.Activities;
using UiPath.Excel.Activities.API;
using UiPath.Orchestrator.Client.Models;
using UiPath.UIAutomationNext.API.Contracts;
using UiPath.UIAutomationNext.API.Models;
using UiPath.UIAutomationNext.Enums;

namespace CodedDemo
{
    public class Excel1_UseFile : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {            
            //Retrieve Excel file handle using "excel" service and default options
            //Options that are not explicitely set will be read from Project Settings -> Excel Modern (as UseExcelFile activity)
            using (var w = excel.UseExcelFile("codedExcel3.xlsx"))
            {
                //Read sheet from the excel file and store it a data table
                var table = w.Sheet["Sheet1"].ReadRange(true, true);
                
                //Display the content of the datatable
                PrintDataTable(table);
                
                //clear the range if is too big
                {
                    var (firstUsed, lastUsed) = w.Sheet["Sheet4"].FindFirstLastDataRow("A", false, false, default);
                    if (lastUsed > 100)
                        w.Sheet["Sheet4"].ClearRange(false);
                }

                //Write  the read datatable to another sheet
                w.Sheet["Sheet4"].WriteRange(table, true, false);
                
                //Append Sheet3 to the content of Sheet4
                w.Sheet["Sheet4"].AppendRange(w.Sheet["Sheet3"]);
                
                //Alternative way of calling AppendRange (using ExcelOperations)
                ExcelOperations.AppendRange(w.Sheet["Sheet4"], w.Sheet["Sheet2"].Range["C2:D3"]);
                
                //Get first and last rows of the new sheet after all the operations
                var (first, last) = w.Sheet["Sheet4"].FindFirstLastDataRow("A", false, true, LastRowConfiguration.LastPopulatedRow);
                
                //Write to the 
                Console.WriteLine($"First {first}, Last {last}");
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