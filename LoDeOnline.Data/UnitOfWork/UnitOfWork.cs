#region

using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;

#endregion

namespace LoDeOnline.Data
{
    public class UnitOfWork : IUnitOfWorkAsync
    {
        #region Private Fields

        private DbContext _dataContext;
        private ObjectContext _objectContext;
        private IDbTransaction _transaction;

        #endregion Private Fields

        #region Constuctor/Dispose

        public UnitOfWork(DbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Dispose()
        {
            if (_objectContext != null && _objectContext.Connection.State == ConnectionState.Open)
            {
                _objectContext.Connection.Close();
            }

            if (_dataContext != null)
            {
                _dataContext.Dispose();
                _dataContext = null;
            }

            GC.SuppressFinalize(this);
        }

        #endregion Constuctor/Dispose

        public int SaveChanges()
        {
            return _dataContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _dataContext.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _dataContext.SaveChangesAsync(cancellationToken);
        }

        #region Unit of Work Transactions

        //IF 04/09/2014 Add IsolationLevel
        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            _objectContext = ((IObjectContextAdapter)_dataContext).ObjectContext;
            if (_objectContext.Connection.State != ConnectionState.Open)
            {
                _objectContext.Connection.Open();
            }

            _transaction = _objectContext.Connection.BeginTransaction(isolationLevel);
            //_transaction = _objectContext.Connection.BeginTransaction();
        }

        public bool Commit()
        {
            _transaction.Commit();
            return true;
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();

                foreach (var entry in _dataContext.ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Deleted:
                            // Note - problem with deleted entities:
                            // When an entity is deleted its relationships to other entities are severed. 
                            // This includes setting FKs to null for nullable FKs or marking the FKs as conceptually null (don’t ask!) 
                            // if the FK property is not nullable. You’ll need to reset the FK property values to 
                            // the values that they had previously in order to re-form the relationships. 
                            // This may include FK properties in other entities for relationships where the 
                            // deleted entity is the principal of the relationship–e.g. has the PK 
                            // rather than the FK. I know this is a pain–it would be great if it could be made easier in the future, but for now it is what it is.
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
            }
        }

        public DbContext DataContext
        {
            get
            {
                return _dataContext;
            }
        }

        #endregion


        public void Dispose(bool disposing)
        {
            if (_transaction != null)
                _transaction.Dispose();

            if (!disposing)
            {
                _dataContext.Dispose();
            }
        }
    }
}