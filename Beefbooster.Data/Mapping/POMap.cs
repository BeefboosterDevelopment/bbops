using System.Data.Entity.ModelConfiguration;
using Beefbooster.Bull.Entities.Models;

namespace Beefbooster.Data.Mapping
{
    public class POMap : EntityTypeConfiguration<PO>
    {
        public POMap()
        {
            ToTable("PO", "bt");

            // Primary Key
            HasKey(t => t.POSN);

            // Properties
            Property(t => t.AccountNo)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.DepositId)
                .HasMaxLength(20);

            // Table & Column Mappings
            Property(t => t.POSN).HasColumnName("POSN");
            Property(t => t.AccountNo).HasColumnName("AccountNo");
            Property(t => t.ReceiveDate).HasColumnName("ReceiveDate");
            Property(t => t.DepositId).HasColumnName("DepositId");

            Property(t => t.SpringSaleSN).HasColumnName("SpringSaleSN");
            HasRequired(t => t.SpringSale)
                .WithMany(t => t.POs)
                .HasForeignKey(d => d.SpringSaleSN);
        }
    }
}