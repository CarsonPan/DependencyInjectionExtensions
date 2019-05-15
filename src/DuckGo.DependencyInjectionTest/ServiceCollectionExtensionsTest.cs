using Microsoft.Extensions.DependencyInjection;
using Should;
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
            count.ShouldEqual(1);

            count = services.Count(s => s.ServiceType == typeof(ITestScoped0) && s.ImplementationType == typeof(TestScoped0) && s.Lifetime == ServiceLifetime.Scoped);
            count.ShouldEqual(1);

            count = services.Count(s => s.ServiceType == typeof(ITestSingleton) && s.ImplementationType == typeof(TestSingleton) && s.Lifetime == ServiceLifetime.Singleton);
            count.ShouldEqual(1);

            count = services.Count(s => s.ServiceType == typeof(ITestSingleton0) && s.ImplementationType == typeof(TestSingleton0) && s.Lifetime == ServiceLifetime.Singleton);
            count.ShouldEqual(1);

            count = services.Count(s => s.ServiceType == typeof(ITestTransient) && s.ImplementationType == typeof(TestTransient) && s.Lifetime == ServiceLifetime.Transient);
            count.ShouldEqual(1);

            count = services.Count(s => s.ServiceType == typeof(ITestTransient0) && s.ImplementationType == typeof(TestTransient0) && s.Lifetime == ServiceLifetime.Transient);
            count.ShouldEqual(1);

            count = services.Count(s => s.ServiceType == typeof(ITestComponent) && s.ImplementationType == typeof(TestComponent) && s.Lifetime == ServiceLifetime.Transient);
            count.ShouldEqual(1);

            count = services.Count(s => s.ServiceType == typeof(TestComponentA) && s.ImplementationType == typeof(TestComponentA) && s.Lifetime == ServiceLifetime.Transient);
            count.ShouldEqual(1);

            count = services.Count(s => s.ServiceType == typeof(TestComponentB) && s.ImplementationType == typeof(TestComponentB) && s.Lifetime == ServiceLifetime.Transient);
            count.ShouldEqual(1);
            count = services.Count(s => s.ServiceType == typeof(TestComponentC) && s.ImplementationType == typeof(TestComponentC) && s.Lifetime == ServiceLifetime.Transient);
            count.ShouldEqual(1);
            count = services.Count(s => s.ServiceType == typeof(TestComponentD) && s.ImplementationType == typeof(TestComponentD) && s.Lifetime == ServiceLifetime.Transient);
            count.ShouldEqual(1);


        }

        [Fact]
       public void AddWithKeyTest()
        {
            ServiceCollection services = new ServiceCollection();
            TestSingleton instance = new TestSingleton();
            services.AddSingletonWithKey(typeof(ITestSingleton),instance, "A");
            int count = services.Count(s => s.ServiceType == typeof(ITestSingleton));
            count.ShouldEqual(0);
            services.AddSingletonWithKey(typeof(ITestSingleton0), typeof(TestSingleton0), "B");
            count = services.Count(s => s.ServiceType == typeof(TestSingleton0));
            count.ShouldEqual(1);
            IServiceProvider serviceProvider= services.BuildServiceProvider();
             var service= serviceProvider.GetRequiredServiceWithKey<ITestSingleton0>("B");
            service.ShouldBeType(typeof(TestSingleton0));
            serviceProvider.GetRequiredServiceWithKey<ITestSingleton>("A").ShouldEqual(instance);
        }

        [Fact]
        public void RemoveWithKeyTest()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddAssemblyByConvention(this.GetType().Assembly);
            services.RemoveWithKey(typeof(ITestComponent), "A");
            int count = services.Count(s => s.ServiceType == typeof(TestComponentA) && s.ImplementationType == typeof(TestComponentA) && s.Lifetime == ServiceLifetime.Transient);
            count.ShouldEqual(0);

            count = services.Count(s => s.ServiceType == typeof(TestComponentB) && s.ImplementationType == typeof(TestComponentB) && s.Lifetime == ServiceLifetime.Transient);
            count.ShouldEqual(1);

            services.RemoveWithKey(typeof(ITestComponent), "B");
            count = services.Count(s => s.ServiceType == typeof(TestComponentB) && s.ImplementationType == typeof(TestComponentB) && s.Lifetime == ServiceLifetime.Transient);
            count.ShouldEqual(0);

        }
    }
}
