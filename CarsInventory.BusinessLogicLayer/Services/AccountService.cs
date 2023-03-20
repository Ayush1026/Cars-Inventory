using CarsInventory.BusinessLogicLayer.Services.Contracts;
using CarsInventory.DataAccessLayer.Model;
using CarsInventory.DataAccessLayer.Repositories.Contracts;

namespace CarsInventory.BusinessLogicLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<UserModel> UserLogin(string userName, string password, string gender)
        {
            try
            {
                var result = await _accountRepository.UserLogin(userName, password, gender);
                return result;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error with Login", exception);
            }
        }
    }
}
