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

        private string myAppFolderPath;
        public DbLocalCiphertextContext() 
        {
            // รับพาธไปยังโฟลเดอร์ "Documents" ของผู้ใช้ปัจจุบัน
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // กำหนดชื่อโฟลเดอร์ที่ต้องการสร้าง
            myAppFolderPath = Path.Combine(documentsPath, "MyPassDocument\\DecryptionFileForUse");

        }
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
                //var folderPath = $"D:\\Storage\\DecryptionFileForUse";
                //Directory.CreateDirectory(folderPath);
                Directory.CreateDirectory(myAppFolderPath);

                // ส่งคืนเส้นทางเต็มรูปแบบที่รวมถึงโฟลเดอร์
                return Path.Combine(myAppFolderPath, $"LocalCiphertexts{NamePath}.db");
            }
        }

        public async Task<string> SetCombinePath() 
        {
            SetNamePath();
            var folderPath = $"D:\\Storage\\DecryptionFileForUse";
            Directory.CreateDirectory(myAppFolderPath);
            return await Task.FromResult(Path.Combine(myAppFolderPath, $"LocalCiphertexts{NamePath}.db"));
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
