using FluentAutomation;
using FluentAutomation.Interfaces;
using ScriptCs.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.FluentAutomation
{
    public class FluentAutomationScript : IScriptPackContext
    {
        internal FluentTest activeTest = null;

        public INativeActionSyntaxProvider Init<T>(string browserName)
        {
            MethodInfo bootstrapMethod = null;
            ParameterInfo[] bootstrapParams = null;

            MethodInfo[] methods = typeof(T).GetMethods(BindingFlags.Static | BindingFlags.Public);
            foreach (var methodInfo in methods)
            {
                if (methodInfo.Name.Equals("Bootstrap"))
                {
                    bootstrapMethod = methodInfo;
                    bootstrapParams = methodInfo.GetParameters();
                    if (bootstrapParams.Length == 1)
                    {
                        break;
                    }
                }
            }

            var browserEnumValue = Enum.Parse(bootstrapParams[0].ParameterType, browserName);
            bootstrapMethod.Invoke(null, new object[] { browserEnumValue });

            this.activeTest = new FluentTest();
            var testProvider = this.activeTest.I;

            return testProvider;
        }

        internal void Terminate()
        {
            if (this.activeTest != null)
                this.activeTest.Dispose();
        }
    }
}
