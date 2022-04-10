using AutoMapper;
using DAL.Entities;

namespace CarPool.BL.Models;
public record RidesListModel(
    DateTime StartTime,
    string StartLocation,
    string EndLocation,
    Guid DriverId) : ModelBase
{
    public DateTime StartTime { get; set; } = StartTime;
    public string StartLocation { get; set; } = StartLocation;
    public string EndLocation { get; set; } = EndLocation;
    public Guid DriverId { get; set; } = DriverId;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RideEntity, RidesListModel>();
        }
    }
}
