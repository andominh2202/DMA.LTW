using System;
using System.Collections.Generic;

namespace DmaLession01
{
    internal static class StudentConsoleView
    {
        // === PHƯƠNG THỨC HIỂN THỊ ===

        // Hiển thị danh sách sinh viên (List)
        public static void hienThiDanhSach(List<Student> students, string title = "Danh sách sinh viên")
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

        // Hiển thị thông tin 1 sinh viên
        public static void hienThiSinhVien(Student? student)
        {
            if (student == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên.");
                return;
            }

            Console.WriteLine("=== Thông tin sinh viên ===");
            Console.WriteLine($"MSSV: {student.maSV}");
            Console.WriteLine($"Họ tên: {student.hoTen}");
            Console.WriteLine($"Ngày sinh: {student.ngaySinh:dd/MM/yyyy}");
            Console.WriteLine($"Giới tính: {student.layTenGioiTinh()}");
            Console.WriteLine($"Email: {student.email}");
            Console.WriteLine($"SĐT: {student.soDienThoai}");
            Console.WriteLine($"Ngành: {student.nganhHoc}");
            Console.WriteLine($"ĐTB: {student.diemTrungBinh:F2}");
            Console.WriteLine($"Trạng thái: {student.trangThaiHocTap}");
            Console.WriteLine();
        }

        // Hiển thị thống kê
        public static void hienThiThongKe(Dictionary<string, int> statistics, string title)
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

        // Hiển thị thông báo
        public static void hienThiThongBao(string message, bool isSuccess = true)
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

        // === PHƯƠNG THỨC NHẬP LIỆU ===

        public static string nhapChuoi(string prompt)
        {
            Console.Write($"{prompt}: ");
            return Console.ReadLine() ?? string.Empty;
        }

        public static DateTime nhapNgay(string prompt)
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

        public static float nhapSoThuc(string prompt)
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

        public static bool nhapGioiTinh(string prompt)
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

        public static string nhapTrangThai(string prompt)
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

        public static Student nhapSinhVien()
        {
            Console.WriteLine("=== NHẬP THÔNG TIN SINH VIÊN ===");

            string maSV = nhapChuoi("Mã sinh viên");
            string hoTen = nhapChuoi("Họ tên");
            DateTime ngaySinh = nhapNgay("Ngày sinh");
            bool gioiTinh = nhapGioiTinh("Giới tính");
            string email = nhapChuoi("Email");
            string soDienThoai = nhapChuoi("Số điện thoại");
            string nganhHoc = nhapChuoi("Ngành học");
            float diemTrungBinh = nhapSoThuc("Điểm trung bình");
            string trangThai = nhapTrangThai("Trạng thái học tập");

            return new Student(maSV, hoTen, ngaySinh, gioiTinh, email,
                              soDienThoai, nganhHoc, diemTrungBinh, trangThai);
        }

        public static void choPhimBatKy()
        {
            Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
    }
}