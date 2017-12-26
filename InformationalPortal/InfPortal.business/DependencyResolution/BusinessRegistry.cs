namespace InfPortal.business.DependencyResolution
{
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;   
    using InfPortal.business.Interfaces;
    using InfPortal.business.Providers;
	
    public class BusinessRegistry : Registry
    {
        #region Constructors and Destructors

        public BusinessRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
            For<IArticleProvider>().Use<ArticleProvider>();
            For<IUserProvider>().Use<UserProvider>();
            For<IHeadingProvider>().Use<HeadingProvider>();
            For<ILanguageProvider>().Use<LanguageProvider>();
           
        }

        #endregion
    }
}