﻿using Product.Api.Common.Infrastructure.Persistence.NHibernate;
using Product.Api.Product.Domain.Repository;

namespace Product.Api.Product.Infrastructure.Persistence.NHibernate.Repository
{
    public class ProductNHibernateRepository : BaseNHibernateRepository<Domain.Entity.Product>, IProductRepository
    {
        public ProductNHibernateRepository(UnitOfWorkNHibernate unitOfWork) : base(unitOfWork)
        {
        }
    }
}
