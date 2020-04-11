using DependencyInjectionExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 解析扩展
    /// </summary>
    public static class ServiceProviderExtensions
    {
        private static ServiceCollectionWithKey GetServiceContainer(this IServiceProvider provider)
        {
            return provider.GetService<ServiceCollectionWithKey>();
        }
        public static object GetServiceWithKey(this IServiceProvider provider,Type serviceType,object key)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            
            Type implementationType = provider.GetServiceContainer()?.GetImplementation(serviceType, key);
            //如果没有认为存在，直接返回null
            if (implementationType == null)
            {
                return null;
            }
          return provider.GetService(implementationType);
            
        }
        public static TService GetServiceWithKey<TService>(this IServiceProvider provider, object key)
            where TService : class
        {
            return (TService)provider.GetServiceWithKey(typeof(TService), key);
        }

        public static object GetRequiredServiceWithKey(this IServiceProvider provider, Type serviceType,object key)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            Type implementationType = provider.GetServiceContainer()?.GetImplementation(serviceType, key);
            //如果没有认为存在，直接返回null
            if (implementationType == null)
            {
                throw new InvalidOperationException($"No service for type '{serviceType}' has been registered.");
            }
            
                return provider.GetRequiredService(implementationType);
            
        }
        public static TService GetRequiredServiceWithKey<TService>(this IServiceProvider provider, object key)
        {
            return (TService)provider.GetRequiredServiceWithKey(typeof(TService), key);
        }
    }
}
