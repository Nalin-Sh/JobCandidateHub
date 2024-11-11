using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Application.Interfaces.Repository
{
    public interface IGenericRepository
    {
        Task<int> Insert<TEntity>(TEntity entity) where TEntity : class;

        Task Update<TEntity>(TEntity entityToUpdate) where TEntity : class;

        Task<bool> Exists<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class;
    }
}
