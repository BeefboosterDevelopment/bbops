using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Pattern.Ef6;

namespace Beefbooster.Bull.Entities.Models
{
    public class VWPOD : Entity
    {
        [Key, Column(Order = 0)]
        public int POSN { get; set; }

        [Key, Column(Order = 1)]
        public int SpringSaleDateSN { get; set; }

        public string AccountNo { get; set; }
        public string Contact { get; set; }

        public decimal Amt { get; set; }
        public short? NBulls { get; set; }
        public int? InvNum { get; set; }

        public virtual PO PO { get; set; }
    }
}