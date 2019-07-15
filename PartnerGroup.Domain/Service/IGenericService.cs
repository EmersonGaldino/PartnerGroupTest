using PartnerGroup.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerGroup.Domain.Service
{
    public interface IGenericService<T> where T : Entity
    {
        APIReturn Create(T toCreate);
        APIReturn GetSingle(int id);
        APIReturn Update(T toUpdate, int id);
        APIReturn Delete(int id);
        APIReturn GetAll();
    }
}
