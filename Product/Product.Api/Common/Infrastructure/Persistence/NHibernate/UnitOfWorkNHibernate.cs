﻿using NHibernate;

namespace Product.Api.Common.Infrastructure.Persistence.NHibernate
{
    using System.Data;
    using Application;

    public class UnitOfWorkNHibernate : IUnitOfWork
    {
        private readonly SessionFactory _sessionFactory;
        private ISession _session;
        private ITransaction _transaction;

        public UnitOfWorkNHibernate(SessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public ISession GetSession()
        {
            return _session;
        }

        public bool BeginTransaction()
        {
            if (_transaction == null || !_transaction.IsActive)
            {
                _session = _sessionFactory.OpenSession();
                _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
                return true;
            }

            return false;
        }

        public void Commit(bool commit)
        {
            if (_transaction != null && _transaction.IsActive && commit)
            {
                try
                {
                    _transaction.Commit();
                }
                finally
                {
                    _transaction.Dispose();
                    _session.Close();
                    _session.Dispose();
                }
            }
        }

        public void Rollback(bool rollback)
        {
            if (_transaction != null && _transaction.IsActive && rollback)
            {
                try
                {
                    _transaction.Rollback();
                }
                finally
                {
                    _transaction.Dispose();
                    _session.Close();
                    _session.Dispose();
                }
            }
        }
    }
}