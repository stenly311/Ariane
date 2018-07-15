using Ariane.Model;
using System.Collections.Generic;

namespace Ariane.Common
{
    public interface IPluginRunner
    {
        void Initiate(string pluginName, ICollection<Process> processes);
        bool HasPlugin(string pluginName);
    }
}
