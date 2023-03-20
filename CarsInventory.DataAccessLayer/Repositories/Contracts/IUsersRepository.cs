using CarsInventory.DataAccessLayer.Model;

namespace CarsInventory.DataAccessLayer.Repositories.Contracts
{
    public interface IUsersRepository
    {
        Task<UserModel> CreateProfile(UserModel userModel);
        Task<UserModel> GetById(int id);
    }
}
