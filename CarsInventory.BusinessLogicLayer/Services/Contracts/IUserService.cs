using CarsInventory.DataAccessLayer.Model;

namespace CarsInventory.BusinessLogicLayer.Services.Contracts
{
    public interface IUserService
    {
        Task<UserModel> CreateProfile(UserModel userModel);
        Task<UserModel> GetUsersById(int id);
    }
}
