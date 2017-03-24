using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Core.Models
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Login { get; set; }

        public int RoleId { get; set; }
        public virtual RoleEntity Role { get; set; }
    }
}
