using System.ComponentModel.DataAnnotations;

namespace Day33_EF1stDemo.Models;

public class Student
{
    public int StudentId { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Email { get; set; }

    // Any changes to your domain classes will change your database after migrations
    // In code first we create domain classes And they converted to database tables into migration commands
}