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
    public class PassengerFacade
    {
        private readonly UserFacade _userFacade;
        private readonly RideFacade _rideFacade;
        private readonly IMapper _mapper;

        public PassengerFacade(UserFacade userFacade, RideFacade rideFacade, IMapper mapper)
        {
            _userFacade = userFacade;
            _rideFacade = rideFacade;
            _mapper = mapper;
        }

        public async Task<RideModel?> AddPassengerToRide(Guid userId, Guid rideId)
        {
            RideModel? ride = await _rideFacade.GetAsync(rideId);
            UserModel? user = await _userFacade.GetAsync(userId);

            if (ride == null || user == null || ride.DriverId == user.Id)
                return null;

            ride.Passengers.Add(user);
            try
            {
                await _rideFacade.SaveAsync(ride);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return ride;
        }
    }
}
