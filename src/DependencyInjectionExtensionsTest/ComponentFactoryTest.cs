using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DependencyInjectionExtensionsTest
{
    public class ComponentFactoryTest
    {
        [Fact]
        public void GetRequiredServiceTest()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            
            serviceCollection.AddAssemblyByConvention(this.GetType().Assembly);
            serviceCollection.AddDependencyInjectionExtensions();
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IComponentFactory<string, ITestComponent> componentFactory = serviceProvider.GetRequiredService<IComponentFactory<string, ITestComponent>>();
            ITestComponent testComponent = componentFactory.GetRequiredService("A");
            testComponent.ShouldBeOfType(typeof(TestComponentA), "<steing,ITestComponent> 解析：A");

            testComponent = componentFactory.GetRequiredService("B");
            testComponent.ShouldBeOfType(typeof(TestComponentB), "<steing,ITestComponent> 解析：B");

            testComponent = componentFactory.GetRequiredService("C");
            testComponent.ShouldBeOfType(typeof(TestComponentC), "<steing,ITestComponent> 解析：C");

            testComponent = componentFactory.GetRequiredService("D");
            testComponent.ShouldBeOfType(typeof(TestComponentD), "<steing,ITestComponent> 解析：D");
            IComponentFactory<int, ITestComponent> componentFactory0 = serviceProvider.GetRequiredService<IComponentFactory<int, ITestComponent>>();
            testComponent = componentFactory0.GetRequiredService(0);
            testComponent.ShouldBeOfType(typeof(TestComponent0), "<int,ITestComponent> 解析：0");

            testComponent = componentFactory0.GetRequiredService(1);
            testComponent.ShouldBeOfType(typeof(TestComponent1), "<int,ITestComponent> 解析：1");

            testComponent = componentFactory0.GetRequiredService(2);
            testComponent.ShouldBeOfType(typeof(TestComponent2), "<int,ITestComponent> 解析：2");
            testComponent = componentFactory0.GetRequiredService(3);
            testComponent.ShouldBeOfType(typeof(TestComponent3), "<int,ITestComponent> 解析：3");

        }
    }
}
