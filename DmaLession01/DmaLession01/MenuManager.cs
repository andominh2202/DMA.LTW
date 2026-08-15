using System;
using System.Collections.Generic;

namespace DmaLession01
{
    internal class MenuManager
    {
        private readonly StudentService _studentService;

        public MenuManager()
        {
            _studentService = new StudentService();
            taoDuLieuMau();
        }

        private void taoDuLieuMau()
        {
            var students = new List<Student>
            {
                new Student("SV001", "Đỗ Minh An", new DateTime(2006, 12, 06), true,
                           "dominhan0612@email.com", "0333455447", "Công nghệ thông tin", 8.5f, "Đang học"),
                new Student("SV002", "Trần Thị Bình", new DateTime(2004, 7, 20), false,
                           "binh.tran@email.com", "0987654321", "Kinh tế", 7.2f, "Đang học"),
                new Student("SV003", "Lê Văn Cường", new DateTime(2005, 11, 2), true,
                           "cuong.le@email.com", "0934567890", "Công nghệ thông tin", 9.0f, "Đang học"),
                new Student("SV004", "Phạm Thị Dung", new DateTime(2004, 9, 10), false,
                           "dung.pham@email.com", "0976543210", "Kế toán", 6.8f, "Nghỉ học"),
                new Student("SV005", "Hoàng Văn Phúc", new DateTime(2005, 5, 25), true,
                           "phuc.hoang@email.com", "0923456789", "Kỹ thuật xây dựng", 5.5f, "Nghỉ học")
            };

            foreach (var student in students)
            {
                _studentService.themSinhVien(student, out _);
            }
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== HỆ THỐNG QUẢN LÝ SINH VIÊN ===");
                Console.WriteLine("1. Thêm sinh viên");
                Console.WriteLine("2. Hiển thị danh sách sinh viên");
                Console.WriteLine("3. Tìm sinh viên theo mã");
                Console.WriteLine("4. Tìm gần đúng theo họ tên");
                Console.WriteLine("5. Cập nhật sinh viên");
                Console.WriteLine("6. Xóa sinh viên");
                Console.WriteLine("7. Sắp xếp theo họ tên");
                Console.WriteLine("8. Sắp xếp theo điểm trung bình");
                Console.WriteLine("9. Hiển thị sinh viên có điểm từ 8 trở lên");
                Console.WriteLine("10. Hiển thị sinh viên có điểm cao nhất");
                Console.WriteLine("11. Tính điểm trung bình toàn bộ sinh viên");
                Console.WriteLine("12. Thống kê sinh viên theo ngành");
                Console.WriteLine("13. Thống kê sinh viên theo trạng thái");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn chức năng: ");

                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1": themSinhVien(); break;
                    case "2": hienThiDanhSach(); break;
                    case "3": timSinhVienTheoMa(); break;
                    case "4": timGanDungTheoHoTen(); break;
                    case "5": capNhatSinhVien(); break;
                    case "6": xoaSinhVien(); break;
                    case "7": sapXepTheoHoTen(); break;
                    case "8": sapXepTheoDiemTrungBinh(); break;
                    case "9": hienThiSinhVienCoDiemCao(); break;
                    case "10": hienThiSinhVienCaoNhat(); break;
                    case "11": tinhDiemTrungBinh(); break;
                    case "12": thongKeSinhVienTheoNganh(); break;
                    case "13": thongKeSinhVienTheoTrangThai(); break;
                    case "14":
                        Console.Clear();
                        Console.WriteLine("Cảm ơn bạn đã sử dụng chương trình!");
                        return;
                    default:
                        StudentConsoleView.hienThiThongBao("Lựa chọn không hợp lệ!", false);
                        StudentConsoleView.choPhimBatKy();
                        break;
                }
            }
        }

        // 1. Thêm sinh viên
        private void themSinhVien()
        {
            Console.Clear();
            var student = StudentConsoleView.nhapSinhVien();

            if (_studentService.themSinhVien(student, out string errorMessage))
            {
                StudentConsoleView.hienThiThongBao("Thêm sinh viên thành công!");
            }
            else
            {
                StudentConsoleView.hienThiThongBao(errorMessage, false);
            }

            StudentConsoleView.choPhimBatKy();
        }

        // 2. Hiển thị danh sách
        private void hienThiDanhSach()
        {
            var students = _studentService.layTatCaSinhVien();
            StudentConsoleView.hienThiDanhSach(students);
            StudentConsoleView.choPhimBatKy();
        }

        // 3. Tìm sinh viên theo mã
        private void timSinhVienTheoMa()
        {
            Console.Clear();
            string studentId = StudentConsoleView.nhapChuoi("Nhập mã sinh viên cần tìm");
            var student = _studentService.timSinhVienTheoMa(studentId);

            if (student != null)
            {
                StudentConsoleView.hienThiSinhVien(student);
            }
            else
            {
                StudentConsoleView.hienThiThongBao($"Không tìm thấy sinh viên với mã {studentId}", false);
            }

            StudentConsoleView.choPhimBatKy();
        }

        // 4. Tìm gần đúng theo họ tên
        private void timGanDungTheoHoTen()
        {
            Console.Clear();
            string searchName = StudentConsoleView.nhapChuoi("Nhập tên cần tìm");
            var students = _studentService.timSinhVienTheoTen(searchName);

            if (students.Count > 0)
            {
                StudentConsoleView.hienThiDanhSach(students, $"Kết quả tìm kiếm: {searchName}");
            }
            else
            {
                StudentConsoleView.hienThiThongBao($"Không tìm thấy sinh viên nào với tên '{searchName}'", false);
            }

            StudentConsoleView.choPhimBatKy();
        }

        // 5. Cập nhật sinh viên
        private void capNhatSinhVien()
        {
            Console.Clear();
            string studentId = StudentConsoleView.nhapChuoi("Nhập mã sinh viên cần cập nhật");
            var existingStudent = _studentService.timSinhVienTheoMa(studentId);

            if (existingStudent == null)
            {
                StudentConsoleView.hienThiThongBao($"Không tìm thấy sinh viên với mã {studentId}", false);
                StudentConsoleView.choPhimBatKy();
                return;
            }

            Console.WriteLine("Thông tin hiện tại:");
            StudentConsoleView.hienThiSinhVien(existingStudent);

            Console.WriteLine("Nhập thông tin mới (bỏ trống để giữ nguyên):");

            string hoTen = StudentConsoleView.nhapChuoi($"Họ tên ({existingStudent.hoTen})");
            if (!string.IsNullOrWhiteSpace(hoTen)) existingStudent.hoTen = hoTen;

            string ngaySinhStr = StudentConsoleView.nhapChuoi($"Ngày sinh ({existingStudent.ngaySinh:dd/MM/yyyy})");
            if (!string.IsNullOrWhiteSpace(ngaySinhStr) &&
                DateTime.TryParseExact(ngaySinhStr, "dd/MM/yyyy", null,
                System.Globalization.DateTimeStyles.None, out DateTime newDate))
            {
                existingStudent.ngaySinh = newDate;
            }

            string gioiTinhStr = StudentConsoleView.nhapChuoi($"Giới tính (1-Nam / 0-Nữ) ({existingStudent.layTenGioiTinh()})");
            if (!string.IsNullOrWhiteSpace(gioiTinhStr))
            {
                if (gioiTinhStr == "1")
                    existingStudent.gioiTinh = true;
                else if (gioiTinhStr == "0")
                    existingStudent.gioiTinh = false;
            }

            string email = StudentConsoleView.nhapChuoi($"Email ({existingStudent.email})");
            if (!string.IsNullOrWhiteSpace(email)) existingStudent.email = email;

            string soDienThoai = StudentConsoleView.nhapChuoi($"SĐT ({existingStudent.soDienThoai})");
            if (!string.IsNullOrWhiteSpace(soDienThoai)) existingStudent.soDienThoai = soDienThoai;

            string nganhHoc = StudentConsoleView.nhapChuoi($"Ngành ({existingStudent.nganhHoc})");
            if (!string.IsNullOrWhiteSpace(nganhHoc)) existingStudent.nganhHoc = nganhHoc;

            string diemStr = StudentConsoleView.nhapChuoi($"ĐTB ({existingStudent.diemTrungBinh:F2})");
            if (!string.IsNullOrWhiteSpace(diemStr) && float.TryParse(diemStr, out float newDiem))
            {
                existingStudent.diemTrungBinh = newDiem;
            }

            string trangThaiStr = StudentConsoleView.nhapChuoi($"Trạng thái (1-Đang học / 0-Nghỉ học) ({existingStudent.trangThaiHocTap})");
            if (!string.IsNullOrWhiteSpace(trangThaiStr))
            {
                if (trangThaiStr == "1")
                    existingStudent.trangThaiHocTap = "Đang học";
                else if (trangThaiStr == "0")
                    existingStudent.trangThaiHocTap = "Nghỉ học";
            }

            StudentConsoleView.hienThiThongBao("Cập nhật sinh viên thành công!");
            StudentConsoleView.choPhimBatKy();
        }

        // 6. Xóa sinh viên
        private void xoaSinhVien()
        {
            Console.Clear();
            string studentId = StudentConsoleView.nhapChuoi("Nhập mã sinh viên cần xóa");

            if (_studentService.xoaSinhVien(studentId, out string errorMessage))
            {
                StudentConsoleView.hienThiThongBao($"Xóa sinh viên {studentId} thành công!");
            }
            else
            {
                StudentConsoleView.hienThiThongBao(errorMessage, false);
            }

            StudentConsoleView.choPhimBatKy();
        }

        // 7. Sắp xếp theo họ tên
        private void sapXepTheoHoTen()
        {
            var sortedStudents = _studentService.sapXepTheoTen();
            StudentConsoleView.hienThiDanhSach(sortedStudents, "Danh sách sinh viên sắp xếp theo họ tên");
            StudentConsoleView.choPhimBatKy();
        }

        // 8. Sắp xếp theo điểm trung bình
        private void sapXepTheoDiemTrungBinh()
        {
            var sortedStudents = _studentService.sapXepTheoDiem();
            StudentConsoleView.hienThiDanhSach(sortedStudents, "Danh sách sinh viên sắp xếp theo điểm trung bình (giảm dần)");
            StudentConsoleView.choPhimBatKy();
        }

        // 9. Hiển thị sinh viên có điểm từ 8 trở lên
        private void hienThiSinhVienCoDiemCao()
        {
            var excellentStudents = _studentService.laySinhVienGioi();
            StudentConsoleView.hienThiDanhSach(excellentStudents, "Danh sách sinh viên có điểm từ 8.0 trở lên");
            StudentConsoleView.choPhimBatKy();
        }

        // 10. Hiển thị sinh viên có điểm cao nhất
        private void hienThiSinhVienCaoNhat()
        {
            var highestStudent = _studentService.laySinhVienCaoNhat();

            if (highestStudent != null)
            {
                Console.Clear();
                Console.WriteLine("=== SINH VIÊN CÓ ĐIỂM TRUNG BÌNH CAO NHẤT ===");
                Console.WriteLine();
                StudentConsoleView.hienThiSinhVien(highestStudent);
            }
            else
            {
                StudentConsoleView.hienThiThongBao("Không có sinh viên nào trong danh sách!", false);
            }

            StudentConsoleView.choPhimBatKy();
        }

        // 11. Tính điểm trung bình toàn bộ sinh viên
        private void tinhDiemTrungBinh()
        {
            var averageGpa = _studentService.tinhDiemTrungBinh();

            Console.Clear();
            Console.WriteLine("=== ĐIỂM TRUNG BÌNH TOÀN BỘ SINH VIÊN ===");
            Console.WriteLine();
            Console.WriteLine($"Điểm trung bình của tất cả sinh viên: {averageGpa:F2}");
            Console.WriteLine();
            StudentConsoleView.choPhimBatKy();
        }

        // 12. Thống kê sinh viên theo ngành
        private void thongKeSinhVienTheoNganh()
        {
            var statistics = _studentService.thongKeTheoNganh();
            StudentConsoleView.hienThiThongKe(statistics, "THỐNG KÊ SINH VIÊN THEO NGÀNH");
            StudentConsoleView.choPhimBatKy();
        }

        // 13. Thống kê sinh viên theo trạng thái
        private void thongKeSinhVienTheoTrangThai()
        {
            var statistics = _studentService.thongKeTheoTrangThai();
            StudentConsoleView.hienThiThongKe(statistics, "THỐNG KÊ SINH VIÊN THEO TRẠNG THÁI");
            StudentConsoleView.choPhimBatKy();
        }
    }
}