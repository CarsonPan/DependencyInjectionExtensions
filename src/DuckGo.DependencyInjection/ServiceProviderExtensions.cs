using DependencyInjection;
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
        public static object GetServiceWidthKey(this IServiceProvider provider,Type serviceType,object key)
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
            object implementation = ServiceCollectionWithKey.GetImplementation(serviceType, key);
            //如果没有认为存在，直接返回null
            if (implementation == null)
            {
                return null;
            }
            if (implementation is Type implementationType)
            {

                return provider.GetService(implementationType);
            }
            else
            {
                return implementation;
            }
        }
        public static TService GetServiceWidthKey<TService>(this IServiceProvider provider, object key)
            where TService : class
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return (TService)provider.GetServiceWidthKey(typeof(TService), key);
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
            object implementation = ServiceCollectionWithKey.GetImplementation(serviceType, key);
            //如果没有认为存在，直接返回null
            if (implementation == null)
            {
                throw new InvalidOperationException($"No service for type '{serviceType}' has been registered.");
            }
            if (implementation is Type implementationType)
            {

                return provider.GetRequiredService(implementationType);
            }
            else
            {
                return implementation;
            }
        }
        public static TService GetRequiredServiceWithKey<TService>(this IServiceProvider provider, object key)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return (TService)provider.GetRequiredServiceWithKey(typeof(TService), key);
        }
    }
}
