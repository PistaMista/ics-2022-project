using AutoMapper;
using CarPool.DAL.Entities;
using CarPool.Common.Enums;

namespace CarPool.BL.Models;
public record CarModel(
    string Manufacturer,
    CarType Type,
    string LicensePlate,
    int SeatCount,
    string PhotoUrl,
    DateTime RegistrationDate,
    Guid CarOwnerId) : ModelBase
{
    public string Manufacturer { get; set; } = Manufacturer;
    public CarType Type { get; set; } = Type;
    public string LicensePlate { get; set; } = LicensePlate;
    public int SeatCount { get; set; } = SeatCount;
    public string PhotoUrl { get; set; } = PhotoUrl;
    public DateTime RegistrationDate { get; set; } = RegistrationDate;
    public Guid CarOwnerId { get; set; } = CarOwnerId;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CarEntity, CarModel>()
                .ReverseMap();
        }
    }
    public static CarModel Empty => new(string.Empty, CarType.None, string.Empty, 0, string.Empty, DateTime.MinValue, Guid.Empty);
}
