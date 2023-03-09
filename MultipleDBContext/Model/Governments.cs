using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MultipleDBContext.Model
{
    public class Governments
    {
        [Key]
        public int Government_Id { get; set; }
        public string Government_Name { get; set; }
    }
}
