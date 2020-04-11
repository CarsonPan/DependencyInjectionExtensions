using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{ 
    public interface IComponentFactory<TKey,TService>
        where TService:class
    {
        TService GetService(TKey key);
        TService GetRequiredService(TKey key);
        TService GetService(TKey key,TKey defaultKey);
        TService GetService(TKey key, TService defaultService);
    }
}
