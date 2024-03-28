using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestFunctionSQL
{
    [Table("ConnectionString")]
    public class AzureConnectionString
    {
        [Key]
        public int Id { get; set; }
        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }

    }
}
