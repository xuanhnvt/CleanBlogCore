using System.Collections.Generic;
using System.Linq;

namespace Nhx.Core.Data
{
    /// <summary>
    /// Represents an entity repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public partial interface IRepository<TEntity>: IRepositoryWithTypedId<TEntity, int> where TEntity : IEntityId<int>
    {

    }
}