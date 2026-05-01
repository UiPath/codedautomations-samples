using System;
using System.Collections.Generic;
using System.Data;
using UiPath.CodedWorkflows;
using UiPath.Core;
using UiPath.Core.Activities.Storage;
using UiPath.Database;
using UiPath.Database.Activities;
using UiPath.Database.Activities.API;
using UiPath.Database.Activities.API.Models;
using UiPath.Orchestrator.Client.Models;

namespace DatabaseCodedDemo
{
    public class SqliteCodedTests : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            // To start using services, use IntelliSense (CTRL + Space) to discover the available services:
            // e.g. system.GetAsset(...)

            // For accessing UI Elements from Object Repository, you can use the Descriptors class e.g:
            // var screen = uiAutomation.Open(Descriptors.MyApp.FirstScreen);
            // screen.Click(Descriptors.MyApp.FirstScreen.SettingsButton);

            string tableName = "MYTESTTABLE";

            using var dbCon = database.UseConnection(new DatabaseScopeOptions() { ProviderName = "Microsoft.Data.Sqlite", ConnectionString = "Data Source=mySqlite.db" });

            //Check if table exists
            var (dt, ds) = dbCon.ExecuteQuery(string.Format("SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='{0}'", tableName),
                default, default);

            bool exists = Convert.ToInt64(dt.Rows[0][0]) > 0;
            Console.WriteLine($"initial table {tableName} exists {exists}");

            if (!exists)
            {
                dbCon.Execute(string.Format("CREATE TABLE {0} ( ID INTEGER PRIMARY KEY, NAME TEXT, CREATED_AT TEXT DEFAULT CURRENT_TIMESTAMP)", tableName),
                      default, TimeSpan.FromSeconds(10)
                  );
            }

            //Check again if table exists
            (dt, ds) = dbCon.ExecuteQuery(string.Format("SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='{0}'", tableName),
                default, default);
            exists = Convert.ToInt64(dt.Rows[0][0]) > 0;
            Console.WriteLine($"after creation - table {tableName} exists {exists}");

            //insert some data into the table
            (dt, ds) = dbCon.ExecuteQuery(string.Format("SELECT COUNT(*) FROM {0}", tableName), default, default);
            var firstId = int.Parse(dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : "0");
            
            Console.WriteLine($"rows before the insertion {firstId}");

            //create new datatable 
            // Create DataTable
            var ndt = new System.Data.DataTable("dt");

            // Add columns
            ndt.Columns.Add("ID", typeof(decimal));
            ndt.Columns.Add("NAME", typeof(string));
            ndt.Columns.Add("CREATED_AT", typeof(System.DateTime));
            
            for (int i = firstId+1; i <= firstId+10; i++)
            {
                ndt.Rows.Add(new object[] { i , Guid.NewGuid().ToString("N"), DateTime.Now});
            }
            
            dbCon.BulkInsertDataTable(tableName, ndt);
            
            (dt, ds) = dbCon.ExecuteQuery(string.Format("SELECT COUNT(*) FROM {0}", tableName), default, default);            
            Console.WriteLine($"rows after the insertion {int.Parse(dt.Rows[0][0].ToString())}");
        }
    }
}