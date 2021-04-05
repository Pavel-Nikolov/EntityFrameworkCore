using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer.DataAccess
{
    public abstract class Repository<E, K> : IRepository<E, K> where E : class, IEntity<K> where K:IConvertible
    {
        protected Context context;
        private DbSet<E> dbSet;
        protected Repository(Context context, DbSet<E> dbSet)
        {
            this.context = context;
            this.dbSet = dbSet;
        }
        public virtual void Create(E item)
        {
            //MapConnections(item);
            dbSet.Add(item);
            context.SaveChanges();
        }

        public virtual void Delete(K key)
        {
            E entityToBeDeleted = dbSet.Find(key);
            if (entityToBeDeleted == null)
            {
                throw new InvalidOperationException("No object to be deleted");
            }
            dbSet.Remove(entityToBeDeleted);
            context.SaveChanges();
        }

        public virtual ICollection<E> Find(string index)
        {
            return dbSet.AsEnumerable().Where(e => e.Index == index).ToList();
        }

        public virtual E Read(K key)
        {
            E readEntity = dbSet.Find(key);
            if (readEntity == null)
            {
                throw new InvalidOperationException("No object to be read");
            }
            
            return readEntity;
        }

        public virtual ICollection<E> ReadAll()
        {
            return dbSet.ToList();
        }

        public virtual void Update(E item)
        {
            //MapConnections(item);
            E entityToBeUpdated = dbSet.Find(item.Key);
            if (entityToBeUpdated == null)
            {
                throw new InvalidOperationException("No object to be updated");
            }
            entityToBeUpdated.UpdateEntity(item);
            context.SaveChanges();
        }

        protected abstract void MapConnections(E entity);
        
    }
}
