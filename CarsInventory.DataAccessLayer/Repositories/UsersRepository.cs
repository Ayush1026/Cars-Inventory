using CarsInventory.DataAccessLayer.Model;
using CarsInventory.DataAccessLayer.Repositories.Contracts;

namespace CarsInventory.DataAccessLayer.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DatabaseContext _databaseContext;
        public UsersRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        /// <summary>
        ///  Creates a new profile of the user and save the data to the Database
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>New User</returns>
        /// <exception cref="Exception"></exception>
        public async Task<UserModel> CreateProfile(UserModel userModel)
        {
            try
            {
                var createdUser = await _databaseContext.Users.AddAsync(userModel);
                await _databaseContext.SaveChangesAsync();
                return createdUser.Entity;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error for creating profile for user", exception);
            }
        }

        /// <summary>
        /// Get the user from the database based on their id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>searched user</returns>
        /// <exception cref="Exception"></exception>
        public async Task<UserModel> GetById(int id)
        {
            try
            {
                var records = await _databaseContext.Users.FindAsync(id);
                return records;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error for getting user with id - {id}", exception);
            }
        }
    }
}
