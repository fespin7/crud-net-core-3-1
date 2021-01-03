﻿using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<Employee> EmployeeRepository { get; }
        IGenericRepository<EmployeeType> EmployeeTypeRepository { get; }
        void Save();
    }
}