using Gymora.Database.Entities;
using Gymora.Service.User.Messaging;
using Gymora.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.User
{
    public interface IUserService
    {
        bool Exists(string username);
        UserModel? GetByUsername(string username);
        ApiResponse<UserModel> Create(CreateUserRequest request);
        ApiResponse Edit(EditUserRequest request);

    }
}
