using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnSale.Common.Entities;
using OnSale.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSale.Web.Controllers
{
    public class CountriesController : Controller
    {
        private readonly DataContext _context;
        public CountriesController(DataContext context)
        {
            _context = context;
        }
        // GET: CountriesController
        public async Task<IActionResult> Index()
        {
            return View(await _context.Countries.Include(c => c.Departments).ToListAsync());
        }

        // GET: CountriesController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var country = await _context.Countries
                        .Include(c => c.Departments) // Include y thenInclude son como InnerJoins en SQL
                        .ThenInclude(d => d.Cities)
                        .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: CountriesController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CountriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country) // [Bind("Id,Name")]
        {

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Add(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }

                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

            }

            return View(country);
        }

        // GET: CountriesController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: CountriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!CountryExists(country.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}

                    
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate") || dbUpdateException.InnerException.Message.Contains("duplicada"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }

                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
               // return RedirectToAction("Index");

            }
  
            return View(country);
        }

        // GET: CountriesController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var country = await _context.Countries
                        .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //return View(country);
        }

        // POST: CountriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCountry(int? id)
        {
            var country = _context.Countries.Find(id);

            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            //TempData["message"] = "The category has deleted successfully";
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> AddDepartment(int? id) // se envia id del pais
        {
            if (id == null)
            {
                return NotFound();
            }

            Country country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            Department model = new Department { IdCountry = country.Id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                Country country = await _context.Countries
                    .Include(c => c.Departments)
                    .FirstOrDefaultAsync(c => c.Id == department.IdCountry);
                if (country == null)
                {
                    return NotFound();
                }

                try
                {
                    department.Id = 0;
                    country.Departments.Add(department);
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"Details", new { country.Id });

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(department);
        }










        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
