using Day33_DataBaseFirstApproach.Models;

using var context = new Day33DatabaseFirstApproachDbContext();

// Fetch Students
Console.WriteLine("Students:");
foreach (var s in context.Students.ToList())
{
    Console.WriteLine($"{s.StudentId} - {s.Name} - {s.Email}");
}

// Fetch Trainers
Console.WriteLine("\nTrainers:");
foreach (var t in context.Trainers.ToList())
{
    Console.WriteLine($"{t.TrainerId} - {t.Name}");
}

// Fetch Courses
Console.WriteLine("\nCourses:");
foreach (var c in context.Courses.ToList())
{
    Console.WriteLine($"{c.CourseId} - {c.Title} - TrainerId: {c.TrainerId}");
}