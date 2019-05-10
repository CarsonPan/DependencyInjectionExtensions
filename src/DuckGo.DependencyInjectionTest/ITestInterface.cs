using System;
using System.Collections.Generic;
using System.Text;

namespace DuckGo.DependencyInjectionTest
{
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
}
