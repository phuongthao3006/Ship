using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ShipwayUnitOfWork.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        bool Insert(T obj);
        bool Update(T obj);
        bool Delete(object id);
        T Find(Expression<Func<T, bool>> expression);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> expression);
    }
}