using System.Data.Entity.ModelConfiguration;
using Beefbooster.Bull.Entities.Models;

namespace Beefbooster.Data.Mapping
{
    public class SpringSaleMap : EntityTypeConfiguration<SpringSale>
    {
        public SpringSaleMap()
        {
            // Primary Key
            HasKey(t => t.SpringSaleSN);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("SpringSale");
            Property(t => t.SpringSaleSN).HasColumnName("SpringSaleSN");
            Property(t => t.CalfBirthYr_Num).HasColumnName("CalfBirthYr_Num");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.NextInvNum).HasColumnName("NextInvNum");
            Property(t => t.Locked_Flag).HasColumnName("Locked_Flag");
            Property(t => t.InsuranceRate).HasColumnName("InsuranceRate");
            Property(t => t.InsuranceRate_US).HasColumnName("InsuranceRate_US");
        }
    }
}