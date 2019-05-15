using Microsoft.Extensions.DependencyInjection;
using Should;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DuckGo.DependencyInjectionTest
{
    public class ComponentFactoryTest
    {
        [Fact]
        public void GetRequiredServiceTest()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddAssemblyByConvention(this.GetType().Assembly);
            serviceCollection.AddIocExtension();
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IComponentFactory<string, ITestComponent> componentFactory = serviceProvider.GetRequiredService<IComponentFactory<string, ITestComponent>>();
            ITestComponent testComponent = componentFactory.GetRequiredService("A");
            testComponent.ShouldBeType(typeof(TestComponentA));

            testComponent = componentFactory.GetRequiredService("B");
            testComponent.ShouldBeType(typeof(TestComponentB));

            testComponent = componentFactory.GetRequiredService("C");
            testComponent.ShouldBeType(typeof(TestComponentC));

            testComponent = componentFactory.GetRequiredService("D");
            testComponent.ShouldBeType(typeof(TestComponentD));
            IComponentFactory<int, ITestComponent> componentFactory0 = serviceProvider.GetRequiredService<IComponentFactory<int, ITestComponent>>();
            testComponent = componentFactory0.GetRequiredService(0);
            testComponent.ShouldBeType(typeof(TestComponent0));

            testComponent = componentFactory0.GetRequiredService(1);
            testComponent.ShouldBeType(typeof(TestComponent1));

            testComponent = componentFactory0.GetRequiredService(2);
            testComponent.ShouldBeType(typeof(TestComponent2));
            testComponent = componentFactory0.GetRequiredService(3);
            testComponent.ShouldBeType(typeof(TestComponent3));

        }
    }
}
