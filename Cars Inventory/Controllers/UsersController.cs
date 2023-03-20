using CarsInventory.BusinessLogicLayer.Services.Contracts;
using CarsInventory.DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace Cars_Inventory.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserModel userModel)
        {
            try
            {
                var createdUser = await _userService.CreateProfile(userModel);
                Logger.LoggerLog4net.Instance.Info("Info: CreateProfile() called from UserService");
                return StatusCode(StatusCodes.Status200OK, createdUser);
            }
            catch (Exception exception)
            {
                Logger.LoggerLog4net.Instance.Error("Error message:", exception);
                return StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById([FromRoute] int id)
        {
            try
            {
                var getUser = await _userService.GetUsersById(id);
                Logger.LoggerLog4net.Instance.Info("Info: GetUsersById() called from UserService");
                return StatusCode(StatusCodes.Status200OK, getUser);
            }
            catch (Exception exception)
            {
                Logger.LoggerLog4net.Instance.Error("Error message:", exception);
                return StatusCode(StatusCodes.Status404NotFound, exception.Message);
            }
        }


    }
}
