using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnSale.Common.Entities
{
    public class City
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field must contain less than 50 characters")]
        [Required]
        public string Name { get; set; }

        //public Department department { get; set; } // se puede referenciar de esta forma con la talba Departments, o al contrario
    }

}
