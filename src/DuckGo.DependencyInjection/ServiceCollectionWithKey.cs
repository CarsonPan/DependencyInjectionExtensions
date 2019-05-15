using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    /// <summary>
    /// 服务容器
    /// </summary>
    internal static class ServiceCollectionWithKey
    {
        private readonly static ConcurrentDictionary<Type, ConcurrentDictionary<object,object>> ServiceContainer = new ConcurrentDictionary<Type, ConcurrentDictionary<object, object>>();
        public static void AddServiceWithKey<TService, TImplementationType>(object key)
        {
            AddServiceWithKey(typeof(TService), typeof(TImplementationType), key);
        }

        public static void AddServiceWithKey(Type serviceType, object implementationType, object key)
        {
            ConcurrentDictionary<object, object> container = ServiceContainer.GetOrAdd(serviceType, t => new ConcurrentDictionary<object, object>());
            container.AddOrUpdate(key, implementationType, (_key, oldType) => implementationType);
        }

        public static void RemoveServiceWithKey(Type serviceType, object key)
        {
            if (ServiceContainer.TryGetValue(serviceType, out ConcurrentDictionary<object, object> container))
            {
                container.TryRemove(key, out object existObj);
            }
        }

        public static object GetImplementation(Type serviceType, object key)
        {
            if (!ServiceContainer.TryGetValue(serviceType, out ConcurrentDictionary<object, object> container))
            {
                return null;
            }
            if (!container.TryGetValue(key, out object implementation))
            {
                return null;
            }
            return implementation;
        }

    }
}
