using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DuckGo.DependencyInjection
{
    public class ReflectionHelper
    {
        public IEnumerable<Type> GetTypes(Assembly assembly,Type flagType)
        {
            return assembly.GetExportedTypes().Where(type => type.IsClass && //类
                                                   !type.IsAbstract &&//非抽象
                                                   !type.IsDefined(typeof(ComponentAttribute))&&//未标记ComponentAttribute 属性
                                                   flagType.IsAssignableFrom(type)
                                              );
        }
    }
}
