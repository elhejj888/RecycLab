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
    public class ConfirmationController : Controller
    {
        private readonly RecycLabContext _recycLabContext;
        // GET: /<controller>/

        public ConfirmationController(RecycLabContext recycLabContext)
        {
            _recycLabContext = recycLabContext;
        }

        public IActionResult Index()
        {
            var confirmationDetails = _recycLabContext.Confirmations
                .Include(e => e.Transaction)
                .Include(s => s.Collector)
                .Include(f=>f.Transaction.Dechet)
                .ToList();
            return View(confirmationDetails);
        }

        public IActionResult Confirm(int transacId , DateTime dateExec)
        {
            Transaction transaction = _recycLabContext.Transactions.Find(transacId);
            if(transaction != null)
            {
                transaction.isConfirmed = true;
                _recycLabContext.SaveChanges();

            }
            Confirmation confirmation = new Confirmation( );
            confirmation.IdTransaction = transacId;
            confirmation.IdCollector = 28;
            confirmation.ExecutionDate = dateExec;
            _recycLabContext.Confirmations.Add(confirmation);
            _recycLabContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

