﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public class ComponentAttribute
    {
        public ComponentAttribute(Type erviceType)
        {
            ServiceType = erviceType;
        }

        /// <summary>
        /// 键
        /// </summary>
        public object Key { get; set; }
        /// <summary>
        /// 服务类型
        /// </summary>
        public Type ServiceType { get;  }

        /// <summary>
        /// 生命周期
        /// </summary>
        public ServiceLifetime ServiceLifetime { get; set; } = ServiceLifetime.Transient;
    }
}
