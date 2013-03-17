using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BCP.SimpleForum.Controllers;
using FlexProviders.Membership;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace BCP.SimpleForum.Test
{
    [TestFixture]
    public class AccountControllerTests
    {
        private AccountController _controller;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            fixture.Customize(new WebModelCustomization());

            _controller = fixture.Create<AccountController>();
        }

        [Test]
        public void CanCreateAccount()
        {
            var result = _controller.Login();

            Assert.That(result, Is.Not.Null);
        }
    }

    internal class WebModelCustomization : CompositeCustomization
    {
        internal WebModelCustomization() : base( new MvcCustomization(),
                                                 new AutoMoqCustomization() )
        {
        }

        private class MvcCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Customize<ControllerContext>(c => c
                    .Without(x => x.DisplayMode));
            }
        }
    }
}
