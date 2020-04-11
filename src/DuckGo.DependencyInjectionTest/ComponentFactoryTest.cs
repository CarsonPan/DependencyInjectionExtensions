using Microsoft.Extensions.DependencyInjection;
using Shouldly;
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
            serviceCollection.AddDependencyInjectionExtension();
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IComponentFactory<string, ITestComponent> componentFactory = serviceProvider.GetRequiredService<IComponentFactory<string, ITestComponent>>();
            ITestComponent testComponent = componentFactory.GetRequiredService("A");
            testComponent.ShouldBeOfType(typeof(TestComponentA));

            testComponent = componentFactory.GetRequiredService("B");
            testComponent.ShouldBeOfType(typeof(TestComponentB));

            testComponent = componentFactory.GetRequiredService("C");
            testComponent.ShouldBeOfType(typeof(TestComponentC));

            testComponent = componentFactory.GetRequiredService("D");
            testComponent.ShouldBeOfType(typeof(TestComponentD));
            IComponentFactory<int, ITestComponent> componentFactory0 = serviceProvider.GetRequiredService<IComponentFactory<int, ITestComponent>>();
            testComponent = componentFactory0.GetRequiredService(0);
            testComponent.ShouldBeOfType(typeof(TestComponent0));

            testComponent = componentFactory0.GetRequiredService(1);
            testComponent.ShouldBeOfType(typeof(TestComponent1));

            testComponent = componentFactory0.GetRequiredService(2);
            testComponent.ShouldBeOfType(typeof(TestComponent2));
            testComponent = componentFactory0.GetRequiredService(3);
            testComponent.ShouldBeOfType(typeof(TestComponent3));

        }
    }
}
