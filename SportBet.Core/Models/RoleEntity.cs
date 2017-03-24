using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Core.Models
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserEntity> Users { get; set; }
    }
}
