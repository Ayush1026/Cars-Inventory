using CarsInventory.DataAccessLayer.Model;
using CarsInventory.DataAccessLayer.Repositories.Contracts;

namespace CarsInventory.DataAccessLayer.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DatabaseContext _databaseContext;

        public AccountRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task<UserModel> UserLogin(string userName, string password, string gender)
        {
            try
            {
                var userData = _databaseContext.Users.Where(user =>
                user.Name.ToLower() == userName.ToLower()
                && user.Password.ToLower() == password.ToLower()
                && user.Gender.ToLower() == gender.ToLower()).FirstOrDefault();
                return userData;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error for Login :", exception);
            }
        }
    }
}
