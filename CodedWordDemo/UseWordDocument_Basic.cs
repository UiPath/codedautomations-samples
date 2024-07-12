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
    public class UseWordDocument_Basic : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            //Retrieve word file handle using "word" service and default options
            using (var w = word.UseWordDocument("CodedTestFiles/wCodedTestsWindowsDocument.docx"))
            {
                var text = w.ReadText();
                Console.WriteLine(text);
                // should be -> Testing the Word

                w.AppendText(" CodedWorkflows API!", false);
                text = w.ReadText();
                Console.WriteLine(text);
                // should be -> Testing the Word CodedWorkflows API!

                w.AppendText("And everything seems just bad");
                text = w.ReadText();
                Console.WriteLine(text);
                // should be -> Testing the Word CodedWorkflows API!\r\nAnd everything seems just bad

                w.ReplaceTextInDocument("bad", "fine");
                text = w.ReadText();
                Console.WriteLine(text);
                // should be -> Testing the Word CodedWorkflows API!\r\nAnd everything seems just fine
            }
        }
    }
}