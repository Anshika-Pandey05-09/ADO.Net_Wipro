// // here we will implement the ProductsController
// using Microsoft.AspNetCore.Mvc;
// using RepoDemo.Api.Models;
// using RepoDemo.Api.Repository.Interface;

// namespace RepoDemo.Api.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class ProductsController : ControllerBase
//     {
//         private readonly IGenericRepository<Product> _productRepository;

//         public ProductsController(IGenericRepository<Product> productRepository)
//         {
//             _productRepository = productRepository;
//         }

//         // Implement the actions for the ProductsController

//         // To see this in action, navigate to /api/products
//         [HttpGet]
//         public async Task<IActionResult> GetAllProducts()
//         {
//             var products = await _productRepository.GetAllAsync();
//             return Ok(products);
//         }

//         [HttpGet("{id}")]
//         public async Task<IActionResult> GetProductById(int id)
//         {
//             var product = await _productRepository.GetByIdAsync(id);
//             if (product == null)
//             {
//                 return NotFound();
//             }
//             return Ok(product);
//         }

//         [HttpPost]
//         public async Task<IActionResult> CreateProduct(Product product)
//         {
//             await _productRepository.AddAsync(product);
//             return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
//         }



//         [HttpDelete("{id}")]
//         public async Task<IActionResult> DeleteProduct(int id)
//         {
//             await _productRepository.DeleteAsync(id);
//             return NoContent();
//         }
//     }
// }


//This controller manages the product-related API endpoints like CRUD operations
// It is used to interact with the product data in the database
//All the action methods in this controller will use the UnitOfWork to perform database operations
//Unit of Work further abstracts the repository layer and provides a single point of access to all repositories
//Unit of work is responsible for managing the lifetime of the repositories and ensuring that all changes are saved to the database in a single transaction


using Microsoft.AspNetCore.Mvc;
using RepoDemo.Api.Models;
using RepoDemo.Api.Repository.Interface;

namespace RepoDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Implement the actions for the ProductsController

        // To see this in action, navigate to /api/products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            await _unitOfWork.Products.AddAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _unitOfWork.Products.DeleteAsync(id);
            return NoContent();
        }
    }
}