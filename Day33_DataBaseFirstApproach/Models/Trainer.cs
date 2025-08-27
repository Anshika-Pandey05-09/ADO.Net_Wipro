using System;
using System.Collections.Generic;

namespace Day33_DataBaseFirstApproach.Models;

public partial class Trainer
{
    public int TrainerId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
