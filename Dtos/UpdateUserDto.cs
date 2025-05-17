using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskTeamManagementSystem.Dtos
{
    public class UpdateUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
    }
}