using DuckGo.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Should;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace DuckGo.DependencyInjectionTest
{
    public interface IScopedMoq0 
    {

    }
    public interface ITest 
    {
    }

    public interface ITestInterface0
    {
    }
    public interface ITestInterface
    {
    }

    public class TestClass : ITestInterface, ITestInterface0, ITest
    {
    }

    public class TestInterface0 : ITestInterface, ITestInterface0, ITest
    {
    }
    public class ScopedMoq0 : IScopedMoq0
    {
    }
    public class ReflectionHelperTest
    {
        [Fact]
        public void GetDefaultServiceTypeTest()
        {
            //如果找不到名称匹配的接口 返回null
            Type defaultServiceType = ReflectionHelper.GetDefaultServiceType(typeof(TestClass));
            defaultServiceType.ShouldBeNull();
            //如果找到名称匹配的接口就返回
            defaultServiceType = ReflectionHelper.GetDefaultServiceType(typeof(ScopedMoq0));
            defaultServiceType.ShouldEqual(typeof(IScopedMoq0));

            //如果有多个匹配,返回 最匹配的那个接口
            defaultServiceType = ReflectionHelper.GetDefaultServiceType(typeof(TestInterface0));
            defaultServiceType.ShouldEqual(typeof(ITestInterface0));
        }

        [Fact]
        public void GetServiceLifetimeTest()
        {
            ServiceLifetime lifetime = ReflectionHelper.GetServiceLifetime(typeof(IScoped));
            lifetime.ShouldEqual(ServiceLifetime.Scoped);
            lifetime= ReflectionHelper.GetServiceLifetime(typeof(ISingleton));
            lifetime.ShouldEqual(ServiceLifetime.Singleton);
            lifetime = ReflectionHelper.GetServiceLifetime(typeof(ITransient));
            lifetime.ShouldEqual(ServiceLifetime.Transient);
            lifetime = ReflectionHelper.GetServiceLifetime(typeof(object));
            lifetime.ShouldEqual(ServiceLifetime.Transient);
        }

        [Fact]
        public void GetTypesTest()
        {
            Assembly assembly = this.GetType().Assembly;
            IEnumerable<Type> types= ReflectionHelper.GetTypes(assembly, typeof(IScoped));
            types.Count().ShouldEqual(2);
            types.ShouldContain(typeof(TestScoped));
            types.ShouldContain(typeof(TestScoped0));

            types = ReflectionHelper.GetTypes(assembly, typeof(ITransient));
            types.Count().ShouldEqual(2);
            types.ShouldContain(typeof(TestTransient));
            types.ShouldContain(typeof(TestTransient0));

            types = ReflectionHelper.GetTypes(assembly, typeof(ISingleton));
            types.Count().ShouldEqual(2);
            types.ShouldContain(typeof(TestSingleton));
            types.ShouldContain(typeof(TestSingleton0));
        }

        [Fact]
        public void GetTypesByComponentAttributeTest()
        {
            Assembly assembly = this.GetType().Assembly;
            IEnumerable<Type> types=  ReflectionHelper.GetTypesByComponentAttribute(assembly);
            types.Count().ShouldEqual(5);
            types.ShouldContain(typeof(TestComponent));
            types.ShouldContain(typeof(TestComponentA));
            types.ShouldContain(typeof(TestComponentB));
            types.ShouldContain(typeof(TestComponentC));
            types.ShouldContain(typeof(TestComponentD));
        }
    }
}
