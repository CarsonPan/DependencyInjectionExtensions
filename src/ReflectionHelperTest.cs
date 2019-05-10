using DuckGo.DependencyInjection;
using Should;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DuckGo.DependencyInjectionTest
{
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

            //如果有多个匹配返回 最匹配的那个接口
            defaultServiceType = ReflectionHelper.GetDefaultServiceType(typeof(TestInterface0));
            defaultServiceType.ShouldEqual(typeof(ITestInterface0));
        }
    }
}
