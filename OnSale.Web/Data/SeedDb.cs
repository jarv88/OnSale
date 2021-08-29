using OnSale.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSale.Web.Data
{
    
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any()) // Any significa si hay algo en la BD
            {
                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    Departments = new List<Department>
                {
                    new Department
                    {
                        Name = "Antioquia",
                        Cities = new List<City>
                        {
                            new City { Name = "Medellín" },
                            new City { Name = "Envigado" },
                            new City { Name = "Itagüí" }
                        }
                    },
                    new Department
                    {
                        Name = "Bogotá",
                        Cities = new List<City>
                        {
                            new City { Name = "Bogotá" }
                        }
                    },
                    new Department
                    {
                        Name = "Valle del Cauca",
                        Cities = new List<City>
                        {
                            new City { Name = "Calí" },
                            new City { Name = "Buenaventura" },
                            new City { Name = "Palmira" }
                        }
                    }
                }
                });
                _context.Countries.Add(new Country
                {
                    Name = "Venezuela",
                    Departments = new List<Department>
                {
                    new Department
                    {
                        Name = "Sucre",
                        Cities = new List<City>
                        {
                            new City { Name = "Carupano" },
                            new City { Name = "Cumana" },
                            
                        }
                    },
                    new Department
                    {
                        Name = "Anzoategui",
                        Cities = new List<City>
                        {
                            new City { Name = "Puerto La Cruz" },
                            new City { Name = "Barcelona" }
                        }
                    }
                }
                });
                _context.Countries.Add(new Country
                {
                    Name = "Peru",
                    Departments = new List<Department>
                    {
                        new Department
                        {
                            Name = "Lima",
                            Cities = new List<City>
                            {
                                new City { Name = "Lima" },
                                new City { Name = "Huaral" },

                            }
                        },
                    
                    }
                });
                await _context.SaveChangesAsync();
            }
        }

    }
}
