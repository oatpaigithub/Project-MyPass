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
    public class DbGenerateKeyEncrypt : DbContext
    {
        public DbSet<GenerateKey> GenerateKey { get; set; }

        public string DatabasePath
        {
            get
            {
                // สร้างโฟลเดอร์หากยังไม่มี
                var folderPath = $"D:\\Storage\\GenerateKey";
                Directory.CreateDirectory(folderPath);

                // ส่งคืนเส้นทางเต็มรูปแบบที่รวมถึงโฟลเดอร์
                return Path.Combine(folderPath, $"GenerateKey.db");
            }
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite($"Data Source={DatabasePath}");

        }



    }
}
