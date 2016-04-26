using System.Collections.Generic;
using System.Linq;
using Beefbooster.Bull.Entities.Models;
using Repository.Pattern.Repositories;

namespace Beefbooster.Operations.Service
{
    public class SpringSaleService : ISpringSaleService
    {
        //private readonly IRepositoryAsync<SpringSaleDate> _springSaleDateRepository;
        private readonly IRepositoryAsync<SpringSale> _springSaleRepository;

        public SpringSaleService(IRepositoryAsync<SpringSale> springSaleRepository //,
            /*IRepositoryAsync<SpringSaleDate> springSaleDateRepository*/)
        {
            //_springSaleDateRepository = springSaleDateRepository;
            _springSaleRepository = springSaleRepository;
        }

        public IEnumerable<SpringSaleDate> CustomerSaleDatesForYear(int saleYear)
        {
            SpringSale sale = GetForSaleYear(saleYear);

            if (sale != null)
                return sale.SpringSaleDates
                    .Where(d => d.BreederDay == 0)
                    .ToList();

            return new List<SpringSaleDate>();
        }

        public SpringSale GetForSaleYear(int saleYear)
        {
            return _springSaleRepository
                .Query(s => s.CalfBirthYr_Num == saleYear - 1)
                .Include(s => s.SpringSaleDates)
                .Select()
                .FirstOrDefault();
        }
    }
}