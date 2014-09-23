﻿using System.ComponentModel.Composition;
using DryIoc.MefAttributedModel.UnitTests.CUT;
using NUnit.Framework;

namespace DryIoc.MefAttributedModel.UnitTests
{
    [TestFixture]
    public class CustomInjectionRulesTests
    {
        [Test]
        public void Can_combine_MEF_Imports_with_custom_Injection_rules_for_parameters()
        {
            var container = new Container().WithAttributedModel();
            container.Register<ClientWithPrimitiveParameter>(setup: Setup.With(parameters: Parameters.Default.With("message", "hell")));
            //container.RegisterExports(typeof(ClientWithPrimitiveParameter));
            container.RegisterExports(typeof(KeyService));

            var client = container.Resolve<ClientWithPrimitiveParameter>();
        }

        [Export]
        public class ClientWithPrimitiveParameter
        {
            public IService Service { get; private set; }
            public string Message { get; private set; }

            public ClientWithPrimitiveParameter([ImportWithKey(ServiceKey.One)]IService service, string message)
            {
                Service = service;
                Message = message;
            }
        }    
    }
}
