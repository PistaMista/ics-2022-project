using AutoMapper;
using CarPool.BL.Models;
using DAL.Entities;
using DAL.UnitOfWork;

namespace CarPool.BL.Facades;

public class CarFacade : CrudFacade<CarEntity, CarInfoModel, CarModel>
{
    public CarFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }
}
