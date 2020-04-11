using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DuckGo.DependencyInjectionTest
{
    public class ServiceCollectionExtensionsTest
    {
        [Fact]
        public void AddAssemblyByConventionTest()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddAssemblyByConvention(this.GetType().Assembly);
            int count = services.Count(s => s.ServiceType == typeof(ITestScoped) && s.ImplementationType == typeof(TestScoped) && s.Lifetime == ServiceLifetime.Scoped);
            count.ShouldBe(1);

            count = services.Count(s => s.ServiceType == typeof(ITestScoped0) && s.ImplementationType == typeof(TestScoped0) && s.Lifetime == ServiceLifetime.Scoped);
            count.ShouldBe(1);

            count = services.Count(s => s.ServiceType == typeof(ITestSingleton) && s.ImplementationType == typeof(TestSingleton) && s.Lifetime == ServiceLifetime.Singleton);
            count.ShouldBe(1);

            count = services.Count(s => s.ServiceType == typeof(ITestSingleton0) && s.ImplementationType == typeof(TestSingleton0) && s.Lifetime == ServiceLifetime.Singleton);
            count.ShouldBe(1);

            count = services.Count(s => s.ServiceType == typeof(ITestTransient) && s.ImplementationType == typeof(TestTransient) && s.Lifetime == ServiceLifetime.Transient);
            count.ShouldBe(1);

            count = services.Count(s => s.ServiceType == typeof(ITestTransient0) && s.ImplementationType == typeof(TestTransient0) && s.Lifetime == ServiceLifetime.Transient);
            count.ShouldBe(1);

            count = services.Count(s => s.ServiceType == typeof(ITestComponent) && s.ImplementationType == typeof(TestComponent) && s.Lifetime == ServiceLifetime.Transient);
            count.ShouldBe(1);

            count = services.Count(s => s.ServiceType == typeof(TestComponentA) && s.ImplementationType == typeof(TestComponentA) && s.Lifetime == ServiceLifetime.Transient);
            count.ShouldBe(1);

            count = services.Count(s => s.ServiceType == typeof(TestComponentB) && s.ImplementationType == typeof(TestComponentB) && s.Lifetime == ServiceLifetime.Transient);
            count.ShouldBe(1);
            count = services.Count(s => s.ServiceType == typeof(TestComponentC) && s.ImplementationType == typeof(TestComponentC) && s.Lifetime == ServiceLifetime.Transient);
            count.ShouldBe(1);
            count = services.Count(s => s.ServiceType == typeof(TestComponentD) && s.ImplementationType == typeof(TestComponentD) && s.Lifetime == ServiceLifetime.Transient);
            count.ShouldBe(1);


        }

        [Fact]
       public void AddWithKeyTest()
        {
            ServiceCollection services = new ServiceCollection();
            TestSingleton instance = new TestSingleton();
            services.AddSingletonWithKey(typeof(ITestSingleton),instance, "A");
            int count = services.Count(s => s.ServiceType == typeof(ITestSingleton));
            count.ShouldBe(0);
            services.AddSingletonWithKey(typeof(ITestSingleton0), typeof(TestSingleton0), "B");
            count = services.Count(s => s.ServiceType == typeof(TestSingleton0));
            count.ShouldBe(1);
            IServiceProvider serviceProvider= services.BuildServiceProvider();
             var service= serviceProvider.GetRequiredServiceWithKey<ITestSingleton0>("B");
            service.ShouldBeOfType<TestSingleton0>();
            serviceProvider.GetRequiredServiceWithKey<ITestSingleton>("A").ShouldBe(instance);
        }

        [Fact]
        public void RemoveWithKeyTest()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddAssemblyByConvention(this.GetType().Assembly);
            services.RemoveWithKey(typeof(ITestComponent), "A");
            int count = services.Count(s => s.ServiceType == typeof(TestComponentA) && s.ImplementationType == typeof(TestComponentA) && s.Lifetime == ServiceLifetime.Transient);
            count.ShouldBe(0);

            count = services.Count(s => s.ServiceType == typeof(TestComponentB) && s.ImplementationType == typeof(TestComponentB) && s.Lifetime == ServiceLifetime.Transient);
            count.ShouldBe(1);

            services.RemoveWithKey(typeof(ITestComponent), "B");
            count = services.Count(s => s.ServiceType == typeof(TestComponentB) && s.ImplementationType == typeof(TestComponentB) && s.Lifetime == ServiceLifetime.Transient);
            count.ShouldBe(0);

        }
    }
}
