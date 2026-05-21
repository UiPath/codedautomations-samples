using System;
using System.Collections.Generic;
using System.Data;
using UiPath.Activities.System.Jobs.Coded;
using UiPath.CodedWorkflows;
using UiPath.Core;
using UiPath.Core.Activities.Storage;
using UiPath.Java;
using UiPath.Java.Activities;
using UiPath.Java.Activities.API;
using UiPath.Java.Activities.API.Models;
using UiPath.Orchestrator.Client.Models;

namespace Java
{
    public class JavaCodedTests : CodedWorkflow
    {
                
        [Workflow]
        public async System.Threading.Tasks.Task ExecuteAsync()
        {
            // To start using services, use IntelliSense (CTRL + Space) to discover the available services:
            // e.g. system.GetAsset(...)

            // For accessing UI Elements from Object Repository, you can use the Descriptors class e.g:
            // var screen = uiAutomation.Open(Descriptors.MyApp.FirstScreen);
            // screen.Click(Descriptors.MyApp.FirstScreen.SettingsButton);
            
            Console.WriteLine("execution begin");
            
            //Initialize java scope and load Jar
            //Replace with valid JDK path
            await using var js = await java.UseJavaScope(new JavaScopeOptions() {JavaPath = @$"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\Eclipse Adoptium\jdk-25.0.2.10-hotspot"});
            await js.LoadJar("Objects.jar");
            
            
            //Invoke static method
            var JavaObjectResultStaticMethod = await js.InvokeStaticMethod("getArrayInt", "uipath.java.test.StaticMethods");
            var arr = js.ConvertObject<int[]>(JavaObjectResultStaticMethod);
            foreach (var r in arr)
            {
                Console.WriteLine($"ConvertObject returned {r}");
            }
            
            //Invoke instance method
            //Great care here if you specify the types (it should match with the objects)!!!!
            var javaObject = await js.CreateObject("uipath.java.test.Coordinate", [200D, 5.5F], [typeof(double), typeof(float)]);
            var javaObjectResultObjectMethod = await js.InvokeMethod("getCoordinateSum", javaObject);
            var objectMethodResultValue = js.ConvertObject<double>(javaObjectResultObjectMethod);
            
            Console.WriteLine($"instance method returned {objectMethodResultValue}");
            
            {
                //no types specified, they are deduced
                var javaObject2 = await js.CreateObject("uipath.java.test.Coordinate", [100D, 50.5]);
                var javaObjectResultObjectMethod2 = await js.InvokeMethod("getCoordinateSum", javaObject2);
                var objectMethodResultValue2 = js.ConvertObject<double>(javaObjectResultObjectMethod2);
            
                Console.WriteLine($"instance method (second call) returned {objectMethodResultValue2}");
            }
        }
    }
}