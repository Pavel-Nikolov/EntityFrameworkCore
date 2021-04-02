using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainLayer.Models
{
    public interface IEntity<K> where K : IConvertible
    {
        
        public K Key { get;}
        public string Index { get; }       
        
        public void UpdateEntity(IEntity<K> newEntity);

    }
}
