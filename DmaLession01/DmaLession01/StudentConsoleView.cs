using System;
using System.Collections.Generic;

namespace DmaLession01
{
    internal static class StudentConsoleView
    {
        public static void DisplayStudents(List<Student> students, string title = "Danh sách sinh viên")
        {
            Console.Clear();
            Console.WriteLine($"=== {title} ===");
            Console.WriteLine();

            if (students == null || students.Count == 0)
            {
                Console.WriteLine("Không có sinh viên nào trong danh sách.");
                Console.WriteLine();
                return;
            }

            Console.WriteLine($"Tổng số sinh viên: {students.Count}");
            Console.WriteLine(new string('-', 160));

            foreach (var student in students)
            {
                Console.WriteLine(student.ToString());
            }

            Console.WriteLine(new string('-', 160));
            Console.WriteLine();
        }

        public static void DisplayStudent(Student? student)
        {
            if (student == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên.");
                return;
            }

            Console.WriteLine("=== Thông tin sinh viên ===");
            Console.WriteLine($"MSSV: {student.masv}");
            Console.WriteLine($"Họ tên: {student.hoTen}");
            Console.WriteLine($"Ngày sinh: {student.ngaySinh:dd/MM/yyyy}");
            Console.WriteLine($"Giới tính: {student.GetGioiTinhText()}");
            Console.WriteLine($"Email: {student.email}");
            Console.WriteLine($"SĐT: {student.soDienThoai}");
            Console.WriteLine($"Ngành: {student.nganhHoc}");
            Console.WriteLine($"ĐTB: {student.diemTrungBinh:F2}");
            Console.WriteLine($"Trạng thái: {student.trangThaiHocTap}");
            Console.WriteLine();
        }

        public static void DisplayStatistics(Dictionary<string, int> statistics, string title)
        {
            Console.WriteLine($"=== {title} ===");
            Console.WriteLine();

            if (statistics == null || statistics.Count == 0)
            {
                Console.WriteLine("Không có dữ liệu thống kê.");
                Console.WriteLine();
                return;
            }

            foreach (var item in statistics)
            {
                Console.WriteLine($"{item.Key}: {item.Value} sinh viên");
            }

            Console.WriteLine();
        }

        public static void DisplayMessage(string message, bool isSuccess = true)
        {
            if (isSuccess)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✓ {message}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"✗ {message}");
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        public static string GetInput(string prompt)
        {
            Console.Write($"{prompt}: ");
            return Console.ReadLine() ?? string.Empty;
        }

        public static DateTime GetDateInput(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} (dd/MM/yyyy): ");
                string input = Console.ReadLine() ?? string.Empty;

                if (DateTime.TryParseExact(input, "dd/MM/yyyy", null,
                    System.Globalization.DateTimeStyles.None, out DateTime date))
                {
                    return date;
                }

                Console.WriteLine("Định dạng ngày không hợp lệ. Vui lòng nhập lại (dd/MM/yyyy).");
            }
        }

        public static float GetFloatInput(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                string input = Console.ReadLine() ?? string.Empty;

                if (float.TryParse(input, out float value))
                {
                    return value;
                }

                Console.WriteLine("Giá trị không hợp lệ. Vui lòng nhập lại.");
            }
        }

        public static bool GetGenderInput(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} (1-Nam / 0-Nữ): ");
                string input = Console.ReadLine() ?? string.Empty;

                if (input == "1")
                    return true;
                else if (input == "0")
                    return false;
                else
                    Console.WriteLine("Vui lòng nhập 1 (Nam) hoặc 0 (Nữ).");
            }
        }

        public static string GetStatusInput(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} (1-Đang học / 0-Nghỉ học): ");
                string input = Console.ReadLine() ?? string.Empty;

                if (input == "1") return "Đang học";
                if (input == "0") return "Nghỉ học";

                Console.WriteLine("Vui lòng nhập 1 (Đang học) hoặc 0 (Nghỉ học).");
            }
        }

        public static Student GetStudentInput()
        {
            Console.WriteLine("=== NHẬP THÔNG TIN SINH VIÊN ===");

            string masv = GetInput("Mã sinh viên");
            string hoTen = GetInput("Họ tên");
            DateTime ngaySinh = GetDateInput("Ngày sinh");
            bool gioiTinh = GetGenderInput("Giới tính");
            string email = GetInput("Email");
            string soDienThoai = GetInput("Số điện thoại");
            string nganhHoc = GetInput("Ngành học");
            float diemTrungBinh = GetFloatInput("Điểm trung bình");
            string trangThai = GetStatusInput("Trạng thái học tập");

            return new Student(masv, hoTen, ngaySinh, gioiTinh, email,
                              soDienThoai, nganhHoc, diemTrungBinh, trangThai);
        }

        public static void WaitForKeyPress()
        {
            Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
    }
}