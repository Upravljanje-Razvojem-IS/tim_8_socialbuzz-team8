using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Data;

namespace ITSolutionsCompanyV1.Repositories
{
    public abstract class GenericRepository<TEntity> where TEntity : class
    {
        protected ProfileDbContext _dataContext;
        private DbSet<TEntity> _table;
    
        protected GenericRepository(ProfileDbContext dataContext)
        {
            _dataContext = dataContext;
            _table = dataContext.Set<TEntity>();
        }
        protected IQueryable<TEntity> Query
        {
            get { return _table; }
        }
        protected List<TEntity> FindAll()
        {
            return _table.ToList();
        }
        protected TEntity FindById(Guid id)
        {
            return _table.Find(id);
        }
        protected void Insert(TEntity entity)
        {
            _table.Add(entity);
            _dataContext.SaveChanges();
        }
        protected void Delete(TEntity entity)
        {
            _table.Remove(entity);
            _dataContext.SaveChanges();
        }
        protected void Update(TEntity entity)
        {
            _table.Update(entity);
            _dataContext.SaveChanges();
        }
    }
}
