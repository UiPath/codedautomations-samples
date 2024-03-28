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
    public class CodedWorkbook1 : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            //Retrieve handle for the workbook (it doesn't require excel to be installed)
            using (var wb = excel.UseWorkBook("codedExcel_workbook.xlsx"))
            {
                //Workbook read range
                var table = wb.ReadRange("Sheet1", null, true, true);
                Console.WriteLine( wb.GetTableRange("Sheet2","Table1",false));
                Console.WriteLine(wb.ReadCellFormula("Sheet2","A8"));
                Console.WriteLine(wb.ReadCell("Sheet2","A2",true));
                wb.WriteCell("Sheet2","A2","1");
                wb.WriteCell("Sheet2","A3","1");
                Console.WriteLine(wb.ReadCell("Sheet2","A8",true));
                foreach(String iterator in  wb.ReadColumn("Sheet2","A1",true)){
                    Console.WriteLine(iterator);
                }
                foreach(String iterator in  wb.ReadRow("Sheet2","A1",true)){
                    Console.WriteLine(iterator);
                }
                
                //Display the read range
                PrintDataTable(table);
                wb.AppendRange("Sheet5",table);
                
                //Write table to new sheet
                wb.WriteRange("Sheet5", null, table, true);                
            }
        }
        
        //Writes to the console the content of a table
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