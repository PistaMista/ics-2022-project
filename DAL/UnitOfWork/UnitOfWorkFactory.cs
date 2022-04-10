using System;
using Microsoft.EntityFrameworkCore;

namespace DAL.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<CarRideDbContext> _dbContextFactory;

    public UnitOfWorkFactory(IDbContextFactory<CarRideDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    public IUnitOfWork Create() => new UnitOfWork(_dbContextFactory.CreateDbContext());
}
