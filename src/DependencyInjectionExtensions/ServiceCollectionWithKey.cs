using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionExtensions
{
    /// <summary>
    /// 服务容器
    /// </summary>
    internal  class ServiceCollectionWithKey
    {

        private readonly  ConcurrentDictionary<Type, ConcurrentDictionary<object,Type>> _serviceContainer = new ConcurrentDictionary<Type, ConcurrentDictionary<object, Type>>();
        public  void AddServiceWithKey<TService, TImplementationType>(object key)
        {
            AddServiceWithKey(typeof(TService), typeof(TImplementationType), key);
        }

        public  void AddServiceWithKey(Type serviceType, Type implementationType, object key)
        {
            ConcurrentDictionary<object, Type> container = _serviceContainer.GetOrAdd(serviceType, t => new ConcurrentDictionary<object, Type>());
            container.AddOrUpdate(key, implementationType, (_key, oldType) => implementationType);
        }

        public  void RemoveServiceWithKey(Type serviceType, object key)
        {
            if (_serviceContainer.TryGetValue(serviceType, out ConcurrentDictionary<object, Type> container))
            {
                container.TryRemove(key, out Type existObj);
                if(container.Count==0)
                {
                    _serviceContainer.TryRemove(serviceType, out container);
                }
            }
        }

        public  Type GetImplementation(Type serviceType, object key)
        {
            if (!_serviceContainer.TryGetValue(serviceType, out ConcurrentDictionary<object, Type> container))
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
