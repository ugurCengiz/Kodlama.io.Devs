using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Security.Entities;

namespace Application.Services.UserService
{
    public interface IUserService
    {
        public Task<User> GetByMail(string email);

    }
}
