using DependencyInjection;
using DuckGo.DependencyInjection;
using System;
using System.Collections.Generic;
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
            ServiceCollectionWithKey.AddServiceWithKey(serviceType, implementationType, key);
            return services.AddTransient(implementationType);
        }


        public static IServiceCollection AddTransientWithKey<TService, TImplementation>(this IServiceCollection services, object key)
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
            return services.AddScoped(implementationType);
        }
        public static IServiceCollection AddScopedWithKey<TService, TImplementation>(this IServiceCollection services, object key)
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
            ServiceCollectionWithKey.AddServiceWithKey(serviceType, implementationType, key);
            return services.AddSingleton(implementationType);
        }


        public static IServiceCollection AddSingletonWithKey<TService, TImplementation>(this IServiceCollection services, object key)
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
            ServiceCollectionWithKey.AddServiceWithKey(serviceType, implementationInstance, key);
            //var serviceDescriptor = new ServiceDescriptor(serviceType, implementationInstance);
            // services.Add(serviceDescriptor);
            return services;
        }

        public static IServiceCollection AddSingletonWithKey<TService>(
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
            return services.AddSingletonWithKey(typeof(TService), implementationInstance, key);
        }

        public static IServiceCollection AddIocExtension(this IServiceCollection services)
        {
            return services.AddScoped<IServiceProvider>(sp => sp.CreateScope().ServiceProvider)
                           .AddScoped(typeof(IComponentFactory<,>), typeof(ComponentFactory<,>));
        }

        public static IServiceCollection AddAssemblyByConvention(this IServiceCollection services, Assembly assembly)
        {
            services.Add(assembly, typeof(ISingleton))
                    .Add(assembly, typeof(IScoped))
                    .Add(assembly, typeof(ITransient))
                    .Add(assembly);
            return services;
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
                component=implementationType.GetCustomAttribute<ComponentAttribute>();
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
                    ServiceCollectionWithKey.AddServiceWithKey(component.ServiceType, implementationType, component.Key);
                    services.Add(new ServiceDescriptor(implementationType, implementationType, component.ServiceLifetime));
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
