using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DuckGo.DependencyInjectionTest
{

    public interface ITestSingleton : ISingleton
    {
    }
    public class TestSingleton : ITestSingleton
    {
    }

    public interface ITestSingleton0
    {
    }
    public class TestSingleton0 : ITestSingleton0, ISingleton
    {
    }

    public abstract class AbstractTestSingleton : ITestSingleton
    {
    }
}
