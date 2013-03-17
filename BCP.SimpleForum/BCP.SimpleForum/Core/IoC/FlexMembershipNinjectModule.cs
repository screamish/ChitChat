using BCP.SimpleForum.Models;
using FlexProviders.Aspnet;
using FlexProviders.Membership;
using FlexProviders.Raven;
using FlexProviders.Roles;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCP.SimpleForum.Core.IoC
{
    public class FlexMembershipNinjectModule : NinjectModule
    {
        public override void Load()
        {
            //x.For<IFlexMembershipProvider>().HybridHttpOrThreadLocalScoped().Use<FlexMembershipProvider>();
            //x.For<IFlexRoleProvider>().HybridHttpOrThreadLocalScoped().Use<FlexRoleProvider>();
            //x.For<IFlexUserStore>().HybridHttpOrThreadLocalScoped().Use<FlexMembershipUserStore<User, Role>>();
            //x.For<IFlexRoleStore>().HybridHttpOrThreadLocalScoped().Use<FlexMembershipUserStore<User, Role>>();
            //x.SetAllProperties(p => p.OfType<IFlexRoleProvider>());
            //x.Forward<IFlexMembershipProvider, IFlexOAuthProvider>();

            //x.For<IApplicationEnvironment>().Singleton().Use<AspnetEnvironment>();
            //x.For<ISecurityEncoder>().Singleton().Use<DefaultSecurityEncoder>();

            Bind<IFlexMembershipProvider>().To<FlexMembershipProvider>();
            Bind<IFlexRoleProvider>().To<FlexRoleProvider>();
            Bind<IFlexUserStore>().To<FlexMembershipUserStore<User, Role>>();

            Bind<IFlexOAuthProvider>().To<FlexMembershipProvider>();

            Bind<IApplicationEnvironment>().To<AspnetEnvironment>().InSingletonScope();
            Bind<ISecurityEncoder>().To<DefaultSecurityEncoder>().InSingletonScope();
        }
    }
}