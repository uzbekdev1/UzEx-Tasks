using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Shopping.Services.IRepository
{
    public interface IUserRepo
    {
        Task<string> GetRoleAsync(string userName);
    }
}
