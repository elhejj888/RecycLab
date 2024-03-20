using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using RecycLab.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecycLab.Controllers
{
    public class UserController : Controller
    {

        private readonly RecycLabContext _recycLabContext;

        public UserController(RecycLabContext recycLabContext)
        {
            _recycLabContext = recycLabContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var users = _recycLabContext.Users
                .Include(e => e.Person)
                .Include(e => e.Transactions)
                .ToList();

            return View(users);
        }

        public IActionResult addUser(string firstName, string lastName, string address, string phone, string email, DateTime birthdate)
        {
            Person person = new Person(firstName, lastName, email, address, phone, birthdate);
            try
            {
                _recycLabContext.People.Add(person);
                _recycLabContext.SaveChanges();
                User user = new User(0);
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Address = address;
                user.PhoneNumber = phone;
                user.Email = email;
                user.BirthDate = birthdate;


                _recycLabContext.Users.Add(user);
                _recycLabContext.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {

                // Provide a custom error message with inner exception details
                return Content(person.IdPerson + " An error occurred while adding the Collector: " + e.InnerException + e.Message);
            }

        }

        public IActionResult Block(int id)
        {
            User user = _recycLabContext.Users.Find(id);
            if (user != null)
            {
                _recycLabContext.Users.Remove(user);
                _recycLabContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else return NotFound();
        }


    }
}

