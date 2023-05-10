using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UiPath.CodedWorkflows;

// NOTICE: The Coded Workflows feature is available as a Public Preview and APIs may be subject to change. 
// Additionally, no warranty or technical support is provided for this preview feature.
// Missing features or encountering bugs? Please click the feedback button in the top-right corner and let us know!
// For more information, please visit the documentation over at https://docs.uipath.com/studio/docs/coded-workflows.
// Feel free to delete these comments after you've read and acknowledged them.
namespace BlankProcess32
{
    public class PythonWorkflowUtils
    {
        private readonly Action<string> invokeWorkflow;
		private readonly Action<string> log;

        public PythonWorkflowUtils(Action<string> invokeWorkflow, Action<string> log)
		{
            this.invokeWorkflow = invokeWorkflow;
			this.log = log;
        }
		
		public void RunWorkflow(string workflowPath)
		{
			invokeWorkflow(workflowPath);
		}
		
		public void Log(string text)
		{
			log(text);
		}
    }
}