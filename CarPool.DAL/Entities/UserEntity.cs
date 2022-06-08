using System;
using System.Collections.Generic;

namespace CarPool.DAL.Entities;
public record UserEntity (
    Guid Id,
    string? FirstName,
    string? LastName,
    string? PhotoUrl) : IEntity
{
    public UserEntity() : this(default, default, default, default) { }

    public ICollection<CarEntity> Cars { get; init; } = new List<CarEntity>();
    public ICollection<RideEntity> RidesDriver { get; init; } = new List<RideEntity>();
    public ICollection<PassengerEntity> RidesPassenger { get; init; } = new List<PassengerEntity>();
}
