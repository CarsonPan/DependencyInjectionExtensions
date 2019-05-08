using DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTransientWithKey(
            this IServiceCollection services,
            Type serviceType,
            Type implementationType,
            object key)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            ServiceCollectionWithKey.AddServiceWithKey(serviceType, implementationType, key);
            return services.AddTransient(serviceType, implementationType);
        }


        public static IServiceCollection AddTransientWithKey<TService, TImplementation>(this IServiceCollection services,object key)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return services.AddTransientWithKey(typeof(TService), typeof(TImplementation), key);
        }

        public static IServiceCollection AddScopedWithKey(
           this IServiceCollection services,
           Type serviceType,
           Type implementationType,
           object key)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            ServiceCollectionWithKey.AddServiceWithKey(serviceType, implementationType, key);
            return services.AddScoped(serviceType, implementationType);
        }
        public static IServiceCollection AddScopedWithKey<TService, TImplementation>(this IServiceCollection services,object key)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return services.AddScopedWithKey(typeof(TService), typeof(TImplementation),key);
        }

        public static IServiceCollection AddSingletonWithKey(
            this IServiceCollection services,
            Type serviceType,
            Type implementationType,
            object key)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            ServiceCollectionWithKey.AddServiceWithKey(serviceType, implementationType, key);
            return services.AddSingleton(serviceType, implementationType);
        }


        public static IServiceCollection AddSingletonWithKey<TService, TImplementation>(this IServiceCollection services,object key)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return services.AddSingletonWithKey(typeof(TService), typeof(TImplementation),key);
        }

        public static IServiceCollection AddSingletonWithKey(
            this IServiceCollection services,
            Type serviceType,
            object implementationInstance,
            object key)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationInstance == null)
            {
                throw new ArgumentNullException(nameof(implementationInstance));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            ServiceCollectionWithKey.AddServiceWithKey(serviceType, implementationInstance, key);
            //var serviceDescriptor = new ServiceDescriptor(serviceType, implementationInstance);
           // services.Add(serviceDescriptor);
            return services;
        }

        public static IServiceCollection AddSingleton<TService>(
            this IServiceCollection services,
            TService implementationInstance,
            object key)
            where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (implementationInstance == null)
            {
                throw new ArgumentNullException(nameof(implementationInstance));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return services.AddSingletonWithKey(typeof(TService), implementationInstance,key);
        }
    }
}
