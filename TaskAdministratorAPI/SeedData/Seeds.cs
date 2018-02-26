using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskAdministratorAPI.Models;

namespace TaskAdministratorAPI.SeedData
{
    public static class Seeds
    {
        public static void InitializeSeed(IServiceProvider serviceProvider)
        {
            using (var context = new TaskAdministratorAPIContext(serviceProvider.GetRequiredService<DbContextOptions<TaskAdministratorAPIContext>>()))
            {
                if (context.Tasks.Any() || context.Users.Any())
                {
                    return;
                }

                context.Tasks.AddRange(
                    
                new Tasks
                {
                    Title = "Mow The Lawn",
                    BeginDateTime = DateTime.Now,
                    DeadlineDateTime = DateTime.Parse("01/01/2019 12:00:00 AM", System.Globalization.CultureInfo.InvariantCulture),
                    Requirements = "This task is carried out by mowing the lawn."
                },

                new Tasks
                {
                    Title = "Walk The Dog",
                    BeginDateTime = DateTime.Now,
                    DeadlineDateTime = DateTime.Parse("01/01/2019 12:00:00 AM", System.Globalization.CultureInfo.InvariantCulture),
                    Requirements = "This task is carried out by walking the dog."
                },

                new Tasks
                {
                    Title = "Feed The Fish",
                    BeginDateTime = DateTime.Now,
                    DeadlineDateTime = DateTime.Parse("01/01/2019 12:00:00 AM", System.Globalization.CultureInfo.InvariantCulture),
                    Requirements = "This task is carried out by feeding the fish."
                }

                );

                context.Users.AddRange(

                new Users
                {
                    FirstName = "Iñaki",
                    LastName = "Ordóñez"
                },

                new Users
                {
                    FirstName = "Rafael",
                    LastName = "Bauzá"
                },

                new Users
                {
                    FirstName = "Cesc",
                    LastName = "Fábregas"
                }

                );
                context.SaveChanges();
            }
        }
    }
}
