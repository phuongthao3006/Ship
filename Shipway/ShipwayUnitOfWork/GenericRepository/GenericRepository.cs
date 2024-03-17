using ShipwayUnitOfWork.Interface;
using ShipwayUnitOfWork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace ShipwayUnitOfWork.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        private IDbSet<T> _entities;
        private string _errorMessage = string.Empty;
        private bool _isDisposed;
        public ShipwayDbEntities Context { get; set; }
        public GenericRepository(IUnitOfWork<ShipwayDbEntities> unitOfWork) : this(unitOfWork.Context)
        {
        }
        public GenericRepository(ShipwayDbEntities context)
        {
            _isDisposed = false;
            Context = context;
        }

        private IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = Context.Set<T>();
                }
                return _entities;
            }
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }
        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
            _isDisposed = true;
        }

        public virtual bool Delete(object id)
        {
            try
            {
                T entity = GetById(id);
                if (entity == null)
                {
                    return false;
                    throw new ArgumentNullException("entity");
                }
                if (Context == null || _isDisposed)
                {
                    Context = new ShipwayDbEntities();
                }
                Entities.Remove(entity);
                Context.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                return false;
                throw new Exception(_errorMessage, dbEx);
            }
        }

        public IEnumerable<T> GetAll()
        {
            return Entities.ToList();
        }

        public virtual T GetById(object id)
        {
            return Entities.Find(id);
        }

        public virtual bool Insert(T obj)
        {
            try
            {
                if (obj == null)
                    throw new ArgumentNullException("entity");
                Entities.Add(obj);
                if (Context == null || _isDisposed)
                {
                    Context = new ShipwayDbEntities();
                }
                Context.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                return false;
                throw new Exception(_errorMessage, dbEx);
            }
        }

        public virtual bool Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                if (Context == null || _isDisposed)
                    Context = new ShipwayDbEntities();
                SetEntryModified(entity);
                Context.SaveChanges(); //commented out call to SaveChanges as Context save changes will be called with Unit of work
                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                return false;
                throw new Exception(_errorMessage, dbEx);
            }
        }
        public virtual void SetEntryModified(T entity)
        {
            //Context.Entry(entity).State = EntityState.Detached;
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual T Find(Expression<Func<T, bool>> expression)
        {
            return Entities.FirstOrDefault(expression);
        }

        public virtual IEnumerable<T> FindAll(Expression<Func<T, bool>> expression)
        {
            Context.Configuration.ProxyCreationEnabled = false;
            return Entities.Where(expression);
        }
    }
}