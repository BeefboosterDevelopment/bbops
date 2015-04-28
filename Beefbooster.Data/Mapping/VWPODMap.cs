using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Beefbooster.Bull.Entities.Models;

namespace Beefbooster.Data.Mapping
{
    public class VWPODMap : EntityTypeConfiguration<VWPOD>
    {
        public VWPODMap()
        {
            // Primary Key
            HasKey(t => new {t.POSN, t.SpringSaleDateSN});
            
            //ToTable("VWPOD");
            Property(t => t.POSN).HasColumnName("POSN").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.SpringSaleDateSN)
                .HasColumnName("SpringSaleDateSN")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.Amt).HasColumnName("Amt");
            Property(t => t.NBulls).HasColumnName("NBulls");
            Property(t => t.InvNum).HasColumnName("InvNum");


            // columns from the view
            Property(t => t.AccountNo);
            Property(t => t.Contact);            


            // Relationships
/*            HasRequired(t => t.PO)
                .WithMany(t => t.Details)
                .HasForeignKey(d => d.POSN);*/

            // Relationships
            //  Note: don't do this! because we are using the view (vwPOD)
            //        it will confound things - as it inner joins onto SpringSaleDate
            //                               AND inner joins on vwPO
//            HasRequired(t => t.SpringSaleDate)
//                .WithMany(t => t.PODetails)
//                .HasForeignKey(t => t.SpringSaleDateSN);
        }
    }
}