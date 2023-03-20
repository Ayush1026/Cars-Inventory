using CarsInventory.DataAccessLayer.Model;
using CarsInventory.DataAccessLayer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CarsInventory.DataAccessLayer.Repositories
{
    public class CarsRepository : ICarsRepository
    {
        private readonly DatabaseContext _databaseContext;
        public CarsRepository(DatabaseContext databaseContext)
        {
           _databaseContext =  databaseContext; 
        }
        /// <summary>
        /// Get all cars from the Database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<CarsModel>> GetAll()
        {
            try
            {
                return await _databaseContext.Cars.ToListAsync();
            }
            catch (Exception exception)
            {
                throw new Exception("Error getting all cars: ", exception);
            }
        }

        /// <summary>
        /// Creates the cars and store into the Database
        /// </summary>
        /// <param name="carsModel"></param>
        /// <returns>New Car</returns>
        /// <exception cref="Exception"></exception>
        public async Task<CarsModel> Create(CarsModel carsModel)
        {
            try
            {
                var userModel = await _databaseContext.Users.FindAsync(carsModel.UserId);
                carsModel.UserModels = userModel;
                var createdCar = await _databaseContext.Cars.AddAsync(carsModel);
                await _databaseContext.SaveChangesAsync();
                return createdCar.Entity;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error Creating car for user", exception);
            }
        }

        /// <summary>
        /// Deletes the Car data from the Database 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete the car </returns>
        /// <exception cref="Exception"></exception>
        public async Task<CarsModel> Delete(int id)
        {
            try
            {
                CarsModel getCarById = await _databaseContext.Cars.FirstOrDefaultAsync(deleteCar => deleteCar.CarId == id);
                var deleteCarData = _databaseContext.Cars.Remove(getCarById);
                await _databaseContext.SaveChangesAsync();
                return deleteCarData.Entity;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error while deleting cars for user with id {id}", exception);
            }
        }

        /// <summary>
        /// Updates the car data which is entered by the user and make a new record in the database
        /// </summary>
        /// <param name="objCarsModel"></param>
        /// <returns>Updates the car data</returns>
        /// <exception cref="Exception"></exception>
        public async Task<CarsModel> Update(CarsModel objCarsModel)
        {
            try
            {
                var updateResult = _databaseContext.Cars.Update(objCarsModel);
                await _databaseContext.SaveChangesAsync();
                return updateResult.Entity;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error while Updating cars", exception);
            }
        }

        /// <summary>
        /// Gets the car data from the user entered id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Get the car by id</returns>
        /// <exception cref="Exception"></exception>
        public async Task<CarsModel> GetById(int id)
        {
            try
            {
                return await _databaseContext.Cars.FindAsync(id);
            }
            catch (Exception exception)
            {
                throw new Exception($"Error getting car with id {id}", exception);
            }
        }
        /// <summary>
        /// Get the cars which is searched by the user based on the model and brand
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="userId"></param>
        /// <returns>Searched car data</returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<CarsModel>> GetSearchedTerm(string searchString, int userId)
        {
            try
            {
                return await _databaseContext.Cars.Where(cars => (cars.Brand.Contains(searchString)
                                                       || cars.Model.Contains(searchString))
                                                      && cars.UserId == userId).ToListAsync();
            }
            catch (Exception exception)
            {
                throw new Exception($"Error searching for cars with search string {searchString} and user id {userId}", exception);
            }
        }

        /// <summary>
        /// Get the all cars which is entered by the particular user by UserId
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Cars which belongs to the particular user</returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<CarsModel>> GetCarsForParticularUserById(int id)
        {
            try
            {
                var cars = await _databaseContext.Cars.Where(carsId => carsId.UserId == id).ToListAsync();
                return cars;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error getting cars for user with id - {id}", exception);
            }
        }
    }
}
