using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD_Library.Models;

namespace TDD_Library.Modules
{
    public class DataMaker
    {
        public List<SaleModel> MakeData()
        {
            List<SaleModel> results = new List<SaleModel>();

            results.Add(new SaleModel { Id = 1, Cost = 1, Revenue = 11, SellPrice = 21 });
            results.Add(new SaleModel { Id = 2, Cost = 2, Revenue = 12, SellPrice = 22 });
            results.Add(new SaleModel { Id = 3, Cost = 3, Revenue = 13, SellPrice = 23 });
            results.Add(new SaleModel { Id = 4, Cost = 4, Revenue = 14, SellPrice = 24 });
            results.Add(new SaleModel { Id = 5, Cost = 5, Revenue = 15, SellPrice = 25 });
            results.Add(new SaleModel { Id = 6, Cost = 6, Revenue = 16, SellPrice = 26 });
            results.Add(new SaleModel { Id = 7, Cost = 7, Revenue = 17, SellPrice = 27 });
            results.Add(new SaleModel { Id = 8, Cost = 8, Revenue = 18, SellPrice = 28 });
            results.Add(new SaleModel { Id = 9, Cost = 9, Revenue = 19, SellPrice = 29 });
            results.Add(new SaleModel { Id = 10, Cost = 10, Revenue = 20, SellPrice = 30 });
            results.Add(new SaleModel { Id = 11, Cost = 11, Revenue = 21, SellPrice = 31 });

            return results;

        }

    }
}
