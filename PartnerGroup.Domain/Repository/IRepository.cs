using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PartnerGroup.Domain.Repository
{
    public interface IRepository<T>
    {
        T Create(T newEntity);
        T GetSingle(int userId);
        bool Delete(int entityId);
        T Update(T updatedEntity, int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllFromReader(IDataReader reader);
        T GetEntityFromReader(IDataReader reader);
    }
}
