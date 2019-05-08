using System;
using System.Collections.Generic;
using System.Text;

namespace DuckGo.DependencyInjection
{
    public interface IComponentFactory<TKey,TService>
    {
        TService GetService(TKey key);
        TService GetRequiredService(TKey key);
        TService GetService(TKey key,TKey defaultKey);
        TService GetService(TKey key, TService defaultService);
    }
}
