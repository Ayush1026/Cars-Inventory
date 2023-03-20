using CarsInventory.DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CarsInventory.Web.Controllers
{
    public class UserController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44371/api/");
        HttpClient client;
        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> UserLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UserLogin(UserModel userModel)
        {
            using (var client = this.client)
            {
                string data = JsonConvert.SerializeObject(userModel);
                ViewData["Validate Message"] = "No User Found";
                using StringContent content = new(data, Encoding.UTF8, "application/json");
                var apiResult = await client.PostAsync($"Account/Login", content);
                var responseContent = await apiResult.Content.ReadAsStringAsync();
                if (apiResult.IsSuccessStatusCode)
                {
                    var userData = JsonConvert.DeserializeObject<UserModel>(responseContent);
                    if(userData==null)
                    {
                        return RedirectToAction("UserLogin", "User");
                    }
                    HttpContext.Session.SetInt32("UserId", userData.UserId);
                    return RedirectToAction("Index", "Cars");
                }
                ModelState.AddModelError(string.Empty, $"Bad request: {responseContent}");
                return View("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserModel userModel)
        {
            using (var client = this.client)
            {
                try
                {
                    string data = JsonConvert.SerializeObject(userModel);
                    using StringContent content = new(data, Encoding.UTF8, "application/json");
                    var apiResult = await client.PostAsync(client.BaseAddress + $"Users/CreateUser", content);
                    var responseContent = await apiResult.Content.ReadAsStringAsync();
                    if (apiResult.IsSuccessStatusCode)
                    {
                        return RedirectToAction("UserLogin");
                    }
                    ModelState.AddModelError(string.Empty, $"Bad request: {responseContent}");
                    return View("SignUp");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, $"Server Error: {exception.Message}");
                    return View("SignUp");
                }
            }

        }
    }
}