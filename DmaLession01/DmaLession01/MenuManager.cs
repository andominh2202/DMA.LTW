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
            SeedSampleData();
        }

        private void SeedSampleData()
        {
            var students = new List<Student>
            {
                new Student("SV001", "Nguyễn Văn An", new DateTime(2005, 3, 15), true,
                           "an.nguyen@email.com", "0912345678", "Công nghệ thông tin", 8.5f, "Đang học"),
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
                _studentService.AddStudent(student, out _);
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
                Console.WriteLine("14. Thoát");
                Console.Write("Chọn chức năng: ");

                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1": AddStudent(); break;
                    case "2": DisplayAllStudents(); break;
                    case "3": FindStudentById(); break;
                    case "4": FindStudentsByName(); break;
                    case "5": UpdateStudent(); break;
                    case "6": DeleteStudent(); break;
                    case "7": SortByName(); break;
                    case "8": SortByGpa(); break;
                    case "9": DisplayExcellentStudents(); break;
                    case "10": DisplayHighestGpaStudent(); break;
                    case "11": CalculateAverageGpa(); break;
                    case "12": StatisticsByMajor(); break;
                    case "13": StatisticsByStatus(); break;
                    case "0":
                        Console.Clear();
                        Console.WriteLine("Cảm ơn bạn đã sử dụng chương trình!");
                        return;
                    default:
                        StudentConsoleView.DisplayMessage("Lựa chọn không hợp lệ!", false);
                        StudentConsoleView.WaitForKeyPress();
                        break;
                }
            }
        }

        // ============== PHƯƠNG THỨC XỬ LÝ 13 CHỨC NĂNG ==============

        // 1. Thêm sinh viên
        private void AddStudent()
        {
            Console.Clear();
            var student = StudentConsoleView.GetStudentInput();

            if (_studentService.AddStudent(student, out string errorMessage))
            {
                StudentConsoleView.DisplayMessage("Thêm sinh viên thành công!");
            }
            else
            {
                StudentConsoleView.DisplayMessage(errorMessage, false);
            }

            StudentConsoleView.WaitForKeyPress();
        }

        // 2. Hiển thị danh sách
        private void DisplayAllStudents()
        {
            var students = _studentService.GetAllStudents();
            StudentConsoleView.DisplayStudents(students);
            StudentConsoleView.WaitForKeyPress();
        }

        // 3. Tìm sinh viên theo mã
        private void FindStudentById()
        {
            Console.Clear();
            string studentId = StudentConsoleView.GetInput("Nhập mã sinh viên cần tìm");
            var student = _studentService.FindStudentById(studentId);

            if (student != null)
            {
                StudentConsoleView.DisplayStudent(student);
            }
            else
            {
                StudentConsoleView.DisplayMessage($"Không tìm thấy sinh viên với mã {studentId}", false);
            }

            StudentConsoleView.WaitForKeyPress();
        }

        // 4. Tìm gần đúng theo họ tên
        private void FindStudentsByName()
        {
            Console.Clear();
            string searchName = StudentConsoleView.GetInput("Nhập tên cần tìm");
            var students = _studentService.FindStudentsByName(searchName);

            if (students.Count > 0)
            {
                StudentConsoleView.DisplayStudents(students, $"Kết quả tìm kiếm: {searchName}");
            }
            else
            {
                StudentConsoleView.DisplayMessage($"Không tìm thấy sinh viên nào với tên '{searchName}'", false);
            }

            StudentConsoleView.WaitForKeyPress();
        }

        // 5. Cập nhật sinh viên
        private void UpdateStudent()
        {
            Console.Clear();
            string studentId = StudentConsoleView.GetInput("Nhập mã sinh viên cần cập nhật");
            var existingStudent = _studentService.FindStudentById(studentId);

            if (existingStudent == null)
            {
                StudentConsoleView.DisplayMessage($"Không tìm thấy sinh viên với mã {studentId}", false);
                StudentConsoleView.WaitForKeyPress();
                return;
            }

            Console.WriteLine("Thông tin hiện tại:");
            StudentConsoleView.DisplayStudent(existingStudent);

            Console.WriteLine("Nhập thông tin mới (bỏ trống để giữ nguyên):");

            string hoTen = StudentConsoleView.GetInput($"Họ tên ({existingStudent.hoTen})");
            if (!string.IsNullOrWhiteSpace(hoTen)) existingStudent.hoTen = hoTen;

            string ngaySinhStr = StudentConsoleView.GetInput($"Ngày sinh ({existingStudent.ngaySinh:dd/MM/yyyy})");
            if (!string.IsNullOrWhiteSpace(ngaySinhStr) &&
                DateTime.TryParseExact(ngaySinhStr, "dd/MM/yyyy", null,
                System.Globalization.DateTimeStyles.None, out DateTime newDate))
            {
                existingStudent.ngaySinh = newDate;
            }

            string gioiTinhStr = StudentConsoleView.GetInput($"Giới tính (1-Nam / 0-Nữ) ({existingStudent.GetGioiTinhText()})");
            if (!string.IsNullOrWhiteSpace(gioiTinhStr))
            {
                if (gioiTinhStr == "1")
                    existingStudent.gioiTinh = true;
                else if (gioiTinhStr == "0")
                    existingStudent.gioiTinh = false;
            }

            string email = StudentConsoleView.GetInput($"Email ({existingStudent.email})");
            if (!string.IsNullOrWhiteSpace(email)) existingStudent.email = email;

            string soDienThoai = StudentConsoleView.GetInput($"SĐT ({existingStudent.soDienThoai})");
            if (!string.IsNullOrWhiteSpace(soDienThoai)) existingStudent.soDienThoai = soDienThoai;

            string nganhHoc = StudentConsoleView.GetInput($"Ngành ({existingStudent.nganhHoc})");
            if (!string.IsNullOrWhiteSpace(nganhHoc)) existingStudent.nganhHoc = nganhHoc;

            string diemStr = StudentConsoleView.GetInput($"ĐTB ({existingStudent.diemTrungBinh:F2})");
            if (!string.IsNullOrWhiteSpace(diemStr) && float.TryParse(diemStr, out float newDiem))
            {
                existingStudent.diemTrungBinh = newDiem;
            }

            string trangThaiStr = StudentConsoleView.GetInput($"Trạng thái (1-Đang học / 0-Nghỉ học) ({existingStudent.trangThaiHocTap})");
            if (!string.IsNullOrWhiteSpace(trangThaiStr))
            {
                if (trangThaiStr == "1")
                    existingStudent.trangThaiHocTap = "Đang học";
                else if (trangThaiStr == "0")
                    existingStudent.trangThaiHocTap = "Nghỉ học";
            }

            StudentConsoleView.DisplayMessage("Cập nhật sinh viên thành công!");
            StudentConsoleView.WaitForKeyPress();
        }

        // 6. Xóa sinh viên
        private void DeleteStudent()
        {
            Console.Clear();
            string studentId = StudentConsoleView.GetInput("Nhập mã sinh viên cần xóa");

            if (_studentService.DeleteStudent(studentId, out string errorMessage))
            {
                StudentConsoleView.DisplayMessage($"Xóa sinh viên {studentId} thành công!");
            }
            else
            {
                StudentConsoleView.DisplayMessage(errorMessage, false);
            }

            StudentConsoleView.WaitForKeyPress();
        }

        // 7. Sắp xếp theo họ tên
        private void SortByName()
        {
            var sortedStudents = _studentService.SortByName();
            StudentConsoleView.DisplayStudents(sortedStudents, "Danh sách sinh viên sắp xếp theo họ tên");
            StudentConsoleView.WaitForKeyPress();
        }

        // 8. Sắp xếp theo điểm trung bình
        private void SortByGpa()
        {
            var sortedStudents = _studentService.SortByGpa();
            StudentConsoleView.DisplayStudents(sortedStudents, "Danh sách sinh viên sắp xếp theo điểm trung bình (giảm dần)");
            StudentConsoleView.WaitForKeyPress();
        }

        // 9. Hiển thị sinh viên có điểm từ 8 trở lên
        private void DisplayExcellentStudents()
        {
            var excellentStudents = _studentService.GetExcellentStudents();
            StudentConsoleView.DisplayStudents(excellentStudents, "Danh sách sinh viên có điểm từ 8.0 trở lên");
            StudentConsoleView.WaitForKeyPress();
        }

        // 10. Hiển thị sinh viên có điểm cao nhất
        private void DisplayHighestGpaStudent()
        {
            var highestStudent = _studentService.GetHighestGpaStudent();

            if (highestStudent != null)
            {
                Console.Clear();
                Console.WriteLine("=== SINH VIÊN CÓ ĐIỂM TRUNG BÌNH CAO NHẤT ===");
                Console.WriteLine();
                StudentConsoleView.DisplayStudent(highestStudent);
            }
            else
            {
                StudentConsoleView.DisplayMessage("Không có sinh viên nào trong danh sách!", false);
            }

            StudentConsoleView.WaitForKeyPress();
        }

        // 11. Tính điểm trung bình toàn bộ sinh viên
        private void CalculateAverageGpa()
        {
            var averageGpa = _studentService.CalculateAverageGpa();

            Console.Clear();
            Console.WriteLine("=== ĐIỂM TRUNG BÌNH TOÀN BỘ SINH VIÊN ===");
            Console.WriteLine();
            Console.WriteLine($"Điểm trung bình của tất cả sinh viên: {averageGpa:F2}");
            Console.WriteLine();
            StudentConsoleView.WaitForKeyPress();
        }

        // 12. Thống kê sinh viên theo ngành
        private void StatisticsByMajor()
        {
            var statistics = _studentService.GetStatisticsByMajor();
            StudentConsoleView.DisplayStatistics(statistics, "THỐNG KÊ SINH VIÊN THEO NGÀNH");
            StudentConsoleView.WaitForKeyPress();
        }

        // 13. Thống kê sinh viên theo trạng thái
        private void StatisticsByStatus()
        {
            var statistics = _studentService.GetStatisticsByStatus();
            StudentConsoleView.DisplayStatistics(statistics, "THỐNG KÊ SINH VIÊN THEO TRẠNG THÁI");
            StudentConsoleView.WaitForKeyPress();
        }
    }
}