using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;



namespace TestFunctionSQL
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // ตรวจสอบว่ามีไฟล์ .db ในโฟลเดอร์หรือไม่
            if (IsDatabaseFileExist())
            {
                // ถ้ามีไฟล์ .db ในโฟลเดอร์ แสดงว่า User เคยสร้าง CreateUser แล้ว
                // เปิดหน้า Login
                Application.Run(new Login());
                //Application.Run(new MainPage());
            }
            else
            {
                // ถ้าไม่มีไฟล์ .db ในโฟลเดอร์ แสดงว่า User ยังไม่ได้สร้าง CreateUser
                // เปิดหน้า Create User
                Application.Run(new CreateMasterPassWord());

                // เมื่อ User สร้าง CreateUser เสร็จสิ้น สามารถทำการสร้างไฟล์ .db
                // แล้วเปิดหน้า Main


            }
        }
        static bool IsDatabaseFileExist()
        {
            // กำหนดที่อยู่ของโฟลเดอร์ที่ต้องการตรวจสอบ
            //test
            //string folderPath = @"C:\Users\Admin\source\repos\TestFunctionSQL\TestFunctionSQL\bin\Debug";
            //=====================================
            // รับพาธไปยังโฟลเดอร์ "Documents" ของผู้ใช้ปัจจุบัน
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // กำหนดชื่อโฟลเดอร์ที่ต้องการสร้าง
            string myAppFolderPath = Path.Combine(documentsPath, "MyPassDocument\\GenerateKey");


            string folderPath1 = @"D:\Storage\GenerateKey";
            string folderPath = @"D:\Storage\DecryptionFileForUse";
            // ตรวจสอบว่าโฟลเดอร์มีอยู่หรือไม่
            if (!Directory.Exists(myAppFolderPath))
            {
                // ถ้าโฟลเดอร์ไม่มีอยู่ ส่งค่า false
                return false;
            }

            // ตรวจสอบไฟล์ .db ในโฟลเดอร์
            string[] dbFiles = Directory.GetFiles(myAppFolderPath, "*.db");

            // ถ้ามีไฟล์ .db ในโฟลเดอร์ แสดงว่า User เคยสร้าง CreateUser แล้ว
            return dbFiles.Length > 0;
        }


    }
}
