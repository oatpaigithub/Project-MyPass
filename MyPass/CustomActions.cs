using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace TestFunctionSQL
{
    public class CustomActions
    {
        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public static void DeleteFiles()
        {
            // ตัวอย่างเส้นทางไฟล์ที่จะลบ
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\AlicePassword.db";

            // ตรวจสอบว่าไฟล์มีอยู่หรือไม่
            if (File.Exists(filePath))
            {
                // ถามผู้ใช้ว่าต้องการลบไฟล์หรือไม่
                DialogResult result = MessageBox.Show("ต้องการลบไฟล์เกมเซฟหรือไม่?", "ยืนยันการลบไฟล์", MessageBoxButtons.YesNo);

                // ถ้าผู้ใช้ต้องการลบ
                if (result == DialogResult.Yes)
                {
                    // ลบไฟล์
                    File.Delete(filePath);
                }
            }
        }
    }
}
