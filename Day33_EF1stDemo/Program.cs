// See https://aka.ms/new-console-template for more information
using System.Data.Common;
using Day33_EF1stDemo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Day33_EF1stDemo.Models;



// var config = new ConfigurationBuilder()
//     .AddJsonFile("appsettings.json")
//     .Build();

var optionsBuilder = new DbContextOptionsBuilder<TrainingContext>();
optionsBuilder.UseSqlServer("Data Source=DELL;Initial Catalog=Day32_BookDB;Integrated Security=True; TrustServerCertificate=True;");

using var context = new TrainingContext(optionsBuilder.Options);

//To retrieve all students from the database

var students = context.Students.ToList();
Console.WriteLine("Students in DB:");
foreach (var s in students)
{
    Console.WriteLine($"{s.StudentId} - {s.Name}");
}

var trainers = context.Trainers.ToList();
Console.WriteLine("Trainers in DB:");
foreach (var t in trainers)
{
    Console.WriteLine($"{t.TrainerId} - {t.Name}");
}

var courses = context.Courses.ToList();
Console.WriteLine("Courses in DB:");
foreach (var c in courses)
{
    Console.WriteLine($"{c.CourseId} - {c.Title} - TrainerId:{c.TrainerId}");
}


//This will seed the database with initial data
if (!context.Students.Any())
{
    var trainer = new Trainer { Name = "Salman khan" };
    var student = new Student { Name = "Student1" };
    var course = new Course { Title = "Driving", Trainer = trainer };

    context.Trainers.Add(trainer);
    context.Students.Add(student);
    context.Courses.Add(course);

    context.SaveChanges();
    Console.WriteLine("Sample data inserted!");
}
else
{
    Console.WriteLine("Database already has data.");
}

Console.WriteLine("Hello, World!");


// var connString = "Data Source=DELL;Initial Catalog=Day32_BookDB;Integrated Security=True;TrustServerCertificate=True;";