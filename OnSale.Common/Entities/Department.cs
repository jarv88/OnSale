using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnSale.Common.Entities
{
    public class Department
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field must contain less than 50 characters")]
        [Required]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; } // relacion con tabla ciudades

        [DisplayName("Cities Number")] 
        public int CitiesNumber => Cities == null ? 0 : Cities.Count; // Propiedad de lectura para saber cuantas cuidades tiene un departamento


        [JsonIgnore] // No va al JSON
        [NotMapped] // no va a la BD
        public int IdCountry { get; set; }
    }

}
