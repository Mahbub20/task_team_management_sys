using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using taskTeamManagementSystem.Data;
using taskTeamManagementSystem.Models;

namespace taskTeamManagementSystem.Seeds
{
    public class DataSeeder
    {
        public static void SeedData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<TaskDBContext>();
                if (context != null && !context.Roles.Any())
                {
                    context.Roles.AddRange(new List<Role>
                    {
                        new() {Name = "Admin"},
                        new() {Name = "Manager"},
                        new() {Name = "Employee"},
                    });
                    context.SaveChanges();
                }

                if (context != null && !context.Users.Any())

                {
                    var hasher = new PasswordHasher<User>();

                    var adminRoleId = context.Roles.FirstOrDefault(r => r.Name == "Admin")?.Id ?? 0;
                    var managerRoleId = context.Roles.FirstOrDefault(r => r.Name == "Manager")?.Id ?? 0;
                    var employeeRoleId = context.Roles.FirstOrDefault(r => r.Name == "Employee")?.Id ?? 0;

                    var adminUser = new User
                    {
                        FullName = "Admin User",
                        Email = "admin@demo.com",
                        RoleId = adminRoleId
                    };
                    adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin123!");

                    var managerUser = new User
                    {
                        FullName = "Manager User",
                        Email = "manager@demo.com",
                        RoleId = managerRoleId
                    };
                    managerUser.PasswordHash = hasher.HashPassword(managerUser, "Manager123!");

                    var employeeUser = new User
                    {
                        FullName = "Employee",
                        Email = "employee@demo.com",
                        RoleId = employeeRoleId
                    };
                    employeeUser.PasswordHash = hasher.HashPassword(employeeUser, "Employee123!");

                    context.Users.AddRange(adminUser, managerUser, employeeUser);
                    context.SaveChanges();
                }
                
                if(context != null && !context.TaskStatuses.Any())
                {
                    context.TaskStatuses.AddRange(new List<taskTeamManagementSystem.Models.TaskStatus>
                    {
                        new() {Name = "Todo"},
                        new() {Name = "InProgress"},
                        new() {Name = "Done"}
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}