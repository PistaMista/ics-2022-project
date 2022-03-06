using System;
using System.Collections.Generic;

namespace DAL.Entities;
public record UserEntity (
    Guid Id,
    string? FirstName,
    string? LastName,
    string? PhotoUrl) : IEntity
{
    public ICollection<CarEntity> Cars { get; init; } = new List<CarEntity>();
    public ICollection<RideEntity> RidesDriver { get; init; } = new List<RideEntity>();
    public ICollection<RideEntity> RidesPassenger { get; init; } = new List<RideEntity>();
}
