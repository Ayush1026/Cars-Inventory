using CarsInventory.DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsInventory.DataAccessLayer.Repositories.Contracts
{
    public interface IAccountRepository
    {
        Task<UserModel> UserLogin(string userName, string password, string gender);
    }
}
