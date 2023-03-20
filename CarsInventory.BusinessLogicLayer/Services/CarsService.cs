using CarsInventory.BusinessLogicLayer.Services.Contracts;
using CarsInventory.DataAccessLayer.Model;
using CarsInventory.DataAccessLayer.Repositories.Contracts;

namespace CarsInventory.BusinessLogicLayer.Services
{
    public class CarsService : ICarService
    {
        private readonly ICarsRepository _carsRepository;
        public CarsService(ICarsRepository carsRepository)
        {
            _carsRepository = carsRepository;
        }

        /// <summary>
        /// Get all cars from the database and check whether the data is null or not 
        /// </summary>
        /// <returns>All cars</returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<CarsModel>> GetAll()
        {
            try
            {
                var result = await _carsRepository.GetAll();
                if (result == null)
                {
                    return result;
                }
                return result;
            }
            catch (Exception exception)
            {
                throw new Exception("Error getting all cars: ", exception);
            }
        }

        /// <summary>
        /// Creates the cars 
        /// </summary>
        /// <param name="carsModel"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<CarsModel> Create(CarsModel carsModel)
        {
            try
            {
                var addCar = await _carsRepository.Create(carsModel);
                return addCar;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error while creating car for user", exception);
            }
        }

        /// <summary>
        /// Deletes the car and check whether the carid should not be null
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deletes car data</returns>
        /// <exception cref="Exception"></exception>
        public async Task<CarsModel> Delete(int id)
        {
            try
            {
                NoCarExistsWithGivenId(id);
                var deleteCar = await _carsRepository.Delete(id);
                return deleteCar;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error while deleting cars for user with id {id}", exception);
            }
        }

        /// <summary>
        /// Updates the car data and check that the data is null or not 
        /// </summary>
        /// <param name="carModel"></param>
        /// <returns>Updated car value</returns>
        /// <exception cref="Exception"></exception>
        public async Task<CarsModel> Update(CarsModel carModel)
        {
            try
            {
                if (carModel == null)
                {
                    return carModel;
                }
                
                var updateCarData = await _carsRepository.Update(carModel);
                return updateCarData;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error while Updating cars", exception);
            }
        }

        /// <summary>
        /// Get the car which is searched by giving the id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Searched car</returns>
        /// <exception cref="Exception"></exception>
        public async Task<CarsModel> GetById(int id)
        {
            try
            {
                NoCarExistsWithGivenId(id);
                var getCar = await _carsRepository.GetById(id);
                return getCar;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error getting car with id {id}", exception);
            }
        }

        /// <summary>
        /// Checks the id entered by the user is null or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static int NoCarExistsWithGivenId(int id)
        {
            if(id ==null)
            {
                return 0;
            }
            return id;
        }

        /// <summary>
        /// Get the cars for the particular user with their id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>cars which belongs to that user</returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<CarsModel>> GetCarsByUser(int id)
        {
            try
            {
               var getCars =await _carsRepository.GetCarsForParticularUserById(id);
               return getCars;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error getting cars for user with id - {id}", exception);
            }
        }

        /// <summary>
        /// Searches the car data which is entered by any user based on the brand and model
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="userId"></param>
        /// <returns>Searched car based on model and brand</returns>
        /// <exception cref="Exception"></exception>
        public Task<List<CarsModel>> GetSearchedTerm(string searchString, int userId)
        {
            try
            {
                var getSearchedTerm =  _carsRepository.GetSearchedTerm(searchString, userId);
                return getSearchedTerm;
            }
            catch (Exception exception)
            {
                throw new Exception($"Error searching for cars with search string {searchString} and user id {userId}", exception);
            }
        }
    }
}
