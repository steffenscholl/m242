using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProject.Shared
{
    public class branchListsModel
    {
        public IEnumerable<GitHubBranch> ListBranches { get; set; }
    }
}
