using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitAdd
{
    class Program
    {
        static void Main(string[] args)
        {
            GitRepositoryManager manager = new GitRepositoryManager("fadid01", "fadi@004", "https://code.it.pickles.com.au/scm/cldint/boomicicd.git", @"C:\Source\POCs\boomicicd");

            bool result = manager.CommitAllChanges("commit by tool");

            if (result)
            {
                manager.PushCommits("code.it.pickles.com.au", @"refs/heads/master");
            }
        }
    }
}
