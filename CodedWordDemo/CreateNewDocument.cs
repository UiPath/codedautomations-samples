using CodedWordDemo.ObjectRepository;
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
using UiPath.Word;
using UiPath.Word.Activities;
using UiPath.Word.Activities.API;
using UiPath.Word.Activities.API.Models;
using System.IO;

namespace CodedWordDemo
{
    public class CreateNewDocument : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            //Open existing file
            using (var wd = word.UseDocument("CodedTestFiles/docWithTextAndImage.docx"))
            {
                var text = wd.ReadText();
                Console.WriteLine($"Read text {text}");
            }
            
            var myFile = "myNewFile.docx";
            if (File.Exists(myFile))
                File.Delete(myFile);
            
            using (var wd = word.UseDocument(new DocumentOptions() {Path = myFile, CreateNew = true}))
            {
                var text = wd.ReadText();
                Console.WriteLine($"Read text {text}");
            }
            
            Console.WriteLine($"File {myFile} created: {File.Exists(myFile)}");
            
            using (var wd = word.UseDocument(new DocumentOptions() {Path = myFile, CreateNew = true, ConflictBehavior = ConflictBehavior.Replace}))
            {
                var text = wd.ReadText();
                Console.WriteLine($"Read text {text}");
                wd.AppendText($"{System.DateTime.Now}");
            }
            
            using (var wd = word.UseDocument(new DocumentOptions() {Path = myFile, CreateNew = true, ConflictBehavior = ConflictBehavior.Skip}))
            {
                var text = wd.ReadText();
                Console.WriteLine($"Read text {text}");
                wd.AppendText($"{System.DateTime.Now}");
            }
            
            try
            {
              using var wd = word.UseDocument(new DocumentOptions() {Path = myFile, CreateNew = true, ConflictBehavior = ConflictBehavior.Fail});
            }
            catch
            {
                Console.WriteLine("Expected exception here since conflict resolution is set to fail");
            }
        }
    }
}