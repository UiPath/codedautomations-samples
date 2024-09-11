using PowerPointCoded.ObjectRepository;
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
using System.Reflection;

namespace PowerPointCoded
{
    public class FormatSlideContent : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            // To start using services, use IntelliSense (CTRL + Space) to discover the available services:
            // e.g. system.GetAsset(...)

            // For accessing UI Elements from Object Repository, you can use the Descriptors class e.g:
            // var screen = uiAutomation.Open(Descriptors.MyApp.FirstScreen);
            // screen.Click(Descriptors.MyApp.FirstScreen.SettingsButton);
                   using (var pow = powerpoint.UsePowerPointPresentation("New Microsoft PowerPoint Presentation (2).pptx"))
            {
                var fontChangeType = typeof(FontSizeModificationModel);
                object [] paramValues = new object[] { 60 };
                Type[] paramTypes = new Type[] { typeof(int) };
                IFormatSlideModicationModel modification =fontChangeType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,null, paramTypes, null).Invoke(paramValues) as IFormatSlideModicationModel;
                var modList = new List<IFormatSlideModicationModel> {modification};
                                  
                pow.FormatSlideContent(1,"Title 1",modList);
            }
        }
    }
}