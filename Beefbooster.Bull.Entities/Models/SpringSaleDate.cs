using System;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace Beefbooster.Bull.Entities.Models
{
    public sealed class SpringSaleDate : Entity
    {
        public SpringSaleDate()
        {
           PODetails = new List<VWPOD>();
        }

        public int SpringSaleDateSN { get; set; }
        public int SpringSaleSN { get; set; }
        public DateTime SaleDate { get; set; }
        public string StrainCode { get; set; }
        public byte BreederDay { get; set; }
       // public string Description { get; set; }

        public SpringSale SpringSale { get; set; }
        public ICollection<VWPOD> PODetails { get; set; }
    }
}