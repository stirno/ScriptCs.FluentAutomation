using FluentAutomation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.FluentAutomation
{
    public class F14NProxy
    {
        public F14N scriptPackInstance = null;

        public F14NProxy(F14N scriptPackInstance)
        {
            this.scriptPackInstance = scriptPackInstance;
        }

        public F14NProxy Config(Action<FluentAutomationSettingsProxy> action)
        {
            this.scriptPackInstance.Config(action);
            return this;
        }

        public F14NProxy Run(Action action)
        {
            this.scriptPackInstance.Run(action);
            return this;
        }

        public F14NProxy Run(string name, Action action)
        {
            this.scriptPackInstance.Run(name, action);
            return this;
        }

        public F14NProxy Run(Action<INativeActionSyntaxProvider> action)
        {
            this.scriptPackInstance.Run(action);
            return this;
        }

        public F14NProxy Run(string name, Action<INativeActionSyntaxProvider> action)
        {
            this.scriptPackInstance.Run(name, action);
            return this;
        }
    }
}
