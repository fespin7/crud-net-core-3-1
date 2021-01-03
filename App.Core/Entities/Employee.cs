using System;

namespace App.Core.Entities
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? EmploymentDate { get; set; }

        public virtual EmployeeType Type { get; set; }
    }
}
