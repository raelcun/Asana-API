using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaAPI.Data
{
    public class Task
    {
        public string Assignee { get; set; }
        public string AssigneeStatus { get; set; }
        public string CreatedAt { get; set; }
        public string Completed { get; set; }
        public string CompletedAt { get; set; }
        public string DueOn { get; set; }
        public string Followers { get; set; }
        public string ModifiedAt { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public string Projects { get; set; }
        public string Parent { get; set; }
        public string Workspace { get; set; }

        public override string ToString()
        {
            return this.ListVars(false, false, 0);
        }
    }
}
