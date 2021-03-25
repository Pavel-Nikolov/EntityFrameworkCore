using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.DataAccess
{
    public interface IRepository<E, K> where E : IEntity<K> where K : IConvertible
    {
        void Create(E item);
        E Read(K key);
        ICollection<E> ReadAll();
        void Update(E item);
        void Delete(K key);
        ICollection<E> Find(string index);
    }
}
