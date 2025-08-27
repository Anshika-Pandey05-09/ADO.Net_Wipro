using System;
using System.Collections.Generic;

namespace Day33_DataBaseFirstApproach.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string? Title { get; set; }

    public int? TrainerId { get; set; }

    public virtual Trainer? Trainer { get; set; }
}
