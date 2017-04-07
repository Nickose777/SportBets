using System.Data.Entity.ModelConfiguration;
using SportBet.Core.Entities;

namespace SportBet.Data.Configurations
{
    class RoleConfiguraion : EntityTypeConfiguration<RoleEntity>
    {
        public RoleConfiguraion()
        {
            //Table and Columns
            this.ToTable("Roles", "public");
            this.Property(role => role.Id)
                .HasColumnName("RoleNo");

            //Primary Keys
            this.HasKey(role => role.Id);

            //Other Settings
            this.Property(role => role.Name)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
