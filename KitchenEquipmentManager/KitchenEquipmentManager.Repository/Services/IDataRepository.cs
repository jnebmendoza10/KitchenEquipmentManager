using System;
using System.Collections.Generic;

namespace KitchenEquipmentManager.Repository.Services
{
    public interface IDataRepository<T> where T : class
    {
        T Get(Guid id);

        IEnumerable<T> GetAll();
        void Add(T entity);

        void Remove(Guid id);

        void Update(T entity, Guid id);
    }
}
