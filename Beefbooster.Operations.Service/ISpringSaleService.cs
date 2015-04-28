using System.Collections.Generic;
using Beefbooster.Bull.Entities.Models;

namespace Beefbooster.Operations.Service
{
    public interface ISpringSaleService
    {
        SpringSale GetForSaleYear(int saleYear);
        IEnumerable<SpringSaleDate> CustomerSaleDatesForYear(int saleYear);
        //IEnumerable<VWPOD> PurchaseOrdersForSaleDate(int springSaleDateSN);
    }
}