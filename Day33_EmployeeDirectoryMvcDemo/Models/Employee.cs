using System;
using System.Collections.Generic;

namespace Day33_EmployeeDirectoryMvcDemo.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public DateOnly JoinDate { get; set; }

    public decimal Salary { get; set; }
}
