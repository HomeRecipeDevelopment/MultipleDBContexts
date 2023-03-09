using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace MultipleDBContext.Model
{
    public class City
    {
        [Key]
        public int  Id {get; set;}
        public string CityName { get; set; }
        public int RegonId { get; set; }
    }
}
