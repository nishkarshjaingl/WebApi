using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConsumeApi.Models;
using Newtonsoft.Json;
using System.Text;

namespace ConsumeApi.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDBContext _context;

        public ProductsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.Products.Include(p => p.Categories).Include(p => p.UserDetails);
            return View(await appDBContext.ToListAsync());
        }
        public async Task<IActionResult> APIIndex()
        {
            

            
            List<Product> prod = new List<Product>();
            using (var httpClient = new HttpClient())//handler
            {
                using (var response = await httpClient.GetAsync("http://localhost:5114/api/Products"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    prod = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
                
            }
            return View(prod);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Categories)
                .Include(p => p.UserDetails)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CategoryName");
            ViewData["UserId"] = new SelectList(_context.UserDetails, "UserId", "FName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,CatId,UserId,ProductName,Price,Quantity")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(APIIndex));
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CategoryName", product.CatId);
            ViewData["UserId"] = new SelectList(_context.UserDetails, "UserId", "FName", product.UserId);
            return View(product);
        }

        public async Task<IActionResult> CreateWithWebAPI()
        {
            List<Category> CategoryList = new List<Category>();
            using (var httpClient = new HttpClient())//handler
            {
                using (var response = await httpClient.GetAsync("http://localhost:5114/api/Categories"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    CategoryList = JsonConvert.DeserializeObject<List<Category>>(apiResponse);
                }
            }
            List<UserDetails> UserList = new List<UserDetails>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5114/api/UserDetails"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    UserList = JsonConvert.DeserializeObject<List<UserDetails>>(apiResponse);
                }
            }

            ViewData["UserId"] = new SelectList(UserList, "UserId", "FName");
            ViewData["CatId"] = new SelectList(CategoryList, "CatId", "CategoryName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWithWebAPI([Bind("ProductId,CatId,UserId,ProductName,Price,Quantity")] Product product)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(product);
                //await _context.SaveChangesAsync();

                Product prod = new Product();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("http://localhost:5114/api/Products", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        prod = JsonConvert.DeserializeObject<Product>(apiResponse);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }


        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CategoryName", product.CatId);
            ViewData["UserId"] = new SelectList(_context.UserDetails, "UserId", "FName", product.UserId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,CatId,UserId,ProductName,Price,Quantity")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CategoryName", product.CatId);
            ViewData["UserId"] = new SelectList(_context.UserDetails, "UserId", "FName", product.UserId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Categories)
                .Include(p => p.UserDetails)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'AppDBContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
