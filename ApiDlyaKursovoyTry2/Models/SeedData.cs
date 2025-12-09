
using ApiDlyaKursovoyTry2.Models;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDlyaKursovoyTry2.Models
{
    public static class SeedData
    {
        public static void SeedDatabase(NormalnayaKursovayaContext context)
        {
            // context.Database.Migrate();
            if (context.Admins.Count() == 0)
            {
                Admin user = new Admin { Email = "admin@mail.ru", Password = "1234" };
                user.Password = AuthOptions.GetHash(user.Password);
                context.Admins.Add(user);
                context.SaveChanges();
            }
        }
    }
}