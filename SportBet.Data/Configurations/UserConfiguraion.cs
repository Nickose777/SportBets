using System.Data.Entity.ModelConfiguration;
using SportBet.Core.Entities;

namespace SportBet.Data.Configurations
{
    class UserConfiguraion : EntityTypeConfiguration<UserEntity>
    {
        public UserConfiguraion()
        {
            //Table and Columns
            this.ToTable("Users", "public");
            this.Property(user => user.Id)
                .HasColumnName("UserNo");
            this.Property(user => user.RoleId)
                .HasColumnName("UserRoleNo");

            //Primary Keys
            this.HasKey(user => user.Id);

            //Foreign Keys
            this.HasRequired<RoleEntity>(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(user => user.RoleId);

            //Other Settings
            this.Property(user => user.Login)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
