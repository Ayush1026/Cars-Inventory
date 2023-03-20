using CarsInventory.BusinessLogicLayer.Services.Contracts;
using CarsInventory.DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace Cars_Inventory.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCars()
        {
            try
            {
                var carRecords = await _carService.GetAll();
                Logger.LoggerLog4net.Instance.Info("Info:");   
                return StatusCode(StatusCodes.Status200OK, carRecords);
            }
            catch (Exception exception)
            {
                Logger.LoggerLog4net.Instance.Error("Exception: ", exception);
                return StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateCar([FromBody] CarsModel carModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createdCar = await _carService.Create(carModel);
                    Logger.LoggerLog4net.Instance.Info("Info: Create() called from CarService");
                    return StatusCode(StatusCodes.Status200OK, createdCar);
                }
                Logger.LoggerLog4net.Instance.Debug("Debugging-");
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception exception)
            {
                Logger.LoggerLog4net.Instance.Error("Exception: ", exception);
                return StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCar(int id)
        {
            try
            {
                var deletedCar = await _carService.Delete(id);
                Logger.LoggerLog4net.Instance.Info("Info: Deleting the car model");
                return StatusCode(StatusCodes.Status200OK, deletedCar);
            }
            catch (Exception exception)
            {
                Logger.LoggerLog4net.Instance.Error("Exception: ", exception);
                return StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCar([FromBody] CarsModel carModel)
        {
            try
            {
                var updatedCar = await _carService.Update(carModel);
                Logger.LoggerLog4net.Instance.Info("Info: Updating the Car Model");
                return StatusCode(StatusCodes.Status200OK, updatedCar);
            }
            catch (Exception exception)
            {
                Logger.LoggerLog4net.Instance.Error("Exception: ", exception);
                return StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCarById([FromRoute] int id)
        {
            try
            {
                var getCar = await _carService.GetById(id);
                Logger.LoggerLog4net.Instance.Info("Info:Getting the searched Cars by Id -GetById() ");
                return StatusCode(StatusCodes.Status200OK, getCar);
            }
            catch (Exception exception)
            {
                Logger.LoggerLog4net.Instance.Error("Exception: ", exception);
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> SearchCars(string searchTerm, int userId)
        {
            try
            {
                var searchedCars = await _carService.GetSearchedTerm(searchTerm,userId);
                Logger.LoggerLog4net.Instance.Info("Info: Getting the searched Cars by GetSearchedTerm Service");
                return StatusCode(StatusCodes.Status200OK, searchedCars);
            }
            catch (Exception exception)
            {
                Logger.LoggerLog4net.Instance.Error("Exception: ", exception);
                return StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CarsModel>>> GetCarsByUserId(int id)
        {
            try
            {
                var getCarsOfSpecificUser = await _carService.GetCarsByUser(id);
                Logger.LoggerLog4net.Instance.Info("Info: GetCarsByUserId() called ");
                return StatusCode(StatusCodes.Status200OK, getCarsOfSpecificUser);
            }
            catch (Exception exception)
            {
                Logger.LoggerLog4net.Instance.Error("Exception: ", exception);
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
        }
    }
}
