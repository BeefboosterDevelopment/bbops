using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace Beefbooster.Bull.Entities.Models
{
    public class PO : Entity
    {
        public PO()
        {
            Details = new List<VWPOD>();
        }

        [Key]
        public int POSN { get; set; }

        public string AccountNo { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string DepositId { get; set; }

        public int SpringSaleSN { get; set; }
        public virtual SpringSale SpringSale { get; set; }

        public virtual ICollection<VWPOD> Details { get; set; }
    }
}