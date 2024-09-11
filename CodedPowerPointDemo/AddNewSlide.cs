using System;
using System.Collections.Generic;
using System.Data;
using PowerPointCoded.ObjectRepository;
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
using UiPath.Testing.Activities.TestData;
using UiPath.Testing.Activities.TestDataQueues.Enums;
using UiPath.Testing.Enums;
using UiPath.UIAutomationNext.API.Contracts;
using UiPath.UIAutomationNext.API.Models;
using UiPath.UIAutomationNext.Enums;


namespace PowerPointCoded
{
    public class AddNewSlide : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            // To start using services, use IntelliSense (CTRL + Space) to discover the available services:
            // e.g. system.GetAsset(...)
            // For accessing UI Elements from Object Repository, you can use the Descriptors class e.g:
            // var screen = uiAutomation.Open(Descriptors.MyApp.FirstScreen);
            // screen.Click(Descriptors.MyApp.FirstScreen.SettingsButton);
            // Code Generation:
            // define a new enum of type string
            // 
            using (var pow = powerpoint.UsePowerPointPresentation("New Microsoft PowerPoint Presentation.pptx"))
            {
                pow.AddNewSlide(ConstClass2.TitleOnly, InsertPositionType.Beginning, "Office Theme");
                pow.AddNewSlide(ConstClass2.TitleAndContent,InsertPositionType.End,"Office Theme" );
            }
        }

    }
}