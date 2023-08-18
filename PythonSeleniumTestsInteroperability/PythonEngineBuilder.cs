using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Scripting.Hosting;

// NOTICE: The Coded Workflows feature is available as a Public Preview and APIs may be subject to change. 
// Additionally, no warranty or technical support is provided for this preview feature.
// Missing features or encountering bugs? Please click the feedback button in the top-right corner and let us know!
// For more information, please visit the documentation over at https://docs.uipath.com/studio/docs/coded-workflows.
// Feel free to delete these comments after you've read and acknowledged them.
namespace PythonSeleniumTestsInteroperability
{
    public static class PythonEngineBuilder
    {
        public static ScriptEngine Build()
		{
			var pyEngine = IronPython.Hosting.Python.CreateEngine();
			pyEngine.SetSearchPaths(new[] { 
				@"C:\Program Files\IronPython 3.4\Lib",
			    @"C:\Program Files\IronPython 3.4\Lib\site-packages"
			});
			return pyEngine;
		}
    }
}