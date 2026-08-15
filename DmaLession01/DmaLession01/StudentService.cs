using System;
using System.Collections.Generic;
using System.Linq;

namespace DmaLession01
{
    internal class StudentService
    {
        private List<Student> _students;

        public StudentService()
        {
            _students = new List<Student>();
        }

        // 1. Thêm sinh viên
        public bool themSinhVien(Student student, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (_students.Any(s => s.maSV == student.maSV))
            {
                errorMessage = $"Sinh viên với mã {student.maSV} đã tồn tại";
                return false;
            }

            _students.Add(student);
            return true;
        }

        // 2. Lấy tất cả sinh viên
        public List<Student> layTatCaSinhVien()
        {
            return _students.ToList();
        }

        // 3. Tìm sinh viên theo mã
        public Student? timSinhVienTheoMa(string studentId)
        {
            return _students.FirstOrDefault(s => s.maSV == studentId);
        }

        // 4. Tìm gần đúng theo họ tên
        public List<Student> timSinhVienTheoTen(string searchName)
        {
            if (string.IsNullOrWhiteSpace(searchName))
                return new List<Student>();

            return _students.Where(s => s.hoTen.Contains(searchName, StringComparison.OrdinalIgnoreCase))
                           .ToList();
        }

        // 5. Cập nhật sinh viên
        public bool capNhatSinhVien(string studentId, Student updatedStudent, out string errorMessage)
        {
            errorMessage = string.Empty;

            var existingStudent = timSinhVienTheoMa(studentId);
            if (existingStudent == null)
            {
                errorMessage = $"Không tìm thấy sinh viên với mã {studentId}";
                return false;
            }

            existingStudent.hoTen = updatedStudent.hoTen;
            existingStudent.ngaySinh = updatedStudent.ngaySinh;
            existingStudent.gioiTinh = updatedStudent.gioiTinh;
            existingStudent.email = updatedStudent.email;
            existingStudent.soDienThoai = updatedStudent.soDienThoai;
            existingStudent.nganhHoc = updatedStudent.nganhHoc;
            existingStudent.diemTrungBinh = updatedStudent.diemTrungBinh;
            existingStudent.trangThaiHocTap = updatedStudent.trangThaiHocTap;

            return true;
        }

        // 6. Xóa sinh viên
        public bool xoaSinhVien(string studentId, out string errorMessage)
        {
            errorMessage = string.Empty;

            var student = timSinhVienTheoMa(studentId);
            if (student == null)
            {
                errorMessage = $"Không tìm thấy sinh viên với mã {studentId}";
                return false;
            }

            _students.Remove(student);
            return true;
        }

        // 7. Sắp xếp theo họ tên
        public List<Student> sapXepTheoTen()
        {
            return _students.OrderBy(s => s.hoTen).ToList();
        }

        // 8. Sắp xếp theo điểm trung bình (giảm dần)
        public List<Student> sapXepTheoDiem()
        {
            return _students.OrderByDescending(s => s.diemTrungBinh).ToList();
        }

        // 9. Lấy sinh viên có điểm >= 8
        public List<Student> laySinhVienGioi()
        {
            return _students.Where(s => s.diemTrungBinh >= 8.0f).ToList();
        }

        // 10. Lấy sinh viên có điểm cao nhất
        public Student? laySinhVienCaoNhat()
        {
            if (_students.Count == 0) return null;
            return _students.OrderByDescending(s => s.diemTrungBinh).First();
        }

        // 11. Tính điểm trung bình toàn bộ sinh viên
        public float tinhDiemTrungBinh()
        {
            if (_students.Count == 0) return 0;
            return _students.Average(s => s.diemTrungBinh);
        }

        // 12. Thống kê theo ngành
        public Dictionary<string, int> thongKeTheoNganh()
        {
            return _students.GroupBy(s => s.nganhHoc)
                           .ToDictionary(g => g.Key, g => g.Count());
        }

        // 13. Thống kê theo trạng thái
        public Dictionary<string, int> thongKeTheoTrangThai()
        {
            return _students.GroupBy(s => s.trangThaiHocTap)
                           .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}