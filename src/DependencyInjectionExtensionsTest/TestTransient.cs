using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionExtensionsTest
{
    
    public interface ITestTransient : ITransient
    {
    }
    public class TestTransient : ITestTransient
    {
    }

    public interface ITestTransient0
    {
    }
    public class TestTransient0 : ITestTransient0, ITransient
    {
    }

    public abstract class AbstractTestTransient : ITestTransient
    {
    }
}
