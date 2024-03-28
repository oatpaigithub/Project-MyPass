using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestFunctionSQL
{
    [Table("CiphertextLocal")]
    public class CiphertextLocal
    {
        [Key]
        public int Id { get; set; }

        public string CiphertextTitle { get; set; }
        public string CiphertextUsername { get; set; }
        //public string CiphertextPassword { get; set; }
        public string vectorBase64 { get; set; }
        public string CiphertextPasswordLocal { get; set; }
        //public string CiphertextPasswordServer { get; set; }
        public string URL { get; set; }
        public string UsernameConnectURL { get; set; }

    }
}
