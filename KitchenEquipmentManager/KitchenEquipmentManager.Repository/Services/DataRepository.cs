using System;
using System.Collections.Generic;
using System.Linq;
using KitchenEquipmentManager.Domain.Models;

namespace KitchenEquipmentManager.Repository.Services
{
    public class DataRepository<T> : IDataRepository<T> where T : BaseDomain
    {
        private readonly IKitchenManagerDbContextFactory _contextFactory;

        public DataRepository(IKitchenManagerDbContextFactory contextFactory) 
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        public void Add(T entity)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var addedEntity = context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }

        public T Get(Guid id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                T entity = context.Set<T>().FirstOrDefault((e) => e.Id ==  id);

                return entity;
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = context.Set<T>().ToList();

                return entities;
            }
        }

        public void Remove(Guid id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                T retrievedEntity = context.Set<T>().FirstOrDefault((e) => e.Id == id);
                context.Set<T>().Remove(retrievedEntity);
            }
        }

        public void Update(T entity, Guid id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                T retrievedEntity = context.Set<T>().FirstOrDefault((e) => e.Id == id);

                if (entity != null)
                {
                    retrievedEntity = entity;
                    context.SaveChanges();
                }
            }
        }
    }
}
