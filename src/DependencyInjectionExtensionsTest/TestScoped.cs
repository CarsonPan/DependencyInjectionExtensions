using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionExtensionsTest
{
    public interface ITestScoped:IScoped
    {
    }
    public class TestScoped: ITestScoped
    {
    }

    public interface ITestScoped0
    {
    }
    public class TestScoped0: ITestScoped0,IScoped
    {
    }

    public abstract class AbstractTestScoped : ITestScoped
    {
    }
}
