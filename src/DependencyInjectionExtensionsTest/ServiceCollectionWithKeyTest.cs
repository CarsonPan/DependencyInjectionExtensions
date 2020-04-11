using DependencyInjectionExtensions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DependencyInjectionExtensionsTest
{
    public  class ServiceCollectionWithKeyTest
    {
        //[Fact]
        //public void AddServiceWithKeyTest()
        //{
        //    ServiceCollectionWithKey.AddServiceWithKey(typeof(ITestComponent), typeof(TestComponentA), "A");
        //    ServiceCollectionWithKey.ServiceContainer.ContainsKey(typeof(ITestComponent)).ShouldBeTrue();
        //    ServiceCollectionWithKey.ServiceContainer[typeof(ITestComponent)].ContainsKey("A").ShouldBeTrue();
        //    ServiceCollectionWithKey.ServiceContainer[typeof(ITestComponent)]["A"].ShouldEqual(typeof(TestComponentA));
        //    TestComponentB instance = new TestComponentB();
        //    ServiceCollectionWithKey.AddServiceWithKey(typeof(ITestComponent),instance , "B");
        //    ServiceCollectionWithKey.ServiceContainer[typeof(ITestComponent)].ContainsKey("B").ShouldBeTrue();
        //    ServiceCollectionWithKey.ServiceContainer[typeof(ITestComponent)]["B"].ShouldEqual(instance);
        //}
    }
}
