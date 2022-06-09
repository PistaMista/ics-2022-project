using AutoMapper;
using CarPool.DAL.Entities;

namespace CarPool.BL.Models;
public record PassengerInfoModel(
    Guid PassengerId,
    Guid RideId) : ModelBase
{
    public Guid PassengerId { get; set; } = PassengerId;
    public Guid RideId { get; set; } = RideId;
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PassengerEntity, PassengerInfoModel>();
        }
    }
}
