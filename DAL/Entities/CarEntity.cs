using System;
using Common.Enums;

namespace DAL.Entities;
public record CarEntity (
    Guid Id,
    string? Manufacturer,
    CarType Type,
    string? LicensePlate,
    DateTime RegistrationDate,
    int SeatCount,
    string? PhotoUrl,
    Guid CarOwnerId) : IEntity
{

#nullable disable
    public CarEntity() : this(default, default, default, default, default, default, default, default) { }
#nullable enable
    public UserEntity? CarOwner { get; init; }
}
