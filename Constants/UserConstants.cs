using JwtApp.Models;
using System.Collections.Generic;

namespace JwtApp.Constants
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel()
            {
                Username = "jason_admin",
                Password = "Pass@123",
                Email = "jason.admin@email.com",
                Role = "Administrator",
                FirstName = "Jason",
                LastName = "Logan"
            },
            new UserModel()
            {
                Username = "mason_seller",
                Password = "Pass@123",
                Email = "mason.seller@email.com",
                Role = "Seller",
                FirstName = "Mason",
                LastName = "Swager"
            },
            new UserModel()
            {
                Username = "rason_admin",
                Password = "Pass@123",
                Email = "rason.admin@email.com",
                Role = "Administrator",
                FirstName = "Rason",
                LastName = "Loson"
            },

        };
    }
}
