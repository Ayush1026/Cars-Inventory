using CarsInventory.DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsInventory.BusinessLogicLayer.Services.Contracts
{
    public interface IAccountService
    {
        Task<UserModel> UserLogin(string userName, string password, string gender);
    }
}
