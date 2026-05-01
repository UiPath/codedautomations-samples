using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using UiPath.Activities.System.Jobs.Coded;
using UiPath.CodedWorkflows;
using UiPath.Core;
using UiPath.Core.Activities.Storage;
using UiPath.Orchestrator.Client.Models;
using UiPath.Python;
using UiPath.Python.Activities;
using UiPath.Python.Activities.API;
using UiPath.Python.Activities.API.Models;

namespace CodedPythonDemo
{
    public class PythonTests : CodedWorkflow
    {
        [Workflow]
        public async System.Threading.Tasks.Task Execute()
        {
            //the above should be valid python installation paths
            using var pyScope = await python.UsePythonScope(new PythonScopeOptions()
            {
                LibraryPath = @$"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\AppData\Local\Programs\Python\Python313\python313.dll",
                Path = @$"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\AppData\Local\Programs\Python\Python313"
            });

            Console.WriteLine("after the creation of the scope");


            //tab formed python code
            var codeInst = await pyScope.LoadCode(@"def sum(a,b):
	return a + b
");

            //Get some random numbers
            Random rand = new Random();
            var n1 = rand.Next(1, 101);
            var n2 = rand.Next(1, 101);
            
            Console.WriteLine($"Generated random numbers");

            //call python script
            var sumResultObj = await pyScope.InvokeMethod(codeInst, "sum", new List<object>() {n1, n2} );

            //Convert to .NET type
            int s = pyScope.GetObject<int>(sumResultObj);
            
            Console.WriteLine($"sum of {n1} and {n2} is {s}");

            Console.WriteLine($"Wait a few seconds");
            Delay(TimeSpan.FromSeconds(3));
           
            Console.WriteLine("Done");
        }
    }
}