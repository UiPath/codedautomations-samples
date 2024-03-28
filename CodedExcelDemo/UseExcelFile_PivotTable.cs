using System;
using System.Linq;
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
    public class UseExcelFile_PivotTable : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            
            using (var w = excel.UseExcelFile("codedExcel3.xlsx"))
            {
                Console.WriteLine("working with pivot tables");
                
                //make sure we start with a clean sheet
                w.Sheet["newPivotSheet"].DeleteSheet();                
                
                //fill pivot table descriptor
                var ptDesc = new PivotTableDescriptor() { LayoutRowType = PivotTableLayoutRowType.Compact, ValuesMode = PivotTableValuesMode.Columns};
                ptDesc.Columns.Add(new ColumnPivotTableFieldDescription(){Name = "Column1"});
                ptDesc.Rows.Add(new RowPivotTableFieldDescription(){Name = "Column2"});
                ptDesc.Filters.Add(new FilterPivotTableFieldDescription() {Name = "Column3"});
                
                var valueDesc = new ValuePivotTableFieldDescription();
                valueDesc.Name = "Column4";
                valueDesc.Function = PivotTableFunction.Count;
                ptDesc.Values.Add(valueDesc);
                
                //make sure we start on a clean sheet
                w.Sheet["newPivotSheet"].DeleteSheet();
                
                //create pivot table from Table1 (is in Sheet7_unfiltered)
                w.Table["Table1"].CreatePivotTable("myNewPT", w.Sheet["newPivotSheet"], ptDesc);
                
                //clear pivot table filter on "Column3"
                w.Sheet["newSheet"].PivotTable["newPivot"].FilterPivotTable("Column3", default, true);
                
                //set the list of values for "Column3"
                w.Sheet["newSheet"].PivotTable["newPivot"].FilterPivotTable("Column3", new string[]{"cc1", "cc2"}, false);
            }
            
        }
    }
}