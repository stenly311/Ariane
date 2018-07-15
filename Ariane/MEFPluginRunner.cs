using Ariane.Model;
using Catel.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;

namespace Ariane
{
    internal class MEFPluginRunner
    {
        private ILog _log = LogManager.GetLogger<MEFPluginRunner>();
        private readonly CompositionComposer _compositionComposer;

        public MEFPluginRunner(CompositionComposer compositionComposer)
        {
            _compositionComposer = compositionComposer;
        }

        public bool HasPluginLoaded(string pluginName)
        {
            return _compositionComposer.Runner.HasPlugin(pluginName);
            //PrintAssemblies();
        }

        public void PrintAssemblies()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            //Provide the current application domain evidence for the assembly.
            Evidence asEvidence = currentDomain.Evidence;

            //Make an array for the list of assemblies.
            Assembly[] assemblies = currentDomain.GetAssemblies();

            _log.Info("List of assemblies loaded in current application domain: ");

            foreach (Assembly a in assemblies.OrderBy(p => p.FullName))
            {
                _log.Info(a.FullName);
            }

            var nonDistinctAssemblies = from assembly in assemblies
                group assembly by assembly.GetName().Name
                into grouped
                where grouped.Count() > 1
                select grouped;

            foreach (var assemblyGroup in nonDistinctAssemblies)
            {
                _log.Warning("The following assemblies have been loaded more than once: ");

                foreach (var a in assemblyGroup)
                {
                    _log.Warning(a.FullName);
                }
            }
        }

        public void Initiate(string pluginName, ICollection<Process> processes)
        {
            _log.Info($"Starting plugin {pluginName}");
            
            try
            {
                pluginName = pluginName.Replace(".dll", "");

                if (_compositionComposer.Runner.HasPlugin(pluginName))
                    _compositionComposer.Runner.Initiate(pluginName, processes);
                else
                    _log.Warning($"Plugin '{pluginName}' has not been loaded.");

            }
            catch (ReflectionTypeLoadException reflectionException)
            {
                foreach (var item in reflectionException.LoaderExceptions)
                {
                    _log.Error(item);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            finally
            {
                _log.Info($"Initiating finished");
            }
        }

    }
}
