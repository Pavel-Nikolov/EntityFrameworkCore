using DomainLayer.Models;
using System;

namespace ServiceLayer.Manipulations.PropertyGetters
{
    public abstract class PropertyGetter<E, K> where E : IEntity<K> where K:IConvertible
    {
        protected string propertyName;
        public PropertyGetter(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public abstract IComparable GetProperty(E entity);
    }
}