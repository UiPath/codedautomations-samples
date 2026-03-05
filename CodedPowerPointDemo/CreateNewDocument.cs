using PowerPointCoded.ObjectRepository;
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
using UiPath.Presentations;
using UiPath.Presentations.Activities;
using UiPath.Presentations.Activities.API;
using UiPath.Presentations.Activities.API.Models;
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

namespace PowerPointCoded
{
    public class CreateNewDocument : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            //Test creation of the files (new document)
            
            var myFile = "myNewPptFile.pptx";
            if (File.Exists(myFile))
                File.Delete(myFile);
                        
            using (var ppt = powerpoint.UsePresentationDocument(new PresentationCreateOptions() {Path = myFile, CreateNew = true, ConflictResolution = ConflictBehavior.Replace  }))
            {
                
            }
            
            Console.WriteLine($"File {myFile} created: {File.Exists(myFile)}");
            
            using (var ppt = powerpoint.UsePresentationDocument(new PresentationCreateOptions() {Path = myFile,  ConflictResolution = ConflictBehavior.Replace  }))
            {
                ppt.AddNewSlide("Blank");
            }
            
            //Use template
            using (var ppt = powerpoint.UsePresentationDocument(new PresentationCreateOptions() {Path = myFile,  ConflictResolution = ConflictBehavior.Replace, TemplatePath = "New Microsoft PowerPoint Presentation.pptx"  }))
            {
                ppt.AddNewSlide("Blank");
            }
            
            //Use existing
            using (var ppt = powerpoint.UsePresentationDocument(new PresentationCreateOptions() {Path = myFile,  ConflictResolution = ConflictBehavior.Skip  }))
            {
                ppt.AddNewSlide("Blank");
            }
            
            try
            {
                using var ppt = powerpoint.UsePresentationDocument(new PresentationCreateOptions() {Path = myFile,  ConflictResolution = ConflictBehavior.Fail  });
            }
            catch
            {
                Console.WriteLine("Expected exception due to conflict resolution");
            }
        }
    }
}