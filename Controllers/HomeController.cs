using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using PayPal.Api;
using RecycLab.Models;
using System.Linq;


namespace RecycLab.Controllers;

public class HomeController : Controller
{
    private readonly RecycLabContext _recyclabContext;
    private readonly ILogger<HomeController> _logger;
    private IHttpContextAccessor httpContextAccessor;
    IConfiguration _configuration;


    public HomeController(ILogger<HomeController> logger , RecycLabContext recycLabContext , IHttpContextAccessor context, IConfiguration iconfiguration)
    {
        _logger = logger;
        _recyclabContext = recycLabContext;
        httpContextAccessor = context;
        _configuration = iconfiguration;
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
        var dailyTransactionCounts = _recyclabContext.Transactions
            .GroupBy(t => t.TransactionDate.Value.Date) // Group by the date part of TransactionDate
            .Select(g => new
            {
                TransactionDay = g.Key,
                TransactionCount = g.Count()
            })
            .ToList();
        var dailyConfirmationCounts = _recyclabContext.Confirmations
            .GroupBy(c => c.ConfirmationDate.Value.Date) // Group by the date part of ConfirmationDate
            .Select(g => new
            {
                ConfirmationDay = g.Key,
                ConfirmationCount = g.Count()
            })
            .ToList();

        var dechetTransactionCounts = _recyclabContext.Transactions
        .GroupBy(t => t.Dechet)
        .Select(g => new
        {
            Dechet = g.Key,
            TransactionCount = g.Count()
        })
        .ToList();

        var Counts = new
        {
            dailyConfirmationCounts = dailyConfirmationCounts,
            dailyTransactionCounts = dailyTransactionCounts,
            dechetTransactionCounts = dechetTransactionCounts,
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



    public ActionResult PaymentWithPaypal(string dechet, string price, string quantity , string Cancel = null, string blogId = "", string PayerID = "", string guid = "" )
    {
        //getting the apiContext  
        var ClientID = "AR76d0CiNF58grETQqJlgfeCBaXmklcspsysbylYWsX2fMqlLYPnBSwDbYGDvMbXlNMJBMy1MwApB6_V";
        var ClientSecret = "EJCC512dI_b71i-biK0_Eruv48kbpxtieCuiyug7aBPRK-kgOzccvxV4uQ9CAA-hA9RA-Yj0j-vS-zeR";
        var mode = "sandbox";
        APIContext apiContext = PaypalConfiguration.GetAPIContext(ClientID, ClientSecret, mode);
        // apiContext.AccessToken="Bearer access_token$production$j27yms5fthzx9vzm$c123e8e154c510d70ad20e396dd28287";
        try
        {
            //A resource representing a Payer that funds a payment Payment Method as paypal  
            //Payer Id will be returned when payment proceeds or click to pay  
            string payerId = PayerID;
            if (string.IsNullOrEmpty(payerId))
            {
                //this section will be executed first because PayerID doesn't exist  
                //it is returned by the create function call of the payment class  
                // Creating a payment  
                // baseURL is the url on which paypal sendsback the data.  
                string baseURI = this.Request.Scheme + "://" + this.Request.Host + "/Home/PaymentWithPayPal?";
                //here we are generating guid for storing the paymentID received in session  
                //which will be used in the payment execution  
                var guidd = Convert.ToString((new Random()).Next(100000));
                guid = guidd;
                //CreatePayment function gives us the payment approval url  
                //on which payer is redirected for paypal account payment  
                var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, blogId , dechet , price , quantity);
                //get links returned from paypal in response to Create function call  
                var links = createdPayment.links.GetEnumerator();
                string paypalRedirectUrl = null;
                while (links.MoveNext())
                {
                    Links lnk = links.Current;
                    if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                    {
                        //saving the payapalredirect URL to which user will be redirected for payment  
                        paypalRedirectUrl = lnk.href;
                    }
                }
                // saving the paymentID in the key guid  
                httpContextAccessor.HttpContext.Session.SetString("payment", createdPayment.id);
                return Redirect(paypalRedirectUrl);
            }
            else
            {
                // This function exectues after receving all parameters for the payment  

                var paymentId = httpContextAccessor.HttpContext.Session.GetString("payment");
                var executedPayment = ExecutePayment(apiContext, payerId, paymentId as string);
                //If executed payment failed then we will show payment failure message to user  
                if (executedPayment.state.ToLower() != "approved")
                {

                    return View("PaymentFailed");
                }
                var blogIds = executedPayment.transactions[0].item_list.items[0].sku;


                return View("PaymentSuccess");
            }
        }
        catch (Exception ex)
        {
            return View("PaymentFailed");
        }
        //on successful payment, show success page to user.  
        return View("SuccessView");
    }
    private PayPal.Api.Payment payment;
    private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
    {
        var paymentExecution = new PaymentExecution()
        {
            payer_id = payerId
        };
        this.payment = new Payment()
        {
            id = paymentId
        };
        return this.payment.Execute(apiContext, paymentExecution);
    }
    private Payment CreatePayment(APIContext apiContext, string redirectUrl, string blogId , string dechet , string price , string quantity)
    {
        //create itemlist and add item objects to it  

        var itemList = new ItemList()
        {
            items = new List<Item>()
        };
        //Adding Item Details like name, currency, price etc  
        itemList.items.Add(new Item()
        {
            name = dechet,
            currency = "USD",
            price = price,
            quantity = quantity,
            sku = "asd"
        });
        var tot = Convert.ToDouble(price) * Convert.ToDouble(quantity);
        var payer = new Payer()
        {
            payment_method = "paypal"
        };
        // Configure Redirect Urls here with RedirectUrls object  
        var redirUrls = new RedirectUrls()
        {
            cancel_url = redirectUrl + "&Cancel=true",
            return_url = redirectUrl
        };
        // Adding Tax, shipping and Subtotal details  
        //var details = new Details()
        //{
        //    tax = "1",
        //    shipping = "1",
        //    subtotal = "1"
        //};
        //Final amount with details  
        var amount = new Amount()
        {
            currency = "USD",
            total = tot.ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                            //details = details
        };
        var transactionList = new List<PayPal.Api.Transaction>();
        // Adding description about the transaction  
        transactionList.Add(new PayPal.Api.Transaction()

        {
            description = "Transaction description",
            invoice_number = Guid.NewGuid().ToString(), //Generate an Invoice No  
            amount = amount,
            item_list = itemList
        });
        this.payment = new Payment()
        {
            intent = "sale",
            payer = payer,
            transactions = transactionList,
            redirect_urls = redirUrls
        };
        // Create a payment using a APIContext  
        return this.payment.Create(apiContext);
    }

}



