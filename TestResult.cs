using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.FluentAutomation
{
    public class TestResult
    {
        public string Name { get; set; }

        public bool Passed { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }
    }
}
