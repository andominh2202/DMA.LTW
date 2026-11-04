using System;
using System.Collections.Generic;

namespace Dma_Lab5_Lab6.DBFirst.Models;

public partial class Course
{
    public int CourseID { get; set; }

    public string Title { get; set; } = null!;

    public int Credits { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
