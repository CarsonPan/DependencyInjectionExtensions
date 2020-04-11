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
        private readonly static ConcurrentDictionary<Type, ConcurrentDictionary<object,Type>> ServiceContainer = new ConcurrentDictionary<Type, ConcurrentDictionary<object, Type>>();
        public static void AddServiceWithKey<TService, TImplementationType>(object key)
        {
            AddServiceWithKey(typeof(TService), typeof(TImplementationType), key);
        }

        public static void AddServiceWithKey(Type serviceType, Type implementationType, object key)
        {
            ConcurrentDictionary<object, Type> container = ServiceContainer.GetOrAdd(serviceType, t => new ConcurrentDictionary<object, Type>());
            container.AddOrUpdate(key, implementationType, (_key, oldType) => implementationType);
        }

        public static void RemoveServiceWithKey(Type serviceType, object key)
        {
            if (ServiceContainer.TryGetValue(serviceType, out ConcurrentDictionary<object, Type> container))
            {
                container.TryRemove(key, out Type existObj);
                if(container.Count==0)
                {
                    ServiceContainer.TryRemove(serviceType, out container);
                }
            }
        }

        public static Type GetImplementation(Type serviceType, object key)
        {
            if (!ServiceContainer.TryGetValue(serviceType, out ConcurrentDictionary<object, Type> container))
            {
                return null;
            }
            if (!container.TryGetValue(key, out Type implementation))
            {
                return null;
            }
            return implementation;
        }

    }
}
