using CodedWordDemo.ObjectRepository;
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
using UiPath.Mail.Activities.Api;
using UiPath.Orchestrator.Client.Models;
using UiPath.Testing;
using UiPath.Testing.Activities.TestData;
using UiPath.Testing.Activities.TestDataQueues.Enums;
using UiPath.Testing.Enums;
using UiPath.UIAutomationNext.API.Contracts;
using UiPath.UIAutomationNext.API.Models;
using UiPath.UIAutomationNext.Enums;
using UiPath.Word;
using UiPath.Word.Activities;
using UiPath.Word.Activities.API;
using UiPath.Word.Activities.API.Models;

namespace CodedWordDemo
{
    public class UseWordDocument_InsertDataTable : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            using (var w = word.UseWordDocument(new WordUseOptions() { Path = "CodedTestFiles/dummyFileWithBookmark.docx", AutoSave = false }))
            {
                // Build a basic data table 
                var dt = new DataTable("table1");
                dt.Columns.Add("C1");
                dt.Columns.Add("C2");
                var row = dt.NewRow();
                row["C1"] = "abc";
                row["C2"] = "42";

                // Will insert the data table at the end of the document by default
                w.InsertDataTableInDocument(dt);

                // Will insert the data table at the start of the document
                w.InsertDataTableInDocument(dt, UiPath.Word.Position.Start);

                // Will insert the data table after the bookmark
                w.InsertDataTableInDocument(dt, "some_bookmark", UiPath.Word.Position.After);

                var text = w.ReadText();
                var result = text.Contains("random");
                Console.WriteLine(result); // should be true
                
                // Will insert the data table by replacing the last occurence of the string "random"
                w.InsertDataTableInDocument(dt, "random", Occurrence.Last, 1, UiPath.Word.Position.Replace);
                text = w.ReadText();
                result = text.Contains("random");
                Console.WriteLine(result); // should be false
            }
        }
    }
}