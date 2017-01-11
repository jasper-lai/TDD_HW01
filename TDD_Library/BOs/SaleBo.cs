using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD_Library.Models;
using TDD_Library.Modules;

namespace TDD_Library.BOs
{
    public class SaleBo
    {

        /// <summary>
        /// 取得每3筆的 Cost 欄位總和
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public List<int> GetSumForCost(List<SaleModel> datas)
        {

            List<int> results = new List<int>();

            results = datas.GroupByAndSum(3, x => x.Cost).ToList();

            return results;
        }

        /// <summary>
        /// 取得每4筆的 Revenue 欄位總和
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public List<int> GetSumForRevenue(List<SaleModel> datas)
        {
            List<int> results = new List<int>();

            results = datas.GroupByAndSum(4, x => x.Revenue).ToList();

            return results;
        }

    }
}
