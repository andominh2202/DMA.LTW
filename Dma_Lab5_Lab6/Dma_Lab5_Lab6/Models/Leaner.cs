using System.ComponentModel.DataAnnotations;
using Dma_Lab5_Lab6.Models;
using System;
using System.Collections.Generic;

namespace Dma_Lab5_Lab6.Models
{
    public class Learner
    {
        public Learner()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        [Display(Name = "Mã người học")]
        public int LearnerID { get; set; }
        [Display(Name = "Họ")]
        public string LastName { get; set; } = string.Empty;
        [Display(Name = "Tên đệm và tên")]
        public string FirstMidName { get; set; } = string.Empty;
        [Display(Name = "Ngày nhập học")]
        public DateTime EnrollmentDate { get; set; }
        [Display(Name = "Chuyên ngành")]
        public int MajorID { get; set; }

        [Display(Name = "Chuyên ngành")]
        public virtual Major? Major { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}