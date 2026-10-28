using Dma_Lab3_Lab4.Models;
using System;
using System.Collections.Generic;

namespace Dma_Lab3_Lab4.Models
{
    public class Learner
    {
        public Learner()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        public int LearnerID { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstMidName { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public int MajorID { get; set; }

        public virtual Major? Major { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}