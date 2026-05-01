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
using UiPath.FTP;
using UiPath.FTP.Activities;
using UiPath.FTP.Activities.API;
using UiPath.FTP.Activities.API.Models;
using UiPath.Orchestrator.Client.Models;
using System.Linq;

namespace CodedFTPDemo
{
    public class CodedTests : CodedWorkflow
    {
        [Workflow]    
        public async System.Threading.Tasks.Task ExecuteAsync()
        {
            // To start using services, use IntelliSense (CTRL + Space) to discover the available services:
            // e.g. system.GetAsset(...)

            // For accessing UI Elements from Object Repository, you can use the Descriptors class e.g:
            // var screen = uiAutomation.Open(Descriptors.MyApp.FirstScreen);
            // screen.Click(Descriptors.MyApp.FirstScreen.SettingsButton);
            
            //create a ftp session using proper credentials
            string host = "Ubuntu64";
            string user = "ftpuser";
            string pass = "somepass";
            
            await using var session = await ftp.UseFtpSession(new FtpScopeOptions() {
                Host = host,
                Username = user,
                Password = pass,
                UseSftp = true
            });
            
            var fileName = "1.txt";
            var exists = await session.FileExists(fileName);
            Console.WriteLine($"file {fileName} exists {exists}");
            
            fileName = "/home/ftpuser/image.png";
            exists = await session.FileExists(fileName);
            Console.WriteLine($"file {fileName} exists {exists}");
            
            Console.WriteLine("List all files in the current remote directory");
            var files = await session.EnumerateObjects(".", true);
            var filesList = files.ToList();
            foreach (var f in filesList)
            {
                Console.WriteLine($"{f.FullName}");
            }
            
            Console.WriteLine($"we found {filesList.Count(x => x.Type == FtpObjectType.Directory)} directories");
            Console.WriteLine($"we found {filesList.Count(x => x.Type == FtpObjectType.Link)} links");
            
            //download some file from the FTP server
            await session.DownloadFiles("/home/ftpuser/image.png", "remoteImg.png", true);
        }
        
    }
}