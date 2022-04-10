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
    public string Manufacturer { get; set; } = Manufacturer;
    public CarType Type { get; set; } = Type;
    public string LicensePlate { get; set; } = LicensePlate;
    public int SeatCount { get; set; } = SeatCount;
    public string PhotoUrl { get; set; } = PhotoUrl;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CarEntity, CarInfoModel>();
        }
    }
    public static CarInfoModel Empty => new(string.Empty, CarType.None, string.Empty, 0, string.Empty);
}
