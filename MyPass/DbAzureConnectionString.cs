using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestFunctionSQL
{
    public class DbAzureConnectionString : DbContext
    {
        public DbSet<AzureConnectionString> azureConnectionStrings { get; set; }

        public string myAppFolderPath;
        public string myAppMainFolderPath;
        public DbAzureConnectionString() 
        {
            // รับพาธไปยังโฟลเดอร์ "Documents" ของผู้ใช้ปัจจุบัน
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            myAppMainFolderPath = Path.Combine(documentsPath, "MyPassDocument");
            myAppFolderPath = Path.Combine(documentsPath, "MyPassDocument\\ConnectionString\\AzureStorage");

        }

        public string DatabasePath
        {
            get
            {
                Directory.CreateDirectory(myAppFolderPath);
                // ส่งคืนเส้นทางเต็มรูปแบบที่รวมถึงโฟลเดอร์
                return Path.Combine(myAppFolderPath, $"AzureStorageConnectionString.db");
            }
        }


        public async Task<string> SetCombinePath()
        {

            Directory.CreateDirectory(myAppFolderPath);
            return await Task.FromResult(Path.Combine(myAppFolderPath, $"AzureStorageConnectionString.db"));

        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite($"Data Source={DatabasePath}");


        }





    }
}
