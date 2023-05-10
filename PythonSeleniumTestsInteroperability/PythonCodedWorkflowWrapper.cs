using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Scripting.Hosting;

// NOTICE: The Coded Automations feature is available as a preview feature and APIs may be subject to change. 
//         No warranty or technical support is provided for this preview feature.
//         Missing features or encountering bugs? Please click the feedback button in the top-right corner and let us know!
// Please delete these comments after you've read and acknowledged them. For more information, please visit the documentation over at https://docs.uipath.com/studio/lang-en/v2023.4/docs/coded-automations.
namespace BlankProcess32
{
    public class PythonCodedWorkflowWrapper : CodedWorkflow
    {
		protected Lazy<PythonWorkflowUtils> _workflowUtils;
		private Lazy<ScriptEngine> _engine;
		
		public PythonCodedWorkflowWrapper()
		{
			_workflowUtils = new Lazy<PythonWorkflowUtils>(() => new PythonWorkflowUtils(a => RunWorkflow(a), a => Log(a)));
			_engine = new Lazy<ScriptEngine>(() => PythonEngineBuilder.Build());
		}
		
		protected void Close()
		{
			_engine.Value.Runtime.Shutdown();
		}
		
        protected IDictionary<string, dynamic> RunPythonScript(string filePath, IDictionary<string, object> variables, IEnumerable<string> outputVariableNames = null, bool closePythonEngine = true)
		{
			var pyEngine = PythonEngineBuilder.Build();
			
			var scope = pyEngine.CreateScope();
			
			foreach (var variable in variables)
			{
				scope.SetVariable(variable.Key, variable.Value);
			}
			
			scope.SetVariable("workflowUtils", _workflowUtils.Value);

			var outScope = pyEngine.ExecuteFile(Path.Combine(Environment.CurrentDirectory, filePath), scope);
			
			var outputs = new Dictionary<string, object>();
			if (outputVariableNames != null)
			{
				foreach (var outVar in outputVariableNames)
				{
					outputs.Add(outVar, outScope.GetVariable(outVar));
				}
			}
			
			Close();
			
			return outputs;
		}
    }
}