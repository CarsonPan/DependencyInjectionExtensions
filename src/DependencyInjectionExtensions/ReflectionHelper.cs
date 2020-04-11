using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace DependencyInjectionExtensions
{
    public static class ReflectionHelper
    {
        public static IEnumerable<Type> GetTypes(Assembly assembly, Type flagType)
        {
            return assembly.GetExportedTypes().Where(type => type.IsClass && //类
                                                   !type.IsAbstract &&//非抽象
                                                   !type.IsDefined(typeof(ComponentAttribute)) &&//未标记ComponentAttribute 属性
                                                   flagType.IsAssignableFrom(type));//实现了相关标记接口
        }

        public static IEnumerable<Type> GetTypesByComponentAttribute(Assembly assembly)
        {
            return assembly.GetExportedTypes().Where(type => type.IsClass && //类
                                                   !type.IsAbstract &&//非抽象
                                                   type.IsDefined(typeof(ComponentAttribute)));
        }

        public static ServiceLifetime GetServiceLifetime(Type flagType)
        {
            if (typeof(IScoped) == flagType)
            {
                return ServiceLifetime.Scoped;
            }
            else
            if (typeof(ISingleton) == flagType)
            {
                return ServiceLifetime.Singleton;
            }
            else
                return ServiceLifetime.Transient;
        }

        public static Type GetDefaultServiceType(Type implementationType)
        {
            Type[] interfaces = implementationType.GetInterfaces();
            //除去接口名的前缀之后与类名尾部完全匹配
            IEnumerable<Type> defaultInterfaces = interfaces.Where(i => implementationType.Name.EndsWith(i.Name.Substring(1)));
            if (!defaultInterfaces.Any())
            {
                return null;
            }
            //名字最长的一个作为默认接口
            return defaultInterfaces.OrderByDescending(i => i.Name.Length).FirstOrDefault();
        }
    }
}
