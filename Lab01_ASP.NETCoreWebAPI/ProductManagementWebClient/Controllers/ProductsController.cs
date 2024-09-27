using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProductManagementWebClient.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient client;
        private readonly string ProductApiUrl = "";
        private readonly string CategoryApiUrl = "";

        public ProductsController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7165/api/Products";
            CategoryApiUrl = ProductApiUrl + "/categories";
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(strData, options);
            return View(listProducts);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/id?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var strData = await response.Content.ReadAsStringAsync();
                Product product = JsonSerializer.Deserialize<Product>(strData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(product);
            }
            return NotFound();
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            HttpResponseMessage response = await client.GetAsync(CategoryApiUrl);
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                List<Category> categories = JsonSerializer.Deserialize<List<Category>>(strData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
            }
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,CategoryId, UnitsInStock, UnitPrice")] Product product)
        {
            if (ModelState.IsValid)
            {
                var productJson = JsonSerializer.Serialize(product);
                var content = new StringContent(productJson, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(ProductApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/id?id={id}");
            HttpResponseMessage res = await client.GetAsync(CategoryApiUrl);
            if (response.IsSuccessStatusCode && res.IsSuccessStatusCode)
            {
                var strData = await response.Content.ReadAsStringAsync();
                Product product = JsonSerializer.Deserialize<Product>(strData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                strData = await res.Content.ReadAsStringAsync();
                List<Category> categories = JsonSerializer.Deserialize<List<Category>>(strData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
                return View(product);
            }
            return NotFound();
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,CategoryId, UnitsInStock, UnitPrice")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var productJson = JsonSerializer.Serialize(product);
                var content = new StringContent(productJson, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"{ProductApiUrl}/id?id={id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/id?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var strData = await response.Content.ReadAsStringAsync();
                Product product = JsonSerializer.Deserialize<Product>(strData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(product);
            }
            return NotFound();
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{ProductApiUrl}/id?id={id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
