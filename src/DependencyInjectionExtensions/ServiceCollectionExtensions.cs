using DependencyInjectionExtensions;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            services.AddTransient(implementationType);
            services.GetServiceContainer().AddServiceWithKey(serviceType, implementationType, key);
            return services;
        }


        public static IServiceCollection AddTransientWithKey<TService, TImplementation>(this IServiceCollection services, object key)
            where TService : class
            where TImplementation : class, TService
        {
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
            services.AddScoped(implementationType);
            services.GetServiceContainer().AddServiceWithKey(serviceType, implementationType, key);
            return services;
        }
        public static IServiceCollection AddScopedWithKey<TService, TImplementation>(this IServiceCollection services, object key)
            where TService : class
            where TImplementation : class, TService
        {
            return services.AddScopedWithKey(typeof(TService), typeof(TImplementation), key);
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
            services.AddSingleton(implementationType);
            services.GetServiceContainer().AddServiceWithKey(serviceType, implementationType, key);
            return services;
        }


        public static IServiceCollection AddSingletonWithKey<TService, TImplementation>(this IServiceCollection services, object key)
            where TService : class
            where TImplementation : class, TService
        {
            return services.AddSingletonWithKey(typeof(TService), typeof(TImplementation), key);
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
            services.AddSingleton(implementationInstance.GetType(), implementationInstance);
            services.GetServiceContainer().AddServiceWithKey(serviceType, implementationInstance.GetType(), key);
            return services;
        }

        public static IServiceCollection AddSingletonWithKey<TService>(
            this IServiceCollection services,
            TService implementationInstance,
            object key)
            where TService : class
        {

            return services.AddSingletonWithKey(typeof(TService), implementationInstance, key);
        }

        public static IServiceCollection AddDependencyInjectionExtensions(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IComponentFactory<,>), typeof(ComponentFactory<,>));
        }
        private static object _root = new object();
        private static ServiceCollectionWithKey GetServiceContainer(this IServiceCollection services)
        {
            var container = services.SingleOrDefault(d => d.ServiceType == typeof(ServiceCollectionWithKey))?.ImplementationInstance as ServiceCollectionWithKey;
            if (container == null)
            {
                lock (_root)
                {
                    container = services.SingleOrDefault(d => d.ServiceType == typeof(ServiceCollectionWithKey))?.ImplementationInstance as ServiceCollectionWithKey;
                    if (container == null)
                    {
                        container = new ServiceCollectionWithKey();
                        services.TryAddSingleton<ServiceCollectionWithKey>(container);
                    }
                }
            }
            return container;
        }

        public static IServiceCollection AddAssemblyByConvention(this IServiceCollection services, Assembly assembly)
        {
            services.Add(assembly, typeof(ISingleton))
                    .Add(assembly, typeof(IScoped))
                    .Add(assembly, typeof(ITransient))
                    .Add(assembly);
            return services;
        }

        public static IServiceCollection RemoveWithKey<TService>(this IServiceCollection services, object key)
        {
            return services.RemoveWithKey(typeof(TService), key);
        }

        public static IServiceCollection RemoveWithKey(this IServiceCollection services, Type serviceType, object key)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Type implementationType = services.GetServiceContainer().GetImplementation(serviceType, key);
            if (implementationType == null)
            {
                return services;
            }
            services.GetServiceContainer().RemoveServiceWithKey(serviceType, key);
            RemoveService(services, implementationType);
            return services;
        }

        private static void RemoveService(IServiceCollection services, Type serviceType)
        {
            for (int i = 0; i < services.Count; i++)
            {//多个注册都移除
                if (services[i].ServiceType == serviceType && services[i].ImplementationType == serviceType)
                {
                    services.Remove(services[i]);
                }
            }
        }

        /// <summary>
        /// 特性标记注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private static IServiceCollection Add(this IServiceCollection services, Assembly assembly)
        {
            IEnumerable<Type> implementationTypes = ReflectionHelper.GetTypesByComponentAttribute(assembly);
            ComponentAttribute component = null;
            foreach (Type implementationType in implementationTypes)
            {
                component = implementationType.GetCustomAttribute<ComponentAttribute>();
                if (component == null)
                {
                    continue;
                }
                if (!component.ServiceType.IsAssignableFrom(implementationType))
                {
                    throw new ArgumentException($"类型{implementationType.FullName}不是派生自类型{component.ServiceType}");
                }
                if (component.Key != null)
                {
                    services.Add(new ServiceDescriptor(implementationType, implementationType, component.ServiceLifetime));
                    services.GetServiceContainer().AddServiceWithKey(component.ServiceType, implementationType, component.Key);
                }
                else
                {
                    services.Add(new ServiceDescriptor(component.ServiceType, implementationType, component.ServiceLifetime));
                }
            }
            return services;
        }
        /// <summary>
        /// 标志接口注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <param name="flagType"></param>
        /// <returns></returns>
        private static IServiceCollection Add(this IServiceCollection services, Assembly assembly, Type flagType)
        {
            IEnumerable<Type> implementationTypes = ReflectionHelper.GetTypes(assembly, flagType);
            Type serviceType = null;
            ServiceLifetime serviceLifetime = ReflectionHelper.GetServiceLifetime(flagType);
            foreach (Type implementationType in implementationTypes)
            {
                serviceType = ReflectionHelper.GetDefaultServiceType(implementationType) ?? implementationType;
                services.Add(new ServiceDescriptor(serviceType, implementationType, serviceLifetime));
            }
            return services;
        }
    }
}
