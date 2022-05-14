using CarPool.BL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarPool.Common.Enums;
using CookBook.App.Wrappers;

namespace CarPool.App.Wrappers
{
    public class CarWrapper : ModelWrapper<CarModel>
    {
        public CarWrapper(CarModel model)
            : base(model)
        {
        }

        public Guid CarId
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }


        public string? Manufacturer
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public CarType? Type
        {
            get => GetValue<CarType>();
            set => SetValue(value);
        }

        public string? LicensePlate
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public int? SeatCount
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        public string? PhotoUrl
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (string.IsNullOrWhiteSpace(Manufacturer))
            {
                yield return new ValidationResult($"{nameof(Manufacturer)} is required", new[] {nameof(Manufacturer)});
            }

            if (string.IsNullOrWhiteSpace(LicensePlate))
            {
                yield return new ValidationResult($"{nameof(LicensePlate)} is required", new[] {nameof(LicensePlate)});
            }

            if (SeatCount == 0)
            {
                yield return new ValidationResult($"{nameof(SeatCount)} is required", new[] {nameof(SeatCount)});
            }
        }

        public static implicit operator CarWrapper(CarModel carModel)
            => new(carModel);

        public static implicit operator CarModel(CarWrapper wrapper)
            => wrapper.Model;
    }
}
