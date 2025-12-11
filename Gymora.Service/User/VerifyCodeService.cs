using Gymora.Database.Entities;
using Gymora.Service.User.Messaging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Gymora.Database;

namespace Gymora.Service.User
{
    public class VerifyCodeService(IGymoraDbContext context):IVerifyCodeService
    {

        public bool CheckExistsSendVerifyCode(string phoneNumber)
        {
            return context.VerifyCodes.Any(x =>
                x.PhoneNumber == phoneNumber &&
                EF.Functions.DateDiffSecond(x.CreateDateTime, DateTime.Now) <= 60);

        }

        public bool VerifyCode(VerifyCodeViewModel model)
        {
            return context.VerifyCodes.Any(x => x.Code == model.Code && x.PhoneNumber == model.PhoneNumber && EF.Functions.DateDiffSecond(x.CreateDateTime, DateTime.Now) <= 60);
        }


        public long? CheckExistsUserForSendVerifyCode(string phoneNumber, UserType userType)
        {
            var user = context.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber && x.UserType == userType);
            return user?.Id;
        }

        public async Task<string> SendVerifyCode(string phoneNumber)
        {
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("X-API-KEY", "Add1Jj3wTbdVBbpRg5BNUoUVjbEjDlBxnPNAdcW8FJKBNaEQIy2ZHFXTclcPEJfT");

            var code = GenerateVerifyCode().ToString();


            var requestBody = new SmsVerifyCodeRequestDto()
            {
                Mobile = phoneNumber,
                TemplateId = 283825,
                Parameters = new List<ParameterObject>()
                {
                    new ParameterObject()
                    {
                        Name = "Code", Value = code
                    }
                }
            };

            var response = await client.PostAsJsonAsync("https://api.sms.ir/v1/send/verify", requestBody);

            var result = await response.Content.ReadAsStringAsync();

            return code;
        }
        private int GenerateVerifyCode()
        {
            const int lengthOfCode = 6;
            var keys = "123456789".ToCharArray();
            return Convert.ToInt32(GenerateCode(keys, lengthOfCode));
        }
        private static string GenerateCode(char[] keys, int lengthOfCode)
        {
            var random = new Random();
            return Enumerable
                .Range(1, lengthOfCode) // for(i.. ) 
                .Select(k => keys[random.Next(0, keys.Length - 1)])  // generate a new random char 
                .Aggregate("", (e, c) => e + c); // join into a string
        }
    }
}
