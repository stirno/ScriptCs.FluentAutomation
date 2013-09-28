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
        private IScriptPackContext scriptPack = new F14N();
        public IScriptPackContext GetContext()
        {
            return scriptPack;
        }

        public void Initialize(IScriptPackSession session)
        {
            session.ImportNamespace("FluentAutomation");
            session.ImportNamespace("FluentAutomation.Exceptions");
        }

        public void Terminate()
        {
            var context = this.GetContext();
            if (context != null)
                (context as F14N).Terminate();
        }
    }
}
