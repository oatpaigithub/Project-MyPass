using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestFunctionSQL
{
    //[Table("CiphertextsServer")]
    public class CiphertextServer
    {
        //[Key]
        public int Id { get; set; }
        public string CiphertextPasswordServer { get; set; }
        public string UsernameConnectURL { get; set; }

    }
}
