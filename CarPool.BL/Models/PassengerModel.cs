using AutoMapper;
using CarPool.DAL.Entities;

namespace CarPool.BL.Models;
public record PassengerModel(
    Guid PassengerId,
    Guid RideId) : ModelBase

{
    public Guid PassengerId { get; set; } = PassengerId;
    public UserModel Passenger { get; set; }
    public Guid RideId { get; set; } = RideId;
    public RideModel Ride { get; set; }

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PassengerEntity, PassengerModel>()
                .ReverseMap();
        }
    }

    public static PassengerModel Empty => new(default, default);
}
