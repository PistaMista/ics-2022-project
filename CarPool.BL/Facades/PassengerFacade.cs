using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using CarPool.BL.Models;
using CarPool.DAL;
using CarPool.DAL.UnitOfWork;
using CarPool.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace CarPool.BL.Facades
{
    public class PassengerFacade : CrudFacade<PassengerEntity, PassengerInfoModel, PassengerModel>
    {
        public PassengerFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
        {
        }

        public async Task<PassengerModel> AddPassengerToRide(Guid userId, Guid rideId)
        {
            PassengerModel passenger = PassengerModel.Empty with { RideId = rideId, PassengerId = userId };
            return await SaveAsync(passenger);
        }
    }
}
