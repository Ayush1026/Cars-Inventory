using CarsInventory.DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CarsInventory.Web.Controllers
{
    public class CarsController : Controller 
    {
        Uri baseAddress = new Uri("https://localhost:44371/api/");
        HttpClient client;
        public CarsController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public async Task<IActionResult> Index(string searchTerm)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return RedirectToAction("UserLogin", "User");
            }
            ViewBag.UserId = userId;
            ViewData["SearchFilter"] = searchTerm;
            List<CarsModel> carsData = new List<CarsModel>();
            if (string.IsNullOrEmpty(searchTerm))
            {
                carsData = await GetCarsByUserId(userId);
            }
            else
            {
                carsData = await SearchCars(searchTerm, userId);
            }
            return View(carsData);
        }

        private int? GetUserId()
        {
            return HttpContext.Session.GetInt32("UserId");
        }

        private async Task<List<CarsModel>> GetCarsByUserId(int? userId)
        {
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "Cars/GetCarsByUserId/" + userId);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<CarsModel>>(data);
            }
            else
            {
                ModelState.AddModelError(string.Empty, $"Bad request: {responseContent}");
                return new List<CarsModel>();
            }
        }

        private async Task<List<CarsModel>> SearchCars(string searchTerm, int? userId)
        {
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + $"Cars/SearchCars?searchTerm={searchTerm}&userId={userId}");
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<CarsModel>>(data);
            }
            else
            {
                ModelState.AddModelError(string.Empty, $"Bad request: {responseContent}");
                return new List<CarsModel>();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarsModel carsModel)
        {
            using (var client = this.client)
            {
                try
                {
                    string data = JsonConvert.SerializeObject(carsModel);
                    using StringContent content = new(data, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(client.BaseAddress + $"Cars/CreateCar", content);
                    var responseContent = await result.Content.ReadAsStringAsync();
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError(string.Empty, $"Bad request: {responseContent}");
                    return View("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, $"Server Error: {exception.Message}");
                    return View("Index");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                CarsModel carsModel = new CarsModel();
                var response = await client.GetAsync(client.BaseAddress + $"Cars/UpdateCar/" + id);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    carsModel = JsonConvert.DeserializeObject<CarsModel>(data);
                }
                return View("Create", carsModel);
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, $"Server Error: {exception.Message}");
                return View("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarsModel carsModel)
        {
            try
            {
                string data = JsonConvert.SerializeObject(carsModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(client.BaseAddress + $"Cars/UpdateCar/" + carsModel.CarId, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, $"Bad request: {responseContent}");
                return View("Create", carsModel);
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, $"Server Error: {exception.Message}");
                return View("Index");
            }
        }
       
       
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await client.DeleteAsync(client.BaseAddress + $"Cars/DeleteCar/{id}");
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Cars");
                }
                ModelState.AddModelError(string.Empty, $"Bad request: {responseContent}");
                return View("Index");
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, $"Server Error: {exception.Message}");
                return View("Index");
            }
        }
    }
}
