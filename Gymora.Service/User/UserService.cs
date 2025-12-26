using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gymora.Database;
using Gymora.Database.Entities;
using Gymora.Service.User.Messaging;
using Gymora.Service.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Gymora.Service.User
{
    public class UserService(IGymoraDbContext context):IUserService
    {
        public bool Exists(string username)
        {
            return context.Users.AsNoTracking().Any(x => x.Username == username);
        }
        public UserModel? GetByUsername(string username)
        {
            return context.Users.Include(x=>x.Coach).AsNoTracking().FirstOrDefault(x => x.Username == username);
        }

        public ApiResponse<UserModel> Create(CreateUserRequest request)
        {
            var userModel=new UserModel()
            {   
                PhoneNumber = request.PhoneNumber,
                Username = request.Username,
                UserType = request.UserType,
                Coach = request.Coach
            };
            userModel.Coach.User = userModel;
            context.Users.Add(userModel);
            context.SaveChangesAsync(CancellationToken.None).GetAwaiter().GetResult();
            return ResponseFactory.Success(userModel);
        }

        public ApiResponse Edit(EditUserRequest request)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == request.UserId);
            if (user is null)
                return ResponseFactory.Fail("کاربر یافت نشد");
            user.FullName = request.FullName;
            context.SaveChangesAsync(CancellationToken.None).GetAwaiter().GetResult();
            return ResponseFactory.Success();
        }
    }
}
