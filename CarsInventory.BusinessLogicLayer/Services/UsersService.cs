using CarsInventory.BusinessLogicLayer.Services.Contracts;
using CarsInventory.DataAccessLayer.Model;
using CarsInventory.DataAccessLayer.Repositories.Contracts;

namespace CarsInventory.BusinessLogicLayer.Services
{
    public class UsersService : IUserService
    {
        private readonly IUsersRepository _userRepository;
        public UsersService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }
        /// <summary>
        /// Creates a new profile of the user by checking if UserData is not null
        /// </summary>
        /// <param name="carsModel"></param>
        /// <returns>A new user profile</returns>
        /// <exception cref="Exception"></exception>
        public async Task<UserModel> CreateProfile(UserModel carsModel)
        {
            try
            {
                if (carsModel == null)
                {
                    return carsModel;
                }
                var addCar = await _userRepository.CreateProfile(carsModel);
                return addCar;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error for creating profile for user", exception);
            }
        }

        /// <summary>
        /// Get the profile of the user by checking if id is not null
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Gets the user based on the searched id</returns>
        /// <exception cref="Exception"></exception>
        public async Task<UserModel> GetUsersById(int id)
        {
            try
            {
                NoCarExists(id);
                var getCar = await _userRepository.GetById(id);
                return getCar;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error for getting user with id - {id}", exception);
            }
        }

        /// <summary>
        /// Check the id parameter if it is null or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns>user id </returns>
        private static int NoCarExists(int id)
        {
            if (id == null)
            {
                return 0;
            }
            return id;
        }
    }
}
