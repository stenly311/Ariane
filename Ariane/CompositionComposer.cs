using Ariane.Common;
using Catel.Logging;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using MEFShared;

namespace Ariane
{
    public class CompositionComposer : IDisposable
    {
        private CompositionContainer _container;
        private ILog _log = LogManager.GetLogger(typeof(CompositionComposer));

        [Import(typeof(IPluginRunner))]
        public IPluginRunner Runner;

        public CompositionComposer(string packagePath)
        {
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();
            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(CompositionComposer).Assembly));

            // search sub-folders within path
            foreach (var d in System.IO.Directory.GetDirectories(packagePath))
            {
                catalog.Catalogs.Add(new DirectoryCatalog(d, "Ariane.*.dll"));
            }

            //Create the CompositionContainer with the parts in the catalog
            _container = new CompositionContainer(catalog);

            try
            {
                _container.ComposeParts(this);
            }
            catch (CompositionException ex)
            {
                _log.Error("Container composition failed", ex);
            }
        }

        public void Dispose()
        {
            var exports = _container.GetExports<IOperation, IOperationData>();
            _container.ReleaseExports(exports);

            var export = _container.GetExport<IPluginRunner>();
            _container.ReleaseExport(export);
            _container.Dispose();
        }
    }
}
