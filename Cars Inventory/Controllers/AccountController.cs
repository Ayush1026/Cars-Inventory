using CarsInventory.BusinessLogicLayer.Services.Contracts;
using CarsInventory.DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace Cars_Inventory.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService) 
        {
            _accountService = accountService;
        }

        [HttpPost]
        public IActionResult Login(UserModel userModel)
        {
            string userName = userModel.Name;
            string password = userModel.Password;
            string gender = userModel.Gender;
            var loginData = _accountService.UserLogin(userName, password, gender);
            if (loginData == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "User not found");
            }
            return StatusCode(StatusCodes.Status200OK, loginData.Result);
        }
    }
}
