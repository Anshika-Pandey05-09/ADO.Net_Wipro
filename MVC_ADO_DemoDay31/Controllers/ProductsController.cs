using MVC_ADO_DemoDay31.Data;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;

namespace MVC_ADO_DemoDay31.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductsRepository _productsRepository;

        public ProductsController(ProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        // Action methods go here
        // Example: Get all products
        public IActionResult Index()
        {
            var products = _productsRepository.GetProducts();
            return View(products);
        }

        // Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name, decimal price)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(name) && price > 0)
            {
                var productId = _productsRepository.InsertProduct(name, price);
                return RedirectToAction("Index");
            }
            return View();
        }


    }
}