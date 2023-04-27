using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using QueueDefinitions;
using UiPath.CodedWorkflows;
using UiPath.Core;

// NOTICE: The Coded Automations feature is available as a preview feature and APIs may be subject to change. 
//         No warranty or technical support is provided for this preview feature.
//         Missing features or encountering bugs? Please click the feedback button in the top-right corner and let us know!
// Please delete these comments after you've read and acknowledged them. For more information, please visit the documentation over at https://docs.uipath.com/studio/lang-en/v2023.4/docs/coded-automations.
namespace RandomQueueGenerator
{
  public class GenerateQueue : CodedWorkflow
  {
    [Workflow]
    public void Execute()
    {
      // Create a new queue (based on the service created from the Orchestrator's swagger.json)
      var client = BuildClient();
      var queueClient = new QueueDefinitionsClient(client);

      var queueName = "SampleQueue" + Guid.NewGuid().ToString("N");
      var queue = queueClient.PostAsync(new QueueDefinitionDto() { Name = queueName }, null).Result;

      Log(queue.Id.ToString());

      Parallel.ForEach(Enumerable.Range(0, 100), i =>
      {
        var data = new Dictionary<string, object>()
        {
          { "Address", testing.Address("Romania", "Bucharest")["City"].ToString() },
          { "FirstName", testing.GivenName() },
          { "LastName", testing.LastName() }
        };

        system.AddQueueItem(queueName, null, DateTime.UtcNow, data, DateTime.UtcNow, QueueItemPriority.Normal, i.ToString(), 15000);
      });
    }

    private HttpClient BuildClient()
    {
      // Uses a sync implementation for simplification, can be converted to async/await.
      var orchestratorUrl = executorRuntime.AccessProvider.GetResourceUrl("Orchestrator").Result;
      var orchestratorToken = executorRuntime.AccessProvider.GetAccessToken("Orchestrator", true).Result;

      var client = new HttpClient();
      client.DefaultRequestHeaders.Add("Authorization", "Bearer " + orchestratorToken);

      foreach (var header in workflowRuntime.OrchestratorSettings.GetHeaders())
      {
        client.DefaultRequestHeaders.Add(header.Key, header.Value);
      }

      return client;
    }
  }
}