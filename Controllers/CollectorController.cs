using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecycLab.Models;
using RecycLab.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecycLab.Controllers
{
    public class CollectorController : Controller
    {
        private readonly RecycLabContext _recycLabContext;

        public CollectorController(RecycLabContext recycLabContext)
        {
            _recycLabContext = recycLabContext;
        }

        // GET: /<controller>/
        public IActionResult Collectors()
        {
            // Include the Person navigation property to eagerly load related entities
            var collectorsWithDetails = _recycLabContext.Collectors
                .Include(P => P.Person)
                .Include(collector => collector.Confirmations)
                .ToList();

            return View(collectorsWithDetails);
        }

        public IActionResult Block(int id)
        {
            Collector collector = _recycLabContext.Collectors.Find(id);
            if (collector != null)
            {
                _recycLabContext.Collectors.Remove(collector);
                _recycLabContext.SaveChanges();
                return RedirectToAction("Collectors");
            }
            else return NotFound();
        }

        public IActionResult addCollector(string firstName , string lastName , string address , string phone , string email , DateTime birthdate , string auto , string autoType)
        {
            Person person = new Person(firstName, lastName, email, address, phone, birthdate);
            try
            {
                _recycLabContext.People.Add(person);
                _recycLabContext.SaveChanges();
                Collector collector = new Collector(auto, autoType);
                collector.FirstName = firstName;
                collector.LastName = lastName;
                collector.Address = address;
                collector.PhoneNumber = phone;
                collector.Email = email;
                collector.BirthDate = birthdate;

            
                _recycLabContext.Collectors.Add(collector);
                _recycLabContext.SaveChanges();
                return RedirectToAction("Collectors");

            }
            catch (Exception e)
            {

                // Provide a custom error message with inner exception details
                return Content(person.IdPerson+" An error occurred while adding the Collector: " + e.InnerException + e.Message);
            }

        }

        
    }
}
