using System;
using System.Collections.Generic;

namespace SyncPrototype.Components
{
    public interface IRepository<TEntity> : IDisposable
    {
        IConnectionFactory Factory { get; }
        void Save(TEntity entity);
        IEnumerable<TEntity> All();
    }
}
