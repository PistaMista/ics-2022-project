using AutoMapper;
using CarPool.BL.Models;
using DAL.Entities;
using DAL.UnitOfWork;

namespace CarPool.BL.Facades;

public class UserFacade : CrudFacade<UserEntity, UserInfoModel, UserModel>
{
    public UserFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }
}
