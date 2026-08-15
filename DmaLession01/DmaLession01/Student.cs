using System;

namespace DmaLession01
{
    internal class Student
    {
        // Properties
        public string masv { get; set; }
        public string hoTen { get; set; }
        public DateTime ngaySinh { get; set; }
        public bool gioiTinh { get; set; }  // true = Nam, false = Nữ
        public string email { get; set; }
        public string soDienThoai { get; set; }
        public string nganhHoc { get; set; }
        public float diemTrungBinh { get; set; }
        public string trangThaiHocTap { get; set; }  // "Đang học" hoặc "Nghỉ học"

        // Constructor mặc định
        public Student()
        {
            masv = string.Empty;
            hoTen = string.Empty;
            ngaySinh = DateTime.Now;
            gioiTinh = false;
            email = string.Empty;
            soDienThoai = string.Empty;
            nganhHoc = string.Empty;
            diemTrungBinh = 0.0f;
            trangThaiHocTap = "Đang học";
        }

        // Constructor đầy đủ
        public Student(string masv, string hoTen, DateTime ngaySinh, bool gioiTinh,
                      string email, string soDienThoai, string nganhHoc,
                      float diemTrungBinh, string trangThaiHocTap)
        {
            this.masv = masv;
            this.hoTen = hoTen;
            this.ngaySinh = ngaySinh;
            this.gioiTinh = gioiTinh;
            this.email = email;
            this.soDienThoai = soDienThoai;
            this.nganhHoc = nganhHoc;
            this.diemTrungBinh = diemTrungBinh;

            if (trangThaiHocTap == "Đang học" || trangThaiHocTap == "Nghỉ học")
                this.trangThaiHocTap = trangThaiHocTap;
            else
                this.trangThaiHocTap = "Đang học";
        }

        // Constructor rút gọn
        public Student(string masv, string hoTen, DateTime ngaySinh, bool gioiTinh,
                      string email, string soDienThoai, string nganhHoc, float diemTrungBinh)
        {
            this.masv = masv;
            this.hoTen = hoTen;
            this.ngaySinh = ngaySinh;
            this.gioiTinh = gioiTinh;
            this.email = email;
            this.soDienThoai = soDienThoai;
            this.nganhHoc = nganhHoc;
            this.diemTrungBinh = diemTrungBinh;
            this.trangThaiHocTap = "Đang học";
        }

        // Phương thức lấy tên giới tính
        public string GetGioiTinhText()
        {
            return gioiTinh ? "Nam" : "Nữ";
        }

        public override string ToString()
        {
            return $"MSSV: {masv,-10} | Họ tên: {hoTen,-25} | " +
                   $"Ngày sinh: {ngaySinh:dd/MM/yyyy,-12} | Giới tính: {GetGioiTinhText(),-5} | " +
                   $"Email: {email,-25} | SĐT: {soDienThoai,-12} | " +
                   $"Ngành: {nganhHoc,-15} | ĐTB: {diemTrungBinh,-5:F2} | " +
                   $"Trạng thái: {trangThaiHocTap}";
        }
    }
}