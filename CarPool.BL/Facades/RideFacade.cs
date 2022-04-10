using AutoMapper;
using CarPool.BL.Models;
using DAL.Entities;
using DAL.UnitOfWork;

namespace CarPool.BL.Facades;

public class RideFacade : CrudFacade<RideEntity, RideInfoModel, RideModel>
{
    public RideFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }
}
