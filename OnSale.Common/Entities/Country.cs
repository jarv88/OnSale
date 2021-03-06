using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnSale.Common.Entities
{
    public class Country
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "The field must contain less than 50 characters")]
        public string Name { get; set; }

        public ICollection<Department> Departments { get; set; } // relacion con tabla Departamentos

        [DisplayName("Departments Number")]
        public int DepartmentsNumber => Departments == null ? 0 : Departments.Count; // Propiedad de lectura para saber cuantos Departamentos tiene un pais
    }
}
