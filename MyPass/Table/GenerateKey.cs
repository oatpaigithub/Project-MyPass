using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TestFunctionSQL
{
    [Table("GenerateKey")]
    public class GenerateKey
    {
        [Key]
        public int Id { get; set; }

        //public string AesKey { get; set; }

        public string GeneratedKey { get; set; }
        // public string Sha256Hash { get; set; }
        // public string ChangeSha256HashForUse { get; set; }
        // Update
        public string HashMasterPassword { get; set; }
        public string HashNameFile { get; set; }

        public string VectorBase64ForAllData { get; set; }
        public string VectorBase64ForFile { get; set; }
        public string VectorBase64ForAzureConnection { get; set; }
        public string VectorBase64ForAzureContainer { get; set; }
        public string DeviceName { get; set; }



    }
}
