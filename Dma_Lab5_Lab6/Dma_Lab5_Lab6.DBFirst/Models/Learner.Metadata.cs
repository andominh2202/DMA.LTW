using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
namespace Dma_Lab5_Lab6.DBFirst.Models;
[ModelMetadataType(typeof(LearnerMetadata))]
public partial class Learner { }
public class LearnerMetadata
{
    [Display(Name = "Họ"), Required(ErrorMessage = "Vui lòng nhập họ")]
    public string LastName { get; set; } = "";
    [Display(Name = "Tên đệm và tên"), Required(ErrorMessage = "Vui lòng nhập tên")]
    public string FirstMidName { get; set; } = "";
    [Display(Name = "Ngày nhập học")]
    public DateTime EnrollmentDate { get; set; }
    [Display(Name = "Chuyên ngành")]
    public int MajorID { get; set; }
    [Display(Name = "Chuyên ngành"), ValidateNever]
    public Major Major { get; set; } = null!;
}
