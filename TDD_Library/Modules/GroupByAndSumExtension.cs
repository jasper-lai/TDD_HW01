using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD_Library.Models;
using TDD_Library.Modules;

namespace TDD_Library.Modules
{
    public static class GroupByAndSumExtension
    {

        /// <summary>
        /// 分頁取某欄位值總和
        /// 本擴充方法適用於幾筆為1組的方式, 例如: 每3筆為1組, 則 
        ///     1, 2, 3     為1組, 
        ///     4, 5, 6     為1組,
        ///     7, 8, 9     為1組,
        ///    10, 11       為1組
        /// 使用範例: .GroupByAndSum(3, x => x.Cost);
        /// </summary>
        /// <typeparam name="T">泛型資料型別</typeparam>
        /// <param name="source">資料集合</param>
        /// <param name="pageSize">幾筆為1組</param>
        /// <param name="selectorSum">Sum的實際運算式</param>
        /// <returns></returns>
        /// <remarks>
        /// 本擴充方法用於將傳入的集合, 依指定的 每頁筆數 / 加總運算式, 進行資料分組, 並回傳加總的結果
        /// </remarks>
        public static IEnumerable<int> GroupByAndSum<T>(this IEnumerable<T> source, int pageSize, Func<T, int> selectorSum)
        {

            #region 檢查傳入參數

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException($"分組的筆數必須大於 0 !");
            }

            if (null == selectorSum)
            {
                throw new ArgumentNullException($"未提供加總的欄位運算 !");
            }

            #endregion

            #region 利用 Skip() + Take() + Sum() 直接作處理, 不再自行以其它類別 (MyGroup<T>) 進行手工分組

            var datas = source.ToList();

            var index = 0;
            while (index <= datas.Count())
            {
                yield return datas.Skip(index).Take(pageSize).Sum(selectorSum);
                index += pageSize;
            }

            #endregion

        }


        /// <summary>
        /// 本擴充方法用於將傳入的集合, 依指定的 群組運算式 / 加總運算式, 進行資料分組, 並回傳加總的結果
        /// 本擴充方法適用於固定組數的方式, 例如: 群組運算式 採用 Id % 3, 代表要分為3組 , 則 
        ///     1, 4, 7, 10 為1組, 
        ///     2, 5, 8, 11 為1組,
        ///     3, 6, 9,    為1組
        /// 使用範例: .GroupByAndSum2(x => x.Id % 3, x => x.Cost);
        /// </summary>
        /// <typeparam name="T">泛型資料型別</typeparam>
        /// <param name="source">資料集合</param>
        /// <param name="selectorGroupBy">GroupBy的實際運算式</param>
        /// <param name="selectorSum">Sum的實際運算式</param>
        /// <returns></returns>
        public static IEnumerable<int> GroupByAndSum2<T>(this IEnumerable<T> source, Func<T, int> selectorGroupBy, Func<T, int> selectorSum)
        {
            #region 檢查傳入參數

            if (null == selectorGroupBy)
            {
                throw new ArgumentNullException($"未提供分組的欄位運算 !");
            }

            if (null == selectorSum)
            {
                throw new ArgumentNullException($"未提供加總的欄位運算 !");
            }

            #endregion

            #region 分組並取得各組內部加總的值

            var groupSum = source.GroupBy(selectorGroupBy, (id, items) => new
            {
                Key = id,
                Count = items.Count(),
                Sum = items.Sum(selectorSum),
            });

            #endregion

            #region 回傳結果

            return groupSum.Select(x => x.Sum);

            #endregion
        }

    }
}
