﻿namespace EmployeeManagement.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public bool Active { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

    }
}
