using System.Collections.Generic;
using SimpleJSON;

namespace AsanaAPI
{
    public class User
    {
        public class Workspace
        {
            public string ID { get; set; }
            public string Name { get; set; }
        }

        public string ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Workspace> Workspaces { get; private set; }

        public User()
        {
            this.Workspaces = new List<Workspace>();
        }

        public static User Parse(JSONNode root)
        {
            User user = new User();

            user.ID = root["id"];
            user.Name = root["name"];
            user.Email = root["email"];
            foreach(JSONNode node in root["workspaces"].AsArray)
            {
                Workspace workspace = new Workspace();
                workspace.ID = node["id"];
                workspace.Name = node["name"];
                user.Workspaces.Add(workspace);
            }

            return user;
        }

        public override string ToString()
        {
            return this.ListVars(false, false, 2);
        }
    }
}
