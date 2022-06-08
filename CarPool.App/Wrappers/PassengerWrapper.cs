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
        public PassengerWrapper(PassengerModel model)
            : base(model)
        {
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return null;
        }

        public static implicit operator PassengerWrapper(PassengerModel userModel)
            => new(userModel);

        public static implicit operator PassengerModel(PassengerWrapper wrapper)
            => wrapper.Model;
    }
}
