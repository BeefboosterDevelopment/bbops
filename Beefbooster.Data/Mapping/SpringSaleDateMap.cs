using System.Data.Entity.ModelConfiguration;
using Beefbooster.Bull.Entities.Models;

namespace Beefbooster.Data.Mapping
{
    public class SpringSaleDateMap : EntityTypeConfiguration<SpringSaleDate>
    {
        public SpringSaleDateMap()
        {
            // Primary Key
            HasKey(t => t.SpringSaleDateSN);

            // Properties
            Property(t => t.StrainCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            ToTable("SpringSaleDate", "bt");
            Property(t => t.SpringSaleDateSN).HasColumnName("SpringSaleDateSN");
            Property(t => t.SpringSaleSN).HasColumnName("SpringSaleSN");
            Property(t => t.SaleDate).HasColumnName("SaleDate");
            Property(t => t.StrainCode).HasColumnName("StrainCode");
            Property(t => t.BreederDay).HasColumnName("BreederDay");
            //Property(t => t.Description).HasColumnName("Description");

            // Relationships
            HasRequired(t => t.SpringSale)
                .WithMany(t => t.SpringSaleDates)
                .HasForeignKey(d => d.SpringSaleSN);
        }
    }
}