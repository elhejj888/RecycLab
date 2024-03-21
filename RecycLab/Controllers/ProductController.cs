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
    public class ProductController : Controller
    {
        private readonly RecycLabContext _recycLabContext;

        public ProductController(RecycLabContext recycLabContext)
        {
            _recycLabContext = recycLabContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var productsDetails = _recycLabContext.Dechets
                .Include(P => P.Transactions)
                .ToList();
            return View(productsDetails);
        }

        public IActionResult addProduct(string productType , string unitPrice)
        {
            Dechet dechet = new Dechet(productType, (float)Convert.ToDouble(unitPrice));
            _recycLabContext.Dechets.Add(dechet);
            _recycLabContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            Dechet product = _recycLabContext.Dechets.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            _recycLabContext.Dechets.Remove(product);
            _recycLabContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult addTransac()
        {
            var Products = _recycLabContext.Dechets.ToList();

            return View(Products);
        }
    }
}

