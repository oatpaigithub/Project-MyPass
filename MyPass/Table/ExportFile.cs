using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestFunctionSQL
{
    [Table("ExportFile")]
    public class ExportFile
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public string PainTextPassword { get; set; }
        public string URL { get; set; }
        public string UsernameConnectURL { get; set; }

        // Azure Storage 
        public string Connectionstring { get; set; }
        public string ContainerName { get; set; }



    }
}
