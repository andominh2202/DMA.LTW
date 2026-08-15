using System;

namespace DmaLession01
{
    internal class Student
    {
        // Thuộc tính
        public string maSV { get; set; }
        public string hoTen { get; set; }
        public DateTime ngaySinh { get; set; }
        public bool gioiTinh { get; set; }  // true = Nam, false = Nữ
        public string email { get; set; }
        public string soDienThoai { get; set; }
        public string nganhHoc { get; set; }
        public float diemTrungBinh { get; set; }
        public string trangThaiHocTap { get; set; }  // "Đang học" hoặc "Nghỉ học"
        public Student()
        {
            maSV = string.Empty;
            hoTen = string.Empty;
            ngaySinh = DateTime.Now;
            gioiTinh = false;
            email = string.Empty;
            soDienThoai = string.Empty;
            nganhHoc = string.Empty;
            diemTrungBinh = 0.0f;
            trangThaiHocTap = "Đang học";
        }

        public Student(string maSV, string hoTen, DateTime ngaySinh, bool gioiTinh,
                      string email, string soDienThoai, string nganhHoc,
                      float diemTrungBinh, string trangThaiHocTap)
        {
            this.maSV = maSV;
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

        public string layTenGioiTinh()
        {
            return gioiTinh ? "Nam" : "Nữ";
        }

        public void chuyenTrangThai(string trangThaiMoi)
        {
            if (trangThaiMoi == "Đang học" || trangThaiMoi == "Nghỉ học")
            {
                trangThaiHocTap = trangThaiMoi;
            }
        }

        // Cho nghỉ học
        public void nghiHoc()
        {
            trangThaiHocTap = "Nghỉ học";
        }

        // Cho đi học lại (camelCase)
        public void diHocLai()
        {
            trangThaiHocTap = "Đang học";
        }

        // Kiểm tra đang học (camelCase)
        public bool isDangHoc()
        {
            return trangThaiHocTap == "Đang học";
        }

        // Kiểm tra nghỉ học
        public bool isNghiHoc()
        {
            return trangThaiHocTap == "Nghỉ học";
        }

        public override string ToString()
        {
            return $"MSSV: {maSV,-10} | Họ tên: {hoTen,-15} | " +
                   $"Ngày sinh: {ngaySinh:dd/MM/yyyy} | Giới tính: {layTenGioiTinh(),-5} | " +
                   $"Email: {email,-25} | SĐT: {soDienThoai,-12} | " +
                   $"Ngành: {nganhHoc,-15} | ĐTB: {diemTrungBinh,-5:F2} | " +
                   $"Trạng thái: {trangThaiHocTap}";
        }
    }
}