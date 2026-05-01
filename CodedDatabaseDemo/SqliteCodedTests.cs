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
using System.Activities;

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
            
            //Use Sqlite database
            using var dbCon = database.UseConnection(new DatabaseScopeOptions() { ProviderName = "Microsoft.Data.Sqlite", ConnectionString = "Data Source=mySqlite.db" });

            //Check if table exists
            //We use a query with params to avoid SQL injection
            //Notice that the syntax might differ for other providers (eg. Oracle or Sql Server)
            //Please check what is the syntax for the provider
            var (dt, ds) = dbCon.ExecuteQuery("SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name=@tableName",
                new Dictionary<string, ParameterInfo>() {
                    {"tableName", new ParameterInfo() {Direction = ArgumentDirection.In, Type = typeof(string), Value = tableName}}
                }, TimeSpan.FromSeconds(5)
                , CommandType.Text);

            bool exists = Convert.ToInt64(dt.Rows[0][0]) > 0;
            Console.WriteLine($"initial table {tableName} exists {exists}");

            //Recreate table if does not exist
            if (!exists)
            {
                //Here we can't use params, so we are constructing the query -> risk of SQL injection
                dbCon.Execute($"CREATE TABLE {tableName} ( ID INTEGER PRIMARY KEY, NAME TEXT, CREATED_AT TEXT DEFAULT CURRENT_TIMESTAMP)",
                default, default
                );
            }

            //Check again if table exists
            (dt, ds) = dbCon.ExecuteQuery("SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name=@tableName",
                new Dictionary<string, ParameterInfo>() {
                    {"tableName", new ParameterInfo() {Direction = ArgumentDirection.In, Type = typeof(string), Value = tableName}}
                }, default);
            exists = Convert.ToInt64(dt.Rows[0][0]) > 0;
            Console.WriteLine($"after creation - table {tableName} exists {exists}");

            //Get the number of rows in the table
            //Again - we can't use params, so we are constructing the query -> risk of SQL injection
            (dt, ds) = dbCon.ExecuteQuery($"SELECT COUNT(*) FROM {tableName}", default, default);
            var firstId = long.Parse(dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : "0");
            
            Console.WriteLine($"rows before the insertion {firstId}");

            //create new datatable and populate it with some data
            var ndt = new System.Data.DataTable("dt");

            // Add columns
            ndt.Columns.Add("ID", typeof(decimal));
            ndt.Columns.Add("NAME", typeof(string));
            ndt.Columns.Add("CREATED_AT", typeof(System.DateTime));
            
            for (var i = firstId+1; i <= firstId+10; i++)
            {
                ndt.Rows.Add(new object[] { i , Guid.NewGuid().ToString("N"), DateTime.Now});
            }
            
            //Insert the datatable
            dbCon.BulkInsertDataTable(tableName, ndt);
            
            //Check that the insertion actually took place
            (dt, ds) = dbCon.ExecuteQuery($"SELECT COUNT(*) FROM {tableName}", default, default);            
            Console.WriteLine($"rows after the insertion {long.Parse(dt.Rows[0][0].ToString())}");
        }
    }
}