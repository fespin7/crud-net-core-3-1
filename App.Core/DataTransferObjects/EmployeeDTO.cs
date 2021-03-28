using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.DataTransferObjects
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public string TypeName { get; set; }
    }
}
