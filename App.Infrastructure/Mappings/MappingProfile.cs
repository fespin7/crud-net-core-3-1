using App.Core.DataTransferObjects;
using App.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO, Employee>();
        }
    }
}
