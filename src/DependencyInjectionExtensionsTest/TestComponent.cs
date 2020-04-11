using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionExtensionsTest
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

    [Component(typeof(ITestComponent), Key = 0)]
    public class TestComponent0 : ITestComponent
    {
    }
    [Component(typeof(ITestComponent), Key = 1)]
    public class TestComponent1 : ITestComponent
    {
    }
    [Component(typeof(ITestComponent), Key = 2)]
    public class TestComponent2 : ITestComponent
    {
    }
    [Component(typeof(ITestComponent), Key = 3)]
    public class TestComponent3 : ITestComponent
    {
    }
}
