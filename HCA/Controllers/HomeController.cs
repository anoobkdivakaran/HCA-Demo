using HCA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HCA.Controllers
{
    public class HomeController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;

        public HomeController(ILogger<HomeController> logger, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = new HttpClient();
            _apiSettings = apiSettings.Value;
            _httpClient.BaseAddress =new  System.Uri(_apiSettings.BaseUrl);
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("book");
            var jsonString = await response.Content.ReadAsStringAsync();
            var Books = JsonConvert.DeserializeObject<IEnumerable<Book>>(jsonString);
            return View(Books);
        }

        //GET: Books/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync($"book/{id}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var Book = JsonConvert.DeserializeObject<Book>(jsonString);

            if (Book == null)
            {
                return NotFound();
            }

            return View(Book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Author,Price")] Book book)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(book);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                await _httpClient.PostAsync("book", contentString);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync($"book/{id}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var Book = JsonConvert.DeserializeObject<Book>(jsonString);

            if (Book == null)
            {
                return NotFound();
            }
            return View(Book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Description,Author,Price")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(book);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                await _httpClient.PutAsync($"book/{id}", contentString);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _httpClient.DeleteAsync($"book/{id}");
            return RedirectToAction(nameof(Index));
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
}
