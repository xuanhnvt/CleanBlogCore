using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nhx.Core;
using Nhx.Core.Data;

namespace CleanBlog.Data
{
    /// <summary>
    /// Represents the Entity Framework repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public partial class EfRepository<TEntity> : EfRepositoryWithTypedId<TEntity, int>, IRepository<TEntity> where TEntity : class, IEntityId<int>
    {
        #region Fields
        
        #endregion

        #region Ctor
        
        public EfRepository(IDbContext context): base(context)
        {

        }

        #endregion
    }
}