using App.Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.TypeId).InclusiveBetween(1,3).WithMessage("Please select a type");
            RuleFor(r => r.EmploymentDate).NotNull();
        }
        
    }
}
