using System;
using System.Collections.Generic;

namespace CarPool.DAL.Entities;
public record RideEntity(
    Guid Id,
    string? StartLocation,
    string? EndLocation,
    DateTime StartTime,
    uint Duration,
    Guid CarId,
    Guid DriverId) : IEntity
{
    public RideEntity() : this(default, default, default, default, default, default, default) { }

    public CarEntity? Car { get; init; }
    public UserEntity? Driver { get; init; }
    public ICollection<PassengerEntity> Passengers { get; init; } = new List<PassengerEntity>();
}
