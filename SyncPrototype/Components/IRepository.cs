using System;
using System.Collections.Generic;

namespace SyncPrototype.Components
{
    public interface IRepository<TEntity> : IDisposable
    {
        int Count { get; }
        IConnectionFactory Factory { get; }
        void Delete(TEntity entity);
        void Save(TEntity entity);
        IEnumerable<TEntity> All();


        /// <summary>
        /// Hackish call to indicate the repository is finished with saving records
        /// </summary>
        void Finish();
        void Reset();
    }
}
