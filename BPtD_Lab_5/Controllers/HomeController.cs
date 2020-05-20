using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BPtD_Lab_5.Models;
using Newtonsoft.Json;

namespace BPtD_Lab_5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(Items);
        }

        public IActionResult Cart(string items)
        {
            var cart = items
                .Split('|')
                .Select(int.Parse)
                .Distinct()
                .Select(x => Items.Find(i => i.Id == x))
                .ToList();

            var totalPrice = Convert.ToDecimal(cart.Sum(x => x.Price));

            var (dataHash, signatureHash) =
                LiqPayHelper.GetLiqPayProcessedData(totalPrice, Guid.NewGuid().ToString(), "http://localhost:5000/Home/LiqPayResult");

            var cartViewModel = new CartViewModel
            {
                Data = dataHash,
                Signature = signatureHash,
                Cart = cart
            };

            return View(cartViewModel);
        }

        [HttpPost]
        public IActionResult LiqPayResult()
        {
            var requestDictionary = Request.Form.Keys.ToDictionary(key => key, key => Request.Form[key]);

            var requestData = Convert.FromBase64String(requestDictionary["data"]);
            var decodedString = Encoding.UTF8.GetString(requestData);
            var requestDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(decodedString);
            var mySignature = LiqPayHelper.GetLiqPaySignature(requestDictionary["data"]);

            if (!mySignature.Equals(requestDictionary["signature"]))
            {
                return RedirectToAction("Completed", new { success = false, result = "ERROR" });
            }

            var orderId = requestDataDictionary["order_id"];
            var transactionId = requestDataDictionary["transaction_id"];

            return RedirectToAction("Completed", new { success = true, result = "Success", orderId, transactionId });
        }

        public IActionResult Completed(bool success, string result, string orderId, string transactionId)
        {
            ViewData["Success"] = success;
            ViewData["Message"] = result;
            ViewData["OrderId"] = orderId;
            ViewData["TransactionId"] = transactionId;

            return View();
        }

        private static readonly List<Item> Items = new List<Item>
        {
            new Item { Id = 1, Name = "Laba 1", Price = 100 },
            new Item { Id = 2, Name = "Laba 2", Price = 200 },
            new Item { Id = 3, Name = "Laba 3", Price = 300 },
            new Item { Id = 4, Name = "Laba 4", Price = 400 },
            new Item { Id = 5, Name = "Laba 5", Price = 500 },
            new Item { Id = 6, Name = "Cursova", Price = 999.99 },
        };
    }
}
