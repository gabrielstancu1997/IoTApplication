using IoTApplication.Entities;
using System.Collections.Generic;

namespace IoTApplication.IRepositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        List<T> GetAllActive();
        List<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void HardDelete(T entity);
        void CreateRange(List<T> entities);

        T FindById(int id);

        bool SaveChanges();
    }
}
