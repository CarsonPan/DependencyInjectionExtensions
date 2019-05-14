using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DuckGo.DependencyInjectionTest
{
    public interface ITestComponent : IScoped
    {
    }
    [Component(typeof(ITestComponent))]
    public class TestComponent : ITestComponent
    {
    }

    [Component(typeof(ITestComponent),Key ="A")]
    public class TestComponentA : ITestComponent
    {
    }
    [Component(typeof(ITestComponent), Key = "B")]
    public class TestComponentB : ITestComponent
    {
    }
    [Component(typeof(ITestComponent), Key = "C")]
    public class TestComponentC : ITestComponent
    {
    }
    [Component(typeof(ITestComponent), Key = "D")]
    public class TestComponentD : ITestComponent
    {
    }
}
