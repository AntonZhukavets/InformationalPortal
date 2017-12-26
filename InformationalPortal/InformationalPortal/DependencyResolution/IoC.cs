namespace InformationalPortal.DependencyResolution {
    using StructureMap;
	
    public static class IoC {
        public static IContainer Initialize()
        {
            return new Container(
                x => x.Scan
                   (
                       scan =>
                       {
                           scan.Assembly("InfPortal.business");
                           scan.Assembly("InfPortal.data");
                           //scan.Assembly("InformationalPortal");
                           scan.WithDefaultConventions();
                           scan.LookForRegistries();
                       }
                   )
                );

        }
    }
}