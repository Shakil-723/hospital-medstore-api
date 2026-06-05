using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalMedstore.API.Data;
using HospitalMedstore.API.Models;

namespace HospitalMedstore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/product
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products
                .Where(p => p.IsActive)
                .ToListAsync();
            return Ok(products);
        }

        // GET api/product/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

            // GET api/product/category/Medicines
        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var products = await _context.Products
                .Where(p => p.Category == category && p.IsActive)
                .ToListAsync();
            return Ok(products);
        }

        // POST api/product
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Product added", product });
        }

        // PUT api/product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product updated)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            product.Name = updated.Name;
            product.Category = updated.Category;
            product.Brand = updated.Brand;
            product.Description = updated.Description;
            product.Price = updated.Price;
            product.OldPrice = updated.OldPrice;
            product.Stock = updated.Stock;
            product.Badge = updated.Badge;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Product updated" });
        }

        // DELETE api/product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            product.IsActive = false;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Product deleted" });
        }
    }
}