using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Wey.Common;
using Wey.Domain;

namespace Wey.API.Read.EF
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class, IEntity, new()
    {
        /// <summary>
        ///     The _entities context
        /// </summary>
        private readonly DbContext _entitiesContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EntityRepository{T}" /> class.
        /// </summary>
        /// <param name="entitiesContext">The entities context.</param>
        public EntityRepository(DbContext entitiesContext)
        {
            _entitiesContext = entitiesContext;
            Throw.IfNull<ArgumentNullException>(entitiesContext, "entitiesContext");
        }

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <value>
        ///     All.
        /// </value>
        public IQueryable<T> All
        {
            get { return GetAll(); }
        }

        /// <summary>
        /// Alls the including.
        /// </summary>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _entitiesContext.Set<T>();

            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return _entitiesContext.Set<T>();
        }

        /// <summary>
        /// Gets the single.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T GetSingle(Guid key)
        {
            return GetAll().FirstOrDefault(x => x.Key == key);
        }

        /// <summary>
        /// Finds the by.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _entitiesContext.Set<T>().Where(predicate);
        }

        /// <summary>
        /// Paginates the specified page index.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public PaginatedList<T> Paginate<TKey>(int pageIndex
            , int pageSize
            , Expression<Func<T, TKey>> keySelector
            , Expression<Func<T, bool>> predicate
            , params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = AllIncluding(includeProperties).OrderBy(keySelector);

            query = (predicate == null) ? query : query.Where(predicate);

            return query.ToPaginatedList(pageIndex, pageSize);
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry(entity);
            _entitiesContext.Set<T>().Add(entity);
            dbEntityEntry.State = EntityState.Added;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        /// <summary>
        /// Edits the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Edit(T entity)
        {
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            _entitiesContext.SaveChanges();
        }
    }
}