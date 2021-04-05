using DomainLayer.Models;
using ServiceLayer.Manipulations.PropertyGetters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.Manipulations
{
    public static class Manipulate<E, K> where E : IEntity<K> where K : IConvertible
    {
        public static ICollection<E> OrderBy(IEnumerable<E> es, PropertyGetter<E, K> propertyGetter, bool assending = true)
        {
            if (assending)
            {
                return es.OrderBy(x => propertyGetter.GetProperty(x)).ToList();
            }
            else
            {
                return es.OrderByDescending(x => propertyGetter.GetProperty(x)).ToList();
            }
        }

        public static ICollection<E> Filter(IEnumerable<E> es, PropertyGetter<E, K> propertyGetter, object value)
        {
            return es.Where(x => ((object)propertyGetter.GetProperty(x)).ToString() == value.ToString()).ToList();
        }
    }
}
