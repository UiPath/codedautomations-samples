using CodedExcelDemo.ObjectRepository;
using System;
using System.Collections.Generic;
using System.Data;
using UiPath.Activities.System.Jobs.Coded;
using UiPath.CodedWorkflows;
using UiPath.Core;
using UiPath.Core.Activities.Storage;
using UiPath.Excel;
using UiPath.Excel.Activities;
using UiPath.Excel.Activities.API;
using UiPath.Excel.Activities.API.Models;
using UiPath.Mail.Activities.Api;
using UiPath.Orchestrator.Client.Models;
using UiPath.Testing;
using UiPath.Testing.Activities.Api.Models;
using UiPath.Testing.Activities.Models;
using UiPath.Testing.Activities.TestData;
using UiPath.Testing.Activities.TestDataQueues.Enums;
using UiPath.Testing.Enums;
using UiPath.UIAutomationNext.API.Contracts;
using UiPath.UIAutomationNext.API.Models;
using UiPath.UIAutomationNext.Enums;
using System.IO;

namespace CodedExcelDemo
{
    public class UseWorkbook_Create_5 : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            //Test creation of the files (new document)
            
            var myFile = "myNewFile.xlsx";
            if (File.Exists(myFile))
                File.Delete(myFile);
            
            using (var wb = excel.UseWorkBook(new WorkbookOptions() {Path = myFile, CreateNew = true}))
            {
                PrintSheets(wb);
                wb.WriteCell("newSheet", "A3", "someText");
                PrintSheets(wb);
            }
            
            Console.WriteLine($"File {myFile} created: {File.Exists(myFile)}");
            
            using (var wb = excel.UseWorkBook(new WorkbookOptions() {Path = myFile, CreateNew = true, ConflictBehavior = ConflictBehavior.Replace}))
            {
                wb.WriteCell("newSheet1", "A3", "someText");
                wb.WriteCell("newSheet2", "A3", "someText");
                PrintSheets(wb);
            }
            
            using (var wb = excel.UseWorkBook(new WorkbookOptions() {Path = myFile, CreateNew = true, ConflictBehavior = ConflictBehavior.Skip}))
            {
                wb.WriteCell("newSheet3", "A3", "someText");
                wb.WriteCell("newSheet4", "A3", "someText");
                PrintSheets(wb);
            }
            
            try
            {
                using var wb = excel.UseWorkBook(new WorkbookOptions() {Path = myFile, CreateNew = true, ConflictBehavior = ConflictBehavior.Fail});
            }
            catch
            {
                //Exception expected here because the file already exists
                Console.WriteLine("Exception: File already exists");
            }
            
            
            //Write cell in every sheet and check that what was written is ok
            using (var wb = excel.UseWorkBook(new WorkbookOptions() {Path = myFile, CreateNew = true, ConflictBehavior = ConflictBehavior.Skip}))
            {
                Console.WriteLine("Write some value in every sheet and check that value was written properly");
                var sheets = wb.GetSheets();
                foreach(var sheet in sheets)
                {
                    var cellValue = wb.ReadCell(sheet, "B7", true) as string;
                    if (!string.IsNullOrEmpty(cellValue))
                        Console.WriteLine("empty value expected in {sheet}:B7");
                    
                    wb.WriteCell(sheet, "B7", "ltcm");
                }
                
                foreach(var sheet in sheets)
                {
                    var cellValue = wb.ReadCell(sheet, "B7", true) as string;
                    if (cellValue != "ltcm")
                        Console.WriteLine("ltcm is expected in {sheet}:B7");
                }
            }
        }
        
        private void PrintSheets(IWorkHandle wb)
        {
            Console.WriteLine("enumerate sheets");
            var sheets = wb.GetSheets();
            foreach(var sheet in sheets)
            {
                Console.WriteLine(sheet);
            }
        }
    }
}