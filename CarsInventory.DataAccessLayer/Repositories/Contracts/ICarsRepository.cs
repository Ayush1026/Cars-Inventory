using CarsInventory.DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsInventory.DataAccessLayer.Repositories.Contracts
{
    public interface ICarsRepository
    {
        Task<List<CarsModel>> GetAll();
        Task<CarsModel> Create(CarsModel objCarsModel);
        Task<CarsModel> Delete(int id);
        Task<CarsModel> Update(CarsModel objCarsModel);
        Task<CarsModel> GetById(int id);
        Task<List<CarsModel>> GetSearchedTerm(string searchString, int userId);
        Task<List<CarsModel>> GetCarsForParticularUserById(int id);

    }
}
