using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public class ComponentFactory<TKey, TService> : IComponentFactory<TKey, TService>
        where TService:class
    {
        protected readonly IServiceProvider ServiceProvider;
        public ComponentFactory(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        public TService GetRequiredService(TKey key)
        {
            return ServiceProvider.GetRequiredServiceWithKey<TService>(key);
        }

        public TService GetService(TKey key)
        {
            return ServiceProvider.GetServiceWithKey<TService>(key);
        }

        public TService GetService(TKey key, TKey defaultKey)
        {
            return GetService(key) ?? GetService(defaultKey);
        }

        public TService GetService(TKey key, TService defaultService)
        {
            return GetService(key) ?? defaultService;
        }
    }
}
