using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
namespace DmaLession07.Models.DataModels
{
    public class Account
    {
        [Key]

        public int Id { get; set; }
        [
            Display(Name = "Họ và tên"),
            Required(ErrorMessage = "Họ và tên không được để trống"),
            MinLength(3, ErrorMessage = "Họ và tên phải có ít nhất 6 ký tự"),
            MaxLength(20, ErrorMessage = "Họ và tên không được vượt quá 20 ký tự")
        ]
        public string FullName { get; set; }
        [
            Display(Name = "Địa chỉ email"),
            Required(ErrorMessage = "Địa chỉ email không được để trống"),
            EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")
        ]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Remote(action: "VeryfyPhone", controller:"Account")]
        public string Phone { get; set; }
        [
            Display(Name = "Địa chỉ"),
            Required(ErrorMessage = "Địa chỉ không được để trống"),
            MinLength(35, ErrorMessage = "Địa chỉ phải có ít nhất 35 ký tự"),
        ]
        public string Address { get; set; }

        [
            Display(Name = "Ảnh đại diện")
        ]
        public string Avatar { get; set; }
        [
            Display(Name = "Ngày sinh"),
            Required(ErrorMessage = "Ngày sinh không được để trống"),
            DataType(DataType.Date, ErrorMessage = "Ngày sinh không hợp lệ")
        ]
        public DateTime Birthday { get; set; }
        [
            Display(Name = "Giới tính"),
        ]
        public string Gender { get; set; }
       
        [
            Display(Name = "Mật khẩu"),
            DataType(DataType.Password),
        ]
        public string Password { get; set; }
        [
            Display(Name = "Facebook"),
            DataType(DataType.Url, ErrorMessage = "Đường dẫn Facebook không hợp lệ"),
            Url(ErrorMessage = "Đường dẫn Facebook không hợp lệ")
        ]
        public string Facebook { get; set; }
    }
}
