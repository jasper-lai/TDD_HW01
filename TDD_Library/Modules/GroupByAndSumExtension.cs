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
        /// 本擴充方法用於將傳入的集合, 依指定的 筆數 / 加總運算式, 進行資料分組, 並回傳加總的結果
        /// 本擴充方法適用於幾筆為1組的方式, 例如: 每3筆為1組, 則 
        ///     1, 2, 3     為1組, 
        ///     4, 5, 6     為1組,
        ///     7, 8, 9     為1組,
        ///    10, 11       為1組
        /// 使用範例: .GroupByAndSum<SaleModel>(3, x => x.Cost);
        /// </summary>
        /// <typeparam name="T">泛型資料型別</typeparam>
        /// <param name="datas">資料集合</param>
        /// <param name="groupCnt">幾筆為1組</param>
        /// <param name="funcSum">Sum的實際運算式</param>
        /// <returns></returns>
        public static IEnumerable<int> GroupByAndSum<T>(this IEnumerable<T> datas, int groupCnt, Func<T, int> funcSum)
        {
            //List<int> results = new List<int>();

            #region 檢查傳入參數

            if (groupCnt <= 0)
            {
                throw new ArgumentOutOfRangeException("分組的筆數必須大於 0 !");
            }

            if (null == funcSum)
            {
                throw new ArgumentNullException("未提供加總的欄位運算 !");
            }

            #endregion

            #region 手工分組 (在原類別之外, 再包一層, 使之成為 MyGroup<T> )

            //註: 會成為以下類似的資料格式 (假設總共 11 筆, 採 3 筆為 一組)
            //GroupSeq  Datas
            //       1  List<T> ---- T
            //                    |- T
            //                    |- T
            //       2  List<T> ---- T
            //                    |- T
            //                    |- T
            //       3  List<T> ---- T
            //                    |- T
            //                    |- T
            //       2  List<T> ---- T
            //                    |- T
            //以上共計將 11 筆資料, 彙整至 4 個群組

            List<MyGroup<T>> groups = new List<MyGroup<T>>();

            int i = 0;
            int groupSeq = 0;
            MyGroup<T> group = new MyGroup<T>();
            foreach (var data in datas)
            {
                if (i % groupCnt == 0)
                {
                    groupSeq++;
                    group = new MyGroup<T>
                    {
                        GroupSeq = groupSeq,
                        Datas = new List<T>(),
                    };
                    group.Datas.Add(data);
                    groups.Add(group);
                }
                else
                {
                    group.Datas.Add(data);
                }

                i++;
            }

            #endregion

            #region 進行 Sum, 並組成回傳的資料

            for (int k = 0; k < groups.Count; k++)
            {
                yield return groups[k].Datas.Sum(funcSum);
                //results.Add(groups[k].Datas.Sum(funcSum));
            }

            #endregion

            //return results;
        }


        /// <summary>
        /// 本擴充方法用於將傳入的集合, 依指定的 群組運算式 / 加總運算式, 進行資料分組, 並回傳加總的結果
        /// 本擴充方法適用於固定組數的方式, 例如: 群組運算式 採用 Id % 3, 代表要分為3組 , 則 
        ///     1, 4, 7, 10 為1組, 
        ///     2, 5, 8, 11 為1組,
        ///     3, 6, 9,    為1組
        /// 使用範例: .GroupByAndSum2<SaleModel>(3, x => x.Id % 3, x => x.Cost);
        /// </summary>
        /// <typeparam name="T">泛型資料型別</typeparam>
        /// <param name="datas">資料集合</param>
        /// <param name="funcGroupBy">GroupBy的實際運算式</param>
        /// <param name="funcSum">Sum的實際運算式</param>
        /// <returns></returns>
        public static IEnumerable<int> GroupByAndSum2<T>(this IEnumerable<T> datas, Func<T, int> funcGroupBy, Func<T, int> funcSum)
        {
            #region 檢查傳入參數

            if (null == funcGroupBy)
            {
                throw new ArgumentNullException("未提供分組的欄位運算 !");
            }

            if (null == funcSum)
            {
                throw new ArgumentNullException("未提供加總的欄位運算 !");
            }

            #endregion

            #region 分組並取得各組內部加總的值

            var groupSum = datas.GroupBy(funcGroupBy, (id, items) => new
            {
                Key = id,
                Count = items.Count(),
                Sum = items.Sum(funcSum),
            });

            #endregion

            #region 回傳結果

            return groupSum.Select(x => x.Sum);

            #endregion
        }

    }
}
