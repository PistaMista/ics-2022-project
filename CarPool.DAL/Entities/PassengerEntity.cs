using System;
using System.Collections.Generic;

namespace CarPool.DAL.Entities;
public record PassengerEntity(
    Guid Id,
    Guid PassengerId,
    Guid RideId) : IEntity
{
    public PassengerEntity() : this(default, default, default) { }

    public UserEntity? Passenger { get; init; }
    public RideEntity? Ride { get; init; }
}
