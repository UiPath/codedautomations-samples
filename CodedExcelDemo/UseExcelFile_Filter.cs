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
using System.Linq;

namespace CodedExcelDemo
{
    public class UseExcelFile_Filter : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            using (var w = excel.UseExcelFile("codedExcel3.xlsx"))
            {
                //clear filter
                w.Sheet["Sheet4"].Filter(new FilterOptions(){ClearFilter = true});
                
                //basic filter
                w.Sheet["Sheet4"].Filter(new FilterOptions(){ColumnName="aa1", Filter = new BasicFilter() {Values = { "bb2", "bb1"}}});
                
                //advanced filter
                var advancedFilter = new AdvancedFilter() {Operator=LogicalOperator.Or,
                  Condition1 = ExcelFilterOperator.CONTAINS, Value1="a",
                    Condition2 = ExcelFilterOperator.CONTAINS, Value2="b"};
                
                w.Sheet["Sheet4"].Filter(new FilterOptions(){ColumnName="aa1", Filter = advancedFilter});
                
                //delay for 1s
                System.Threading.Thread.Sleep(1000);
                
                
                //for each sheet exaple
                w.ForEachSheet((sheet) => {
                    //add logic to execute for each row here
                    Console.WriteLine(sheet.Name);
                });
                
                //Create fresh copy of a sheet
                w.Sheet["Sheet7_filtered"].DeleteSheet();
                w.Sheet["Sheet6_filtered"].DuplicateSheet("Sheet7_filtered");
                
                //for each row example
                w.Sheet["Sheet7_filtered"].ForEachRow(default, true, default, (row) => 
                {                   
                    //simulates filling a new column in the table
                    //determine cell address
                    string cellAddr = row.Address.Split(":").First().Replace("A", "F");

                    //append the computed value
                    string newValue = $"{row.ByIndex[0]}_{row.ByField["Col2"]}";
                    
                    //display the info for the cells
                    Console.WriteLine($"in cell {row.Address} value {newValue} will be written");
                    
                    //write the computed value
                    w.Sheet[row.Worksheet].Cell[cellAddr].WriteCell(newValue);
                });
                
            }
        }
    
    
        private void PrintDataTable(System.Data.DataTable dt)
        {
            //Console.WriteLine("start print data table");
            
            for(int i = 0; i < dt.Rows.Count; ++i) 
            {
                string row = string.Empty; 
                foreach (var value in dt.Rows[i].ItemArray)
                {
                    row += value.ToString() + " ";
                }
                Console.WriteLine(row);
            }
            
            //Console.WriteLine("end print data table");
        }
    }
}