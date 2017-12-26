namespace InfPortal.data.DependencyResolution
{
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
    using InfPortal.data.Interfaces;       
    using InfPortal.data.Repository;
	
    public class DataRegistry : Registry 
    {
        #region Constructors and Destructors

        public DataRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
            For<IArticleRepository>().Use<ArticleRepository>();
            For<IHeadingRepository>().Use<HeadingRepository>();
            For<IUserRepository>().Use<UserRepository>();
            For<ILanguageRepository>().Use<LanguageRepository>();
        }

        #endregion
    }
}