using System;
using System.Collections.Generic;
using System.Data;
using UiPath.CodedWorkflows;
using UiPath.Core;
using UiPath.Core.Activities.Storage;
using UiPath.Orchestrator.Client.Models;
using UiPath.Testing;
using UiPath.Testing.Activities.TestData;
using UiPath.Testing.Activities.TestDataQueues.Enums;
using UiPath.Testing.Enums;
using UiPath.UIAutomationNext.API.Contracts;
using UiPath.UIAutomationNext.API.Models;
using UiPath.UIAutomationNext.Enums;
using Microsoft.VisualBasic;
using System.Text;
using System.IO;

// NOTICE: The Coded Automations feature is available as a preview feature and APIs may be subject to change. 
//         No warranty or technical support is provided for this preview feature.
//         Missing features or encountering bugs? Please click the feedback button in the top-right corner and let us know!
// Please delete these comments after you've read and acknowledged them. For more information, please visit the documentation over at https://docs.uipath.com/studio/lang-en/v2023.4/docs/coded-automations.
namespace CodedAutomationDemo
{
    public class GenerateTestData : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            string prompt = "How many data records do you need?";
			string title = "Test data generator";
			int numberOfRecords = Int32.Parse(Interaction.InputBox(prompt, title, "1"));
			
			
			// Create a new DataTable
			DataTable dataTable = new DataTable("TestData");
			
			// Add columns to the table
			dataTable.Columns.Add("FirstName", typeof(string));
			dataTable.Columns.Add("LastName", typeof(string));
			dataTable.Columns.Add("BirthDate", typeof(DateTime));
			dataTable.Columns.Add("Country", typeof(string));
			dataTable.Columns.Add("ZipCode", typeof(string));
			dataTable.Columns.Add("City", typeof(string));
			dataTable.Columns.Add("StreetName", typeof(string));
			dataTable.Columns.Add("StreetNumber", typeof(string));
			dataTable.Columns.Add("Income", typeof(int));
			
			for(int i =1; i<= numberOfRecords; i++){
				
				string firstName = testing.GivenName();
				string lastName = testing.LastName();
				DateTime birtDate = testing.RandomDate(DateTime.Now.AddYears(-100),DateTime.Now.AddYears(-18));
				var address = testing.Address("<Random Country>","<Random City>");
				string country = address["Country"];
				string zipCode = address["PostalCode"];
				string city = address["City"];
				string streetName = address["StreetName"];
				string streetNumber = address["StreetNumber"];
				int income = (int)testing.RandomNumber(10000,50000);			
				
				Console.WriteLine("Adding row: "+i);
				dataTable.Rows.Add(firstName, lastName, birtDate, country, zipCode, city, streetName, streetNumber, income);
				
			}
			
			SaveTestData(dataTable,"testdata.csv");
		
		}
		
		public static void SaveTestData(DataTable dataTable, string filePath)
		{
		    StringBuilder sb = new StringBuilder();
		
		    // Write header row
		    for (int i = 0; i < dataTable.Columns.Count; i++)
		    {
		        sb.Append(dataTable.Columns[i].ColumnName);
		        if (i < dataTable.Columns.Count - 1)
		            sb.Append(",");
		    }
		    sb.AppendLine();
		
		    // Write data rows
		    foreach (DataRow row in dataTable.Rows)
		    {
		        for (int i = 0; i < dataTable.Columns.Count; i++)
		        {
		            sb.Append(row[i].ToString());
		            if (i < dataTable.Columns.Count - 1)
		                sb.Append(",");
		        }
		        sb.AppendLine();
		    }
		
		    // Save the CSV file
		    File.WriteAllText(filePath, sb.ToString());
		}
    }
}