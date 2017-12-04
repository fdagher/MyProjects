using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pickles.Integration.Core.Logging;

namespace TestNuget
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger("", "", "", "", "");
            logger.Monitor("Enter", "{}", "SelfInspect", "TestNuget.Main");
        }
    }
}
