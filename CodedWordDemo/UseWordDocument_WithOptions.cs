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
    public class UseWordDocument_WithOptions : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            //Retrieve the Word file handle using "word" service and some default options
            //Changes won't be saved after the execution because AutoSave is set to false
            using (var w = word.UseWordDocument(new WordUseOptions() { Path = "CodedTestFiles/docWithTextAndImage.docx", AutoSave = false }))
            {
                var text = w.ReadText();
                Console.WriteLine(text);
                // should be -> Test for Add/Replace image;

                // Add a picture at the end of the document
                w.AddPicture("CodedTestFiles/imageToInsert.png");

                // Replace it with another one
                w.ReplacePicture("imageToReplace", "CodedTestFiles/testImage.png");
            }
        }
    }
}