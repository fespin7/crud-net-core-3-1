using System;
using System.Collections.Generic;

namespace App.Core.Entities
{
    public partial class EmployeeType
    {
        public EmployeeType()
        {
            Employee = new HashSet<Employee>();
        }

        public int TypeId { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
