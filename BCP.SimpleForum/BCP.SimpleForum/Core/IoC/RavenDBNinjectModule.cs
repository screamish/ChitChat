using Ninject.Modules;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Database.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Web.Common;

namespace BCP.SimpleForum.Core.IoC
{
    public class RavenDBNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDocumentStore>().ToMethod(context =>
                                    {
                                        //NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);
                                        var documentStore = new EmbeddableDocumentStore { DataDirectory = "App_Data" };
                                        documentStore.Initialize();

                                        Glimpse.RavenDb.Profiler.AttachTo(documentStore);
                                        Glimpse.RavenDb.Profiler.HideFields("PasswordHash", "PasswordSalt");

                                        return documentStore;
                                    })
                                    .InSingletonScope();

            Bind<IDocumentSession>().ToMethod(context => context.Kernel.Get<IDocumentStore>().OpenSession()).InRequestScope();
        }
    }
}