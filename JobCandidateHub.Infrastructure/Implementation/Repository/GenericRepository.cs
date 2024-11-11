using JobCandidateHub.Application.Interfaces.Repository;
using JobCandidateHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Infrastructure.Implementation.Repository
{
    public class GenericRepository(ApplicationDbContext _dbContext) : IGenericRepository
    {
        public async Task<bool> Exists<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
        {
            return filter != null && await _dbContext.Set<TEntity>().AnyAsync(filter);
        }

        public async Task<int> Insert<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null) throw new ArgumentNullException("Entity");

            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            var ret = 0;
            var key = typeof(TEntity).GetProperties().FirstOrDefault(p =>
                p.CustomAttributes.Any(attr => attr.AttributeType == typeof(KeyAttribute)));

            if (key != null)
            {
                var keyType = key.PropertyType;

                if (keyType == typeof(int))
                {
                    ret = (int)key.GetValue(entity, null)!;
                }
                else if (keyType == typeof(long))
                {
                    ret = Convert.ToInt32(key.GetValue(entity, null));
                }
            }

            return ret;
        }

        public async Task Update<TEntity>(TEntity entityToUpdate) where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entityToUpdate);

            _dbContext.Update(entityToUpdate);

            await _dbContext.SaveChangesAsync();
        }
    }
}
