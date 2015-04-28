using System;
using System.Collections.Generic;
using System.Linq;
using Beefbooster.Bull.Entities.Models;
using Repository.Pattern.Repositories;

namespace Beefbooster.Operations.Service
{
    public class ShufflerService : IShufflerService
    {
        private readonly IRepositoryAsync<VWPOD> _poRepository;
        private readonly Random _random = new Random();

        public ShufflerService(IRepositoryAsync<VWPOD> poRepository)
        {
            _poRepository = poRepository;
        }

        public string[] BingoDraw(int saleDateSN)
        {
            // all purchase order details
            IEnumerable<VWPOD> pods = _poRepository
                .Query(d => d.SpringSaleDateSN == saleDateSN)
                .Select()
                .ToList();

            // extract the accounts - 1 time for each bull orderd
            var accounts = new List<string>();
            foreach (VWPOD detail in pods)
            {
                if (detail.NBulls.HasValue)
                {
                    for (int i = 0; i < detail.NBulls; i++)
                        accounts.Add(detail.Contact);
                }
            }
            return RandomizeStrings(accounts.ToArray());
            //return RandomizeStrings(RandomizeStrings(RandomizeStrings(accounts.ToArray())));
        }

        private string[] RandomizeStrings(string[] arr)
        {
            List<KeyValuePair<int, string>> list = arr
                .Select(s => new KeyValuePair<int, string>(_random.Next(), s))
                .ToList();
            // Add all strings from array
            // Add new random int each time
            // Sort the list by the random number
            IOrderedEnumerable<KeyValuePair<int, string>> sorted = from item in list
                orderby item.Key
                select item;
            // Allocate new string array
            var result = new string[arr.Length];
            // Copy values to array
            int index = 0;
            foreach (var pair in sorted)
            {
                result[index] = pair.Value;
                index++;
            }
            // Return copied array
            return result;
        }
    }
}