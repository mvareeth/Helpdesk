using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpdesk.Data
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private AppDBContext context;

        public EFUnitOfWork(AppDBContext context)
        {
            this.context = context;
        }

        public ITransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Snapshot)
        {
            return new DbTransaction(context.Database.BeginTransaction(isolationLevel));
        }

        public void Add<T>(T obj)
            where T : class
        {
            var set = context.Set<T>();
            set.Add(obj);
        }

        public void Update<T>(T obj)
            where T : class
        {
            var set = context.Set<T>();
            set.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
        }

        void IUnitOfWork.Remove<T>(T obj)
        {
            var set = context.Set<T>();
            set.Remove(obj);
        }

        public IQueryable<T> Query<T>()
            where T : class
        {
            return context.Set<T>();
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Attach<T>(T newUser) where T : class
        {
            var set = context.Set<T>();
            set.Attach(newUser);
        }

        public void Dispose()
        {
            context = null;
        }
    }
}
