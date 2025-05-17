using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using taskTeamManagementSystem.Models;

namespace taskTeamManagementSystem.Data
{
    public class TaskDBContext : DbContext
    {
        public TaskDBContext(DbContextOptions<TaskDBContext> options) : base(options)
        {
            
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<taskTeamManagementSystem.Models.Task> Tasks { get; set; }
        public DbSet<taskTeamManagementSystem.Models.TaskStatus> TaskStatuses { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
    }
}