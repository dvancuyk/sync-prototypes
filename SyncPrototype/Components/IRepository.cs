using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncPrototype.Components
{
    public interface IRepository<TEntity>
    {
        void Save(TEntity entity);
        IEnumerable<TEntity> All();
    }
}
