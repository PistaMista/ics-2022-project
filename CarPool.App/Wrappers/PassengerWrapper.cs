using CarPool.BL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarPool.Common.Enums;
using CarPool.App.Wrappers;
using System.Linq;
using System.Collections.ObjectModel;
using CarPool.App.Extensions;

namespace CarPool.App.Wrappers
{
    public class PassengerWrapper : ModelWrapper<PassengerModel>
    {
        public UserWrapper? Passenger
        {
            get => GetValue<UserModel>();
            set => SetValue(value);
        }

        public RideWrapper? Ride
        {
            get => GetValue<RideModel>();
            set => SetValue(value);
        }


        public PassengerWrapper(PassengerModel model)
            : base(model)
        {
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return null;
        }

        public static implicit operator PassengerWrapper(PassengerModel passengerModel)
            => new(passengerModel);

        public static implicit operator PassengerModel(PassengerWrapper wrapper)
            => wrapper.Model;
    }
}
