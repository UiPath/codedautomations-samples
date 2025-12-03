using System;
using System.Collections.Generic;
using System.Data;
using UiPath.CodedWorkflows;
using UiPath.Core;
using UiPath.Core.Activities.Storage;
using MongoDB.Driver;

namespace MongoDBCodedWorkflowsSample
{
    public class TestConnection : CodedWorkflow
    {
        [Workflow]
        public List<string> Execute(string username, string password, string cluster)
        {
            List<string> databaseNames = new List<string>();

            try
            {
                // Create MongoDB Atlas connection string
                string connectionString = $"mongodb+srv://{username}:{password}@{cluster}/?retryWrites=true&w=majority";

                Console.WriteLine("Connecting to MongoDB Atlas...");

                // Initialize MongoDB client
                var client = new MongoClient(connectionString);

                // List all databases to verify connection
                Console.WriteLine("\nAvailable databases:");
                databaseNames = client.ListDatabaseNames().ToList();
                foreach (var dbName in databaseNames)
                {
                    Console.WriteLine($"- {dbName}");
                }

                Console.WriteLine("\nConnection successful!");
            }
            catch (MongoAuthenticationException authEx)
            {
                Console.WriteLine($"Authentication error: {authEx.Message}");
                Console.WriteLine("Please verify your username and password.");
                throw;
            }
            catch (MongoConnectionException connEx)
            {
                Console.WriteLine($"Connection error: {connEx.Message}");
                Console.WriteLine("Please check your network access settings in MongoDB Atlas.");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MongoDB connection error: {ex.Message}");
                throw;
            }

            return databaseNames;
        }
    }
}