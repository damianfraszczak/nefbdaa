using Microsoft.EntityFrameworkCore;
using NEFBDAACommons.Database.Extensions;
using NEFBDAACommons.Database.Models;
using NEFBDAACommons.Database.Models.PagedList;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Z.EntityFramework.Plus;

namespace NEFBDAACommons.Database.Services.Repository
{
    public interface IRepositoryService
    {
    }


    public interface IGenericRepository<T> : IRepositoryService where T : class, IEntity
    {
        #region add

        void Add(T item);

        Task AddAsync(T item);

        void AddRange(IEnumerable<T> items);

        Task AddRangeAsync(IEnumerable<T> items);

        #endregion

        #region aggregated

        QueryFutureValue<bool> Any();

        QueryFutureValue<bool> Any(Expression<Func<T, bool>> where);

        Task<bool> AnyAsync();

        Task<bool> AnyAsync(Expression<Func<T, bool>> where);

        QueryFutureValue<int> Count();

        QueryFutureValue<int> Count(Expression<Func<T, bool>> where);

        Task<long> CountAsync();
        Task<long> CountAsync(Expression<Func<T, bool>> where);

        IQueryable<T> AsQueryable();
        void ExpireCache(IEnumerable<T> items);

        #endregion

        #region delete

        void Delete(object key);
        void Delete(T value);
        void Delete(Expression<Func<T, bool>> where);

        Task DeleteAsync(object key);

        Task DeleteAsync(Expression<Func<T, bool>> where);

        #endregion

        #region update

        void Update(T item);

        Task UpdateAsync(T item);

        #endregion

        #region loadmany

        QueryFutureEnumerable<T> GetAll();
        IEnumerable<T> GetAllFromCache();
        QueryFutureEnumerable<T> GetAll(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAllFromCache(Expression<Func<T, bool>> where);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where);

        QueryFutureEnumerable<T> GetAll(params Expression<Func<T, object>>[] include);
        QueryFutureEnumerable<T> GetAll(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] include);

        QueryFutureEnumerable<T> GetAll(Expression<Func<T, bool>> where, IEnumerable<Order> orders,
            params Expression<Func<T, object>>[] include);

        IEnumerable<T> GetAllFromCache(Expression<Func<T, bool>> where,
            IEnumerable<Order> orders,
            params Expression<Func<T, object>>[] include);

        PagedList<T> GetAll(PagedListParameters parameters, params Expression<Func<T, object>>[] include);

        PagedList<T> GetAll(PagedListParameters parameters, Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] include);


        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] include);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] include);

        #endregion

        #region load one

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where);

        T GetById(object key, params Expression<Func<T, object>>[] include);
        T GetByIdFromCache(object key, params Expression<Func<T, object>>[] include);
        QueryFutureValue<T> GetByGuid(Guid guid);
        T GetByGuidFromCache(Guid guid);
        QueryFutureValue<T> GetByGuid(string guid);
        T GetByGuidFromCache(string guid);
        ValueTask<T> GetByIdAsync(object key);
        QueryFutureValue<T> FirstOrDefault(Expression<Func<T, bool>> where);
        T FirstOrDefaultFromCache(Expression<Func<T, bool>> where);
        QueryFutureValue<T> FirstOrDefault(params Expression<Func<T, object>>[] include);
        T FirstOrDefaultFromCache(params Expression<Func<T, object>>[] include);

        QueryFutureValue<T> FirstOrDefault(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] include);

        T FirstOrDefaultFromCache(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] include);

        Task<T> FirstOrDefaultAsync(params Expression<Func<T, object>>[] include);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] include);
        void ChangeDetectChangesLazyLoading(bool lazyLoading);

        Type GetRepositoryModel();

        #endregion


        void SaveChanges();
        void SaveChanges(AuthenticatedUserViewModel authenticatedUser);

        AbstractDbContext GetContext();
    }

    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        public MemoryCacheEntryOptions CacheOptions { get; set; } = new MemoryCacheEntryOptions();
        protected DbSet<T> Set => Context.Set<T>();

        protected AbstractDbContext Context { get; }

        public GenericRepository(AbstractDbContext context)
        {
            Context = context;
            ChangeDetectChangesLazyLoading(false);
        }

        #region add

        public void Add(T item)
        {
            Set.Add(item);
            ExpireCache(new[] {item});
        }

        public void ExpireCache(IEnumerable<T> items)
        {
            QueryCacheManager.ExpireTag(GetCacheTag());
        }

        public async Task AddAsync(T item)
        {
            await Set.AddAsync(item).ConfigureAwait(false);
            ExpireCache(new[] {item});
        }

        public void AddRange(IEnumerable<T> items)
        {
            Set.AddRange(items);
            ExpireCache(items);
        }

        public async Task AddRangeAsync(IEnumerable<T> items)
        {
            await Set.AddRangeAsync(items).ConfigureAwait(false);
            ExpireCache(items);
        }

        #endregion

        #region update

        //TODO usunac key
        public void Update(T item)
        {
            Set.Update(item);
            ExpireCache(new[] {item});
        }

        public void ChangeDetectChangesLazyLoading(bool lazyLoading)
        {
            Context.DetectChangesLazyLoading(lazyLoading);
        }

        public async Task UpdateAsync(T item)
        {
            await Task.Run(() => Update(item)).ConfigureAwait(false);
            ExpireCache(new[] {item});
        }

        #endregion

        #region delete

        public void Delete(object key)
        {
            Delete(GetById(key));
        }

        public void Delete(T value)
        {
            Set.Remove(value);
            ExpireCache(new[] {value});
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            ChangeDetectChangesLazyLoading(true);
            Set.Where(where).Delete();
            ChangeDetectChangesLazyLoading(false);
            ExpireCache(null);
        }

        public async Task DeleteAsync(object key)
        {
            await Task.Run(() => Delete(key)).ConfigureAwait(false);
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> where)
        {
            await Task.Run(() => Delete(where)).ConfigureAwait(false);
        }

        #endregion

        #region agregated

        public QueryFutureValue<bool> Any()
        {
            return Set.DeferredAny().FutureValue();
        }

        public QueryFutureValue<bool> Any(Expression<Func<T, bool>> where)
        {
            return Set.DeferredAny(where).FutureValue();
        }

        public Task<bool> AnyAsync()
        {
            return Set.AnyAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            return Set.AnyAsync(where);
        }

        public QueryFutureValue<int> Count()
        {
            return Set.DeferredCount().FutureValue();
        }

        public QueryFutureValue<int> Count(Expression<Func<T, bool>> where)
        {
            return Set.DeferredCount(where).FutureValue();
        }

        public Task<long> CountAsync()
        {
            return Set.LongCountAsync();
        }

        public Task<long> CountAsync(Expression<Func<T, bool>> where)
        {
            return Set.LongCountAsync(where);
        }

        public IQueryable<T> AsQueryable()
        {
            return Set.AsQueryable();
        }

        #endregion

        #region load one

        public QueryFutureValue<T> FirstOrDefault(Expression<Func<T, bool>> where)
        {
            return Set.Where(where).DeferredFirstOrDefault().FutureValue();
        }

        public T FirstOrDefaultFromCache(Expression<Func<T, bool>> where)
        {
            return Set.Where(where).DeferredFirstOrDefault().FromCache();
        }

        public QueryFutureValue<T> FirstOrDefault(params Expression<Func<T, object>>[] include)
        {
            return Set.Include(include).DeferredFirstOrDefault().FutureValue();
        }

        public T FirstOrDefaultFromCache(params Expression<Func<T, object>>[] include)
        {
            return Set.Include(include).DeferredFirstOrDefault().FromCache();
        }

        public QueryFutureValue<T> FirstOrDefault(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] include)
        {
            return Set.Where(where).Include(include).DeferredFirstOrDefault().FutureValue();
        }

        public T FirstOrDefaultFromCache(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] include)
        {
            return Set.Where(where).Include(include).DeferredFirstOrDefault().FromCache();
        }

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where)
        {
            return Set.Where(where).FirstOrDefaultAsync();
        }

        public Task<T> FirstOrDefaultAsync(params Expression<Func<T, object>>[] include)
        {
            return Set.Include(include).FirstOrDefaultAsync();
        }

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] include)
        {
            return Set.Where(where).Include(include).FirstOrDefaultAsync();
        }


        public T GetById(object key, params Expression<Func<T, object>>[] include)
        {
            if (include != null)
            {
                return Set.Include(include)
                    .FilterByPrimaryKey(Context, new[] {key})
                    .First();
            }
            else
            {
                return Set.Find(key);
            }
        }

        public T GetByIdFromCache(object key, params Expression<Func<T, object>>[] include)
        {
            if (include != null)
            {
                return Set.Include(include)
                    .FilterByPrimaryKey(Context, new[] {key})
                    .FromCache(CacheOptions, GetCacheTag())
                    .First();
            }
            else
            {
                return Set.Find(key);
            }
        }

        public T GetByIdCache(object key, params Expression<Func<T, object>>[] include)
        {
            if (include != null)
            {
                return Set.Include(include)
                    .FilterByPrimaryKey(Context, new[] {key})
                    .FromCache(CacheOptions, GetCacheTag()).First();
            }
            else
            {
                return Set.FilterByPrimaryKey(Context, new[] {key})
                    .FromCache(CacheOptions, GetCacheTag()).First();
            }
        }


        public QueryFutureValue<T> GetByGuid(Guid guid)
        {
            return Set.Where(x => x.Guid == guid && !x.IsDeleted).DeferredFirstOrDefault().FutureValue();
        }

        public T GetByGuidFromCache(Guid guid)
        {
            return Set.Where(x => x.Guid == guid && !x.IsDeleted).DeferredFirstOrDefault()
                .FromCache(CacheOptions, GetCacheTag());
        }

        public QueryFutureValue<T> GetByGuid(string guid) => GetByGuid(Guid.Parse(guid));

        public T GetByGuidFromCache(string guid)
        {
            return GetByGuidFromCache(Guid.Parse(guid));
        }

        public ValueTask<T> GetByIdAsync(object key)
        {
            return Set.FindAsync(key);
        }

        #endregion

        #region loadmany

        public QueryFutureEnumerable<T> GetAll()
        {
            return Set.Future();
        }

        public IEnumerable<T> GetAllFromCache()
        {
            return Set.FromCache(CacheOptions, GetCacheTag());
        }

        public QueryFutureEnumerable<T> GetAll(Expression<Func<T, bool>> where)
        {
            return Set.Where(where).Future();
        }

        public IEnumerable<T> GetAllFromCache(Expression<Func<T, bool>> @where)
        {
            return Set.Where(where).FromCache(CacheOptions, GetCacheTag());
        }

        public QueryFutureEnumerable<T> GetAll(params Expression<Func<T, object>>[] include)
        {
            return Set.Include(include).Future();
        }

        public QueryFutureEnumerable<T> GetAll(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] include)
        {
            return GetAll(where, GetDefaultMultipleOrder(), include);
        }

        public QueryFutureEnumerable<T> GetAll(Expression<Func<T, bool>> where, IEnumerable<Order> orders,
            params Expression<Func<T, object>>[] include)
        {
            return Set.Where(where).Include(include).OrderByMultiple(orders).Future();
        }

        public IEnumerable<T> GetAllFromCache(Expression<Func<T, bool>> @where, IEnumerable<Order> orders,
            params Expression<Func<T, object>>[] include)
        {
            return Set.Where(where).Include(include).OrderByMultiple(orders).FromCache(CacheOptions, GetCacheTag());
        }

        public string GetCacheTag()
        {
            return GetRepositoryModel().Name;
        }


        public PagedList<T> GetAll(PagedListParameters parameters, params Expression<Func<T, object>>[] include)
        {
            return new PagedList<T>(Set.Include(include), parameters);
        }


        public PagedList<T> GetAll(PagedListParameters parameters, Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] include)
        {
            return new PagedList<T>(Set.Where(where).Include(include), parameters);
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Set.ToListAsync().ConfigureAwait(false);
        }


        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where)
        {
            return await Set.Where(where).ToListAsync().ConfigureAwait(false);
        }


        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] include)
        {
            return await Set.Include(include).ToListAsync().ConfigureAwait(false);
        }


        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] include)
        {
            return await Set.Where(where).Include(include).ToListAsync().ConfigureAwait(false);
        }

        #endregion

        protected virtual IEnumerable<Order> GetDefaultMultipleOrder()
        {
            return new List<Order>() {new Order() {Property = "Id", IsAscending = true}};
        }

        public Type GetRepositoryModel()
        {
            return typeof(T);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public void SaveChanges(AuthenticatedUserViewModel authenticatedUser)
        {
            Context.SaveChanges(authenticatedUser);
        }

        public AbstractDbContext GetContext()
        {
            return Context;
        }
    }
}