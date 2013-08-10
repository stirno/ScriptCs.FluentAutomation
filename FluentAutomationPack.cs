using ScriptCs.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.FluentAutomation
{
    public class FluentAutomationPack : IScriptPack
    {
        public IScriptPackContext GetContext()
        {
            return new FluentAutomationScript();
        }

        public void Initialize(IScriptPackSession session)
        {
            session.ImportNamespace("FluentAutomation");
        }

        public void Terminate()
        {
            var context = this.GetContext();
            if (context != null)
                (context as FluentAutomationScript).Terminate();
        }
    }
}
