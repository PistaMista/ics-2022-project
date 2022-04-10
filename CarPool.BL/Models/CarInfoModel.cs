using AutoMapper;
using DAL.Entities;
using Common.Enums;

namespace CarPool.BL.Models;
public record CarInfoModel(
    string Manufacturer,
    CarType Type,
    string LicensePlate,
    int SeatCount,
    string PhotoUrl) : ModelBase
{
    public string Manufacturer { get; } = Manufacturer;
    public CarType Type { get; } = Type;
    public string LicensePlate { get; } = LicensePlate;
    public int SeatCount { get; } = SeatCount;
    public string PhotoUrl { get; } = PhotoUrl;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CarEntity, CarInfoModel>();
        }
    }
    public static CarInfoModel Empty => new(string.Empty, CarType.None, string.Empty, 0, string.Empty);
}
