using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;

namespace TestFunctionSQL
{
    public class DbInMemoryDataBaseServer:DbContext
    {

        public DbSet<InMemoryDataBaseServer> InMemoryDataBase { get;  set;}
        private DbGenerateKey DbGenerateKey_this = new DbGenerateKey();
        private string NamePath;
        public string FolderPath = @"D:\Storage\ServerRemote";

        public void SetNamePath()
        {
            var existingGeneratekey = DbGenerateKey_this.GenerateKey.Find(1);

            if (existingGeneratekey != null)
            {
                // ทำการอัปเดตข้อมูลใน DbContext
                NamePath = existingGeneratekey.DeviceName;

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
                SetNamePath();
                // สร้างโฟลเดอร์หากยังไม่มี
                var folderPath = $"D:\\Storage\\ServerRemote";
                // Directory.CreateDirectory(folderPath);

                // ส่งคืนเส้นทางเต็มรูปแบบที่รวมถึงโฟลเดอร์
                return Path.Combine(folderPath, $"{NamePath}.csv");
            }
            set
            {
            }
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the context to use the in-memory database
            //optionsBuilder.UseInMemoryDatabase("memory");
            optionsBuilder.UseInMemoryDatabase(databaseName: "InMemoryDbImport");

        }


    }

}
