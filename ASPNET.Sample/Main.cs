using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ASPSample.ObjectRepository;
using Microsoft.Extensions.DependencyInjection;
using UiPath.CodedWorkflows;
using UiPath.CodedWorkflows.Interfaces;
using UiPath.Core;
using UiPath.Core.Activities.API;
using UiPath.Core.Activities.Storage;
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

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace ASPSample
{
    public class Workflow : CodedWorkflow
    {
        [Workflow]
        public async Task Execute()
        {
            var builder = WebApplication.CreateBuilder();
            builder.WebHost.UseUrls("http://localhost:5005");
            var app = builder.Build();
            app.MapGet("/", () => "Hello World!");
            await Task.Run(() => app.Run());

        }
    }
}