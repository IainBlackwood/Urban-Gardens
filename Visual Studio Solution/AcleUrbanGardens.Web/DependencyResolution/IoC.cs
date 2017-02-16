using AcleUrbanGardens.Domain;
using AcleUrbanGardens.Web.Infrastructure;
using StructureMap;
namespace AcleUrbanGardens.Web {
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });
                            x.For<ICategoryDataSource>().HttpContextScoped().Use<AcleUrbanGardensDb>();
                        });
            return ObjectFactory.Container;
        }
    }
}