namespace InfPortal.common.DependencyResolution {
    using InfPortal.common.Logs;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
	
    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();					
                });
            For<ILoger>().Use<Loger>();
        }

        #endregion
    }
}