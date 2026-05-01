using CredentialManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security;
using UiPath.CodedWorkflows;
using UiPath.Core;
using UiPath.Core.Activities.Storage;
using UiPath.Credentials.Activities;
using UiPath.Credentials.Activities.API;
using UiPath.Credentials.Activities.API.Models;
using UiPath.Orchestrator.Client.Models;

namespace CodedCredentialsDemo
{
    public class CodedCredentialsT : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            // To start using services, use IntelliSense (CTRL + Space) to discover the available services:
            // e.g. system.GetAsset(...)

            // For accessing UI Elements from Object Repository, you can use the Descriptors class e.g:
            // var screen = uiAutomation.Open(Descriptors.MyApp.FirstScreen);
            // screen.Click(Descriptors.MyApp.FirstScreen.SettingsButton);
            
            string target = "someTarget";
            string user = "someUser";
            string password = "somePassword";
            
            var added = credentials.AddCredential(target, user, password, CredentialType.Generic, PersistanceType.LocalComputer);
            Console.WriteLine($"added credential for target {target}: {added}");
            
            var cred = credentials.GetSecureCredential(target, CredentialType.Generic, PersistanceType.LocalComputer);
            Console.WriteLine($"retrieved credential: {nameof(cred.Found)} {cred.Found}, {nameof(cred.Username)} {cred.Username}, {nameof(cred.Password)} {cred.Password}");
            //credentials.DeleteCredential(target);
            
            var reqCred = credentials.RequestCredential("some message", "some title");
            
            Console.WriteLine($"retrieved credential: {nameof(reqCred.Confirmed)} {reqCred.Confirmed}, {nameof(reqCred.Username)} {reqCred.Username}, {nameof(reqCred.Password)} {reqCred.Password}");
            
            var delCred = credentials.DeleteCredential(target);
            
            Console.WriteLine("deleted credential for target {target}: {delCred}");
        }
    }
}