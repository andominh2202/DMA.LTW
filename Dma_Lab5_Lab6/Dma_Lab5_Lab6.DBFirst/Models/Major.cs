using System;
using System.Collections.Generic;

namespace Dma_Lab5_Lab6.DBFirst.Models;

public partial class Major
{
    public int MajorID { get; set; }

    public string MajorName { get; set; } = null!;

    public virtual ICollection<Learner> Learners { get; set; } = new List<Learner>();
}
