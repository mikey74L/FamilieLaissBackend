using FamilieLaissBackend.Data.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissBackend.Data.Repository
{
    public class RepositoryData<TEntity> : iRepositoryData<TEntity> where TEntity : class
    {
        //Protected Members
        protected ObjectContext Context { get; private set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context">Context</param>
        public RepositoryData(IObjectContextAdapter contextAdapter)
        {
            Context = contextAdapter.ObjectContext;
        }

        /// <summary>
        /// Get all Entities for the concrete type
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> All()
        {
            return Context.CreateObjectSet<TEntity>().AsQueryable();
        }

        /// <summary>
        /// Find Entities based on the required predicate
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Context.CreateObjectSet<TEntity>().Where(predicate);
        }

        /// <summary>
        /// Find first or default Entity
        /// </summary>
        /// <param name="predicate">The search predicate</param>
        /// <returns>The Entity</returns>
        public virtual TEntity FirstOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Context.CreateObjectSet<TEntity>().Where(predicate).FirstOrDefault();
        }

        /// <summary>
        /// Find first Entity matching predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>The Entity</returns>
        public virtual TEntity First(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Context.CreateObjectSet<TEntity>().Where(predicate).First();
        }

        /// <summary>
        /// Add Entity to the working Context
        /// </summary>
        /// <param name="entity">The Entity</param>
        public virtual void Add(TEntity entity)
        {
            Context.CreateObjectSet<TEntity>().AddObject(entity);
        }

        /// <summary>
        /// Remove Entity to the working Context
        /// </summary>
        /// <param name="entity">The Entity</param>
        public virtual void Delete(TEntity entity)
        {
            Context.DeleteObject(entity);
        }

        /// <summary>
        /// Attach Entity to the working Context
        /// </summary>
        /// <param name="entity">The Entity</param>
        public virtual void Attach(TEntity entity)
        {
            Context.CreateObjectSet<TEntity>().Attach(entity);
        }
    }
}
