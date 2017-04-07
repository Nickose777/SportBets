namespace SportBet.Core.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public int RoleId { get; set; }
        public virtual RoleEntity Role { get; set; }
    }
}
