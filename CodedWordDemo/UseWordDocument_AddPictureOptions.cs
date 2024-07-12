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
    public class UseWordDocument_AddPictureOptions : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            using (var w = word.UseWordDocument(new WordUseOptions() { Path = "CodedTestFiles/dummyFileWithBookmark.docx", AutoSave = false }))
            {
                // Add a picture by default at the end of the document
                w.AddPicture("CodedTestFiles/imageToInsert.png");

                // Add a picture by mentioning the position relative to the document
                w.AddPicture("CodedTestFiles/imageToInsert.png", UiPath.Word.Position.Start);

                // Add a picture relative to some bookmark set inside the document
                w.AddPicture("CodedTestFiles/imageToInsert.png", "some_bookmark", UiPath.Word.Position.Before);

                // Add a picture relative to a string found insid the document, mentioning how many occurences to search for and repeat the action
                w.AddPicture("CodedTestFiles/imageToInsert.png", "random", Occurrence.First, null, UiPath.Word.Position.After);
            }
        }
    }
}