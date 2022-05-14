using CarPool.BL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarPool.Common.Enums;
using CarPool.App.Wrappers;

namespace CarPool.App.Wrappers
{
    public class UserWrapper : ModelWrapper<UserModel>
    {
        public UserWrapper(UserModel model)
            : base(model)
        {
        }

        public string? FirstName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string? LastName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string? PhotoUrl
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (string.IsNullOrWhiteSpace(FirstName))
            {
                yield return new ValidationResult($"{nameof(FirstName)} is required", new[] { nameof(FirstName) });
            }

            if (string.IsNullOrWhiteSpace(LastName))
            {
                yield return new ValidationResult($"{nameof(LastName)} is required", new[] { nameof(LastName) });
            }

        }

        public static implicit operator UserWrapper(UserModel userModel)
            => new(userModel);

        public static implicit operator UserModel(UserWrapper wrapper)
            => wrapper.Model;
    }
}