using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public class ComponentAttribute
    {
        /// <summary>
        /// 键
        /// </summary>
        public object Key { get; set; }
        /// <summary>
        /// 服务类型
        /// </summary>
        public Type ServiceType { get; set; }

        /// <summary>
        /// 生命周期
        /// </summary>
        public ServiceLifetime ServiceLifetime { get; set; } = ServiceLifetime.Transient;
    }
}
