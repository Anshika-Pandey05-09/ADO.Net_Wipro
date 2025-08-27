namespace Day33_EF1stDemo.Models;

public class Course
{
    public int CourseId { get; set; }
    public string? Title { get; set; }
    public int TrainerId { get; set; } // it is also a fk
    // Foreign key to the trainer class
    public Trainer? Trainer { get; set; } // This Trainer is the instructor for the course
}