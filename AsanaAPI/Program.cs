using System;
using System.Collections.Generic;

namespace AsanaAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://app.asana.com/api/1.0/projects";
            string workspaceID = "4153074572416"; // Work
            string taskID = "4245890110753"; // Test History Sourcing
            string subtaskID = "4305274408810"; // Verify LineDiscountPercentage

            //List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            //parameters.Add(new KeyValuePair<string, string>("name", "My New Workspace"));
            //parameters.Add(new KeyValuePair<string, string>("workspace", "4153074572416"));

            AsanaAPI api = new AsanaAPI();
            //System.Diagnostics.Trace.WriteLine(api.GetAllUsers().AsString(2));
            System.Diagnostics.Trace.WriteLine(api.GetSingleUser());
            Console.ReadKey();
        }
    }
}
