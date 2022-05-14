using CarPool.BL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarPool.Common.Enums;
using CarPool.App.Wrappers;

namespace CarPool.App.Wrappers
{
    public class RideWrapper : ModelWrapper<RideModel>
    {
        public RideWrapper(RideModel model)
            : base(model)
        {
        }

        public string? StartLocation
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string? EndLocation
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public DateTime? StartTime
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }

        public int? Duration
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (string.IsNullOrWhiteSpace(StartLocation))
            {
                yield return new ValidationResult($"{nameof(StartLocation)} is required", new[] { nameof(StartLocation) });
            }

            if (string.IsNullOrWhiteSpace(EndLocation))
            {
                yield return new ValidationResult($"{nameof(EndLocation)} is required", new[] { nameof(EndLocation) });
            }

            if (StartTime == default(DateTime))
            {
                yield return new ValidationResult($"{nameof(StartTime)} is required", new[] { nameof(StartTime) });
            }
        }

        public static implicit operator RideWrapper(RideModel rideModel)
            => new(rideModel);

        public static implicit operator RideModel(RideWrapper wrapper)
            => wrapper.Model;
    }
}
