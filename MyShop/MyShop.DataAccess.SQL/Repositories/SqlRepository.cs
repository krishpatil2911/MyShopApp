using MyShop.Core.Models;
using MyShop.DataAccess.Contracts;
using MyShop.DataAccess.SQL.DbClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL.Repositories
{
    public class SqlRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal DataContext dataContext;
        internal DbSet<T> dbSet;

        public SqlRepository(DataContext context)
        {
            dataContext = context;
            this.dbSet = context.Set<T>();
        }
        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            dataContext.SaveChanges();
        }

        public void Delete(string Id)
        {
            var t = dbSet.Find(Id);
            if (dataContext.Entry(t).State == EntityState.Detached)
                dbSet.Attach(t);

            dbSet.Remove(t);
        }

        public T Find(string Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(T item)
        {
            dbSet.Add(item);
        }

        public void Update(T item)
        {
            dbSet.Attach(item);
            dataContext.Entry(item).State = EntityState.Modified;
        }
    }
}
