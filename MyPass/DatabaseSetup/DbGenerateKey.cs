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
    public class DbGenerateKey : DbContext
    {
        public DbSet<GenerateKey> GenerateKey { get; set; }
        private string myAppFolderPath;
        public string myAppMainFolderPath;

        public DbGenerateKey()
        {
            // รับพาธไปยังโฟลเดอร์ "Documents" ของผู้ใช้ปัจจุบัน
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // กำหนดชื่อโฟลเดอร์ที่ต้องการสร้าง
            myAppMainFolderPath = Path.Combine(documentsPath, "MyPassDocument");
            myAppFolderPath = Path.Combine(documentsPath, "MyPassDocument\\GenerateKey");

        }


     


        public string DatabasePath
        {
            get
            {
                // สร้างโฟลเดอร์หากยังไม่มี
                //var folderPath = $"D:\\Storage\\DecryptionFileForUse";
                //Directory.CreateDirectory(folderPath);

                Directory.CreateDirectory(myAppFolderPath);
                // ส่งคืนเส้นทางเต็มรูปแบบที่รวมถึงโฟลเดอร์
                return Path.Combine(myAppFolderPath, $"GenerateKey.db");
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite($"Data Source={DatabasePath}");

        }



    }


}
