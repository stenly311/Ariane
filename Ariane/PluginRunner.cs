using Ariane.Common;
using Ariane.Model;
using Catel.Logging;
using MEFShared;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Ariane
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(IPluginRunner))]
    public class PluginRunner : IPluginRunner
    {
        #pragma warning disable CS0649
        [ImportMany]
        readonly ICollection<Lazy<IOperation, IOperationData>> Operations = new List<Lazy<IOperation, IOperationData>>();
        #pragma warning restore CS0649

        private readonly ILog _log = LogManager.GetLogger(typeof(PluginRunner));

        public bool HasPlugin(string pluginName)
        {
            return Operations != null && Operations.Any(x => x.Metadata.Plugin.Equals(pluginName));
        }

        public void Initiate(string pluginName, ICollection<Process> processes)
        {
            var op = Operations.FirstOrDefault(x => x.Metadata.Plugin.Equals(pluginName));
            if (op != null)
            {
                op.Value.Initiate((ICollection<object>)processes);
            }
            else
                throw new Exception($"Operation Not Found! \"{pluginName}\"");
        }
    }
}
