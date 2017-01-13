using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDD_Library.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpectedObjects;
using FluentAssertions;
using TDD_Library.Models;

namespace TDD_Library.Modules.Tests
{
    [TestClass()]
    public class GroupByAndSumExtensionTests
    {

        private List<SaleModel> MakeData()
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

        #region GroupByAndSum

        /// <summary>
        /// GroupAndSumTest_3筆1組_加總Cost欄位_應回傳_6_15_24_21
        /// </summary>
        /// <remarks>
        /// 採用 ExpectedObjects framework 作驗證
        /// </remarks>
        [TestMethod()]
        [TestCategory("GroupByAndSum")]
        public void GroupByAndSumTest_3筆1組_加總Cost欄位_應回傳_6_15_24_21()
        {
            List<int> expected = new List<int>() { 6, 15, 24, 21 };

            List<int> actual = MakeData().GroupByAndSum(3, x => x.Cost).ToList();

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        /// <summary>
        /// GroupAndSumTest_3筆1組_加總Revenue欄位_應回傳_50_66_60
        /// </summary>
        /// <remarks>
        /// 採用 ExpectedObjects framework 作驗證
        /// </remarks>
        [TestMethod()]
        [TestCategory("GroupByAndSum")]
        public void GroupByAndSumTest_3筆1組_加總Revenue欄位_應回傳_50_66_60()
        {
            List<int> expected = new List<int>() { 50, 66, 60 };

            List<int> actual = MakeData().GroupByAndSum(4, x => x.Revenue).ToList();

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        /// <summary>
        /// GroupAndSumTest_筆數為負值_回傳例外狀況_ArgumentOutOfRangeException
        /// </summary>
        /// <remarks>
        /// FluentAssertions
        /// </remarks>
        [TestMethod()]
        [TestCategory("GroupByAndSum")]
        public void GroupByAndSumTest_筆數為負值_回傳例外狀況_ArgumentOutOfRangeException()
        {

            //筆數為負值
            Func<List<int>> func = () => MakeData().GroupByAndSum(-1, x => x.Revenue).ToList();
            Action act = () => func();

            act.ShouldThrow<ArgumentOutOfRangeException>();
        }

        /// <summary>
        /// GroupAndSumTest_未傳加總運算式_回傳例外狀況_ArgumentNullException
        /// </summary>
        /// <remarks>
        /// FluentAssertions
        /// </remarks>
        [TestMethod()]
        [TestCategory("GroupByAndSum")]
        public void GroupByAndSumTest_未傳加總運算式_回傳例外狀況_ArgumentNullException()
        {

            //未傳加總運算式
            Func<List<int>> func = () => MakeData().GroupByAndSum(4, null).ToList();
            Action act = () => func();

            act.ShouldThrow<ArgumentNullException>();
        }

        #endregion

        #region GroupByAndSum2

        /// <summary>
        /// GroupByAndSum2Test_以Id欄位分為3組_加總Cost欄位_應回傳_22_26_18
        /// </summary>
        /// <remarks>
        /// 採用 ExpectedObjects framework 作驗證
        /// </remarks>
        [TestMethod()]
        [TestCategory("GroupByAndSum2")]
        public void GroupByAndSum2Test_以Id欄位分為3組_加總Cost欄位_應回傳_22_26_18()
        {
            //Arrange
            List<int> expected = new List<int>() { 22, 26, 18 };

            //Act
            List<int> actual = MakeData().GroupByAndSum2(x => x.Id % 3, x => x.Cost).ToList();

            //Assert
            expected.ToExpectedObject().ShouldEqual(actual);

        }

        /// <summary>
        /// GroupByAndSum2Test_以Id欄位分為3組_加總Revenue欄位_應回傳_62_66_48
        /// </summary>
        /// <remarks>
        /// 採用 ExpectedObjects framework 作驗證
        /// </remarks>
        [TestMethod()]
        [TestCategory("GroupByAndSum2")]
        public void GroupByAndSum2Test_以Id欄位分為3組_加總Revenue欄位_應回傳_62_66_48()
        {
            //Arrange
            List<int> expected = new List<int>() { 62, 66, 48 };

            //Act
            List<int> actual = MakeData().GroupByAndSum2(x => x.Id % 3, x => x.Revenue).ToList();

            //Assert
            expected.ToExpectedObject().ShouldEqual(actual);

        }

        /// <summary>
        /// GroupByAndSum2Test_以Id欄位分為3組_未給加總運算式_應回傳_ArgumentNullException
        /// </summary>
        /// <remarks>
        /// 採用 FluentAssertions framework 作驗證
        /// </remarks>
        [TestMethod()]
        [TestCategory("GroupByAndSum2")]
        public void GroupByAndSum2Test_以Id欄位分為3組_未給加總運算式_應回傳_ArgumentNullException()
        {

            //未傳加總運算式
            Func<List<int>> func = () => MakeData().GroupByAndSum2(x => x.Id % 3, null).ToList();
            Action act = () => func();

            act.ShouldThrow<ArgumentNullException>();

        }

        #endregion




    }
}