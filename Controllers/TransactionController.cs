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
    public class TransactionController : Controller
    {
        private readonly RecycLabContext _recycLabContext;
        // GET: /<controller>/

        public TransactionController(RecycLabContext recycLabContext)
        {
            _recycLabContext = recycLabContext;
        }
        public IActionResult Index()
        {
            var TransactionsDetails = _recycLabContext.Transactions
                .Include(P => P.User)
                .Include(s => s.Dechet)
                .ToList();
            return View(TransactionsDetails);
        }

        public IActionResult addTransaction(string firstName, string lastName, string address, string phone, string ddate, string prods, string etat, string quantity, string totalPrice)
        {
            Transaction transaction = new Transaction(22, Convert.ToInt32(prods), Convert.ToInt32(quantity), (float)Convert.ToDouble(totalPrice), etat, "achat");
            var test = _recycLabContext.Transactions.Add(transaction);
            _recycLabContext.SaveChanges();

            return RedirectToAction("addTransac", "Product");
        }
        public IActionResult DelTransaction(int id)
        {
            Transaction transac = _recycLabContext.Transactions.Find(id);
            if (transac != null)
            {
                _recycLabContext.Transactions.Remove(transac);
                _recycLabContext.SaveChanges();
                return RedirectToAction("Index");

            }

            else return NotFound();

        }
    }
}

