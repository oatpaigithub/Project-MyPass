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
    public class DbExportFile : DbContext
    {
        public DbSet<ExportFile> ExportFile { get; set; }

        // เพิ่ม Constructor ที่รับ Path เป็นพารามิเตอร์
        public string UsersetDatabasePath { get; set; }
        public DbExportFile(string databasePath)
        {
            UsersetDatabasePath = databasePath;
        }

        // Constructor ที่ไม่มี parameter (optional)
        public DbExportFile()
        {
            // ทำสิ่งที่ต้องการเมื่อไม่มี parameter
        }

        public string DatabasePath
        {
            get
            {
                // สร้างโฟลเดอร์หากยังไม่มี
                var folderPath = $"D:\\Storage\\ExportFile";
                Directory.CreateDirectory(folderPath);

                // ส่งคืนเส้นทางเต็มรูปแบบที่รวมถึงโฟลเดอร์
                return Path.Combine(folderPath, $"ExportFile.db");
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //MessageBox.Show($"UsersetDatabasePath: {UsersetDatabasePath}","แจ้งแตือน");
            if (UsersetDatabasePath == null)
            {
                optionsBuilder.UseSqlite($"Data Source={DatabasePath}");
            }
            else {
                optionsBuilder.UseSqlite($"Data Source={UsersetDatabasePath}");
            }

        }

    }
}
