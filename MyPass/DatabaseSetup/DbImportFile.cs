using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestFunctionSQL
{
    public class DbImportFile : DbContext
    {
        public DbSet<ImportFile> ImportFile { get; set; }

        public string UsergetDatabasePath { get; set; }

        private string myAppFolderPath;

        public DbImportFile(string databasePath)
        {
            UsergetDatabasePath = databasePath;
            // รับพาธไปยังโฟลเดอร์ "Documents" ของผู้ใช้ปัจจุบัน
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // กำหนดชื่อโฟลเดอร์ที่ต้องการสร้าง
            myAppFolderPath = Path.Combine(documentsPath, "MyPassDocument\\Import");

        }
        public DbImportFile() 
        {
            // รับพาธไปยังโฟลเดอร์ "Documents" ของผู้ใช้ปัจจุบัน
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // กำหนดชื่อโฟลเดอร์ที่ต้องการสร้าง
            myAppFolderPath = Path.Combine(documentsPath, "MyPassDocument\\Import");


        }
        public string DatabasePath
        {
            get
            {
                // สร้างโฟลเดอร์หากยังไม่มี
                //var folderPath = $"D:\\Storage\\Import";
                Directory.CreateDirectory(myAppFolderPath);


                //// ส่งคืนเส้นทางเต็มรูปแบบที่รวมถึงโฟลเดอร์
                //return Path.Combine(folderPath, $"Import.db");
                // ส่งคืนเส้นทางโฟลเดอร์เท่านั้น
                return myAppFolderPath;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //MessageBox.Show($"UsersetDatabasePath: {UsergetDatabasePath}", "แจ้งแตือน");
            if (UsergetDatabasePath == null)
            {
                optionsBuilder.UseSqlite($"Data Source={DatabasePath}");
            }
            else
            {
                optionsBuilder.UseSqlite($"Data Source={UsergetDatabasePath}");
            }

        }
    }
}
