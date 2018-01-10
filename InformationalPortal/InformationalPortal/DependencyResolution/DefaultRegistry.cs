namespace InformationalPortal.DependencyResolution {
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;    
    using InformationalPortal.business.Interfaces;
    using InformationalPortal.business.Implementations;
	
    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
					scan.With(new ControllerConvention());
                });
            For<IOperationResult>().Use<OperationResult>();
            //For<IAuthenticationService>().Use<AuthenticationService>();
            For<IAuthenticationUser>().Use<AuthenticationUser>();
            
        }

        #endregion
    }
}