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
    public class DbLocalCiphertextContext : DbContext
    {
        public DbSet<CiphertextLocal> Ciphertexts { get; set; }

        private DbGenerateKey DbGenerateKey_this = new DbGenerateKey();
        private string NamePath;

        public void SetNamePath() 
        {
            var existingGeneratekey = DbGenerateKey_this.GenerateKey.Find(1) ;

            if (existingGeneratekey != null)
            {
                // ทำการอัปเดตข้อมูลใน DbContext
                NamePath = existingGeneratekey.HashNameFile;

            }
            else 
            {
                MessageBox.Show($"ไม่พบ GenerateKey ภายในเครื่องของคุณ");
                return;
            }
        }
        public string DatabasePath
        {
            get
            {
                // สร้างโฟลเดอร์หากยังไม่มี
                var folderPath = $"D:\\Storage\\LocalCiphertexts";
                Directory.CreateDirectory(folderPath);

                // ส่งคืนเส้นทางเต็มรูปแบบที่รวมถึงโฟลเดอร์
                return Path.Combine(folderPath, $"LocalCiphertexts{NamePath}.db");
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SetNamePath();
            // ใช้ property หรือ method ที่คืนค่า Path ของฐานข้อมูล
            //optionsBuilder.UseSqlite($"Data Source={DatabasePath}");
            optionsBuilder.UseSqlite($"Data Source={DatabasePath}");

        }
    }
}
