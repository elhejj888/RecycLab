using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using RecycLab.Models;

namespace RecycLab.Controllers;

public class HomeController : Controller
{
    private readonly RecycLabContext _recyclabContext;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger , RecycLabContext recycLabContext)
    {
        _logger = logger;
        _recyclabContext = recycLabContext;
    }

    public IActionResult Index()
    {
        var usersCount = _recyclabContext.Users.Count();
        var collectorsCount = _recyclabContext.Collectors.Count();
        var confirmationsCount = _recyclabContext.Confirmations.Count();
        var transactionsCount = _recyclabContext.Transactions.Count();
        var TopCollectors = _recyclabContext.Collectors
            .Include(c => c.Confirmations)
            .OrderByDescending(c => c.Confirmations.Count)
            .Take(5)
            .ToList();
        var TopSellers = _recyclabContext.Users
            .Include(u => u.Transactions) // Inclure les transactions pour chaque utilisateur
            .OrderByDescending(u => u.Transactions.Count) // Trier par le nombre de transactions
            .Take(5) // Prendre les premiers utilisateurs en fonction du paramètre "limit"
            .ToList();
        var TopProducts = _recyclabContext.Dechets
            .Include(u => u.Transactions)
            .OrderByDescending(u => u.Transactions.Count)
            .Take(5)
            .ToList();
        var sum = _recyclabContext.Transactions.Sum(e => e.TotalPrice);

        var Counts = new
        {
            TopProducts = TopProducts,
            TopCollectors = TopCollectors,
            TopSellers = TopSellers,
            UsersCount = usersCount,
            CollectorsCount = collectorsCount,
            ConfirmationsCount = confirmationsCount,
            TransactionsCount = transactionsCount,
            Sum = sum,
        };
        return View(Counts);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

