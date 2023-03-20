using CarsInventory.DataAccessLayer.Model;
using Newtonsoft.Json;
using System.Text;

namespace CarsInventory.Web.Controllers
{
    public class CarsApiHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _baseAddress = new Uri("https://localhost:44371/api/");

        public CarsApiHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = _baseAddress;
        }

        public async Task<List<CarsModel>> GetCarsByUserId(int userId)
        {
            var response = await _httpClient.GetAsync($"Cars/GetCarsByUserId/{userId}");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CarsModel>>(data);
        }

        public async Task<List<CarsModel>> SearchCars(string searchTerm, int userId)
        {
            var response = await _httpClient.GetAsync($"Cars/SearchCars?searchTerm={searchTerm}&userId={userId}");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CarsModel>>(data);
        }

        public async Task<CarsModel> GetCarById(int id)
        {
            var response = await _httpClient.GetAsync($"Cars/UpdateCar/{id}");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CarsModel>(data);
        }

        public async Task CreateCar(CarsModel carsModel)
        {
            var data = JsonConvert.SerializeObject(carsModel);
            using var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Cars/CreateCar", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateCar(CarsModel carsModel)
        {
            var data = JsonConvert.SerializeObject(carsModel);
            using var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Cars/UpdateCar/{carsModel.CarId}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCar(int id)
        {
            var response = await _httpClient.DeleteAsync($"Cars/DeleteCar/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
