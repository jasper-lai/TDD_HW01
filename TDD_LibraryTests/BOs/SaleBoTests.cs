using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDD_Library.BOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpectedObjects;
using FluentAssertions;
using TDD_Library.Models;
using TDD_Library.Modules;

namespace TDD_Library.BOs.Tests
{
    [TestClass()]
    public class SaleBoTests
    {
        private List<SaleModel> _datas = new List<SaleModel>();

        #region TestContext

        private TestContext testContextInstance;

        /// <summary>
        ///取得或設定提供目前測試回合的相關資訊與功能的測試內容。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #endregion

        #region Method Initialize and Cleanup

        [TestInitialize()]
        public void MethodInitialize()
        {
            _datas = DataMaker.MakeData();
        }

        [TestCleanup()]
        public void MethodCleanup()
        {
            _datas.Clear();
        }

        #endregion

        /// <summary>
        /// GetSumForCostTest_3筆1組_應回傳_6_15_24_21
        /// </summary>
        /// <remarks>
        /// ExpectedObjects
        /// </remarks>
        [TestMethod()]
        public void GetSumForCostTest_3筆1組_應回傳_6_15_24_21()
        {
            List<int> expected = new List<int>() {6, 15, 24, 21};

            //List<int> actual = _datas.GroupByAndSum(3, x => x.Cost).ToList();
            SaleBo target = new SaleBo();
            List<int> actual = target.GetSumForCost(_datas, 3);

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        /// <summary>
        /// GetSumForRevenueTest_4筆1組_應回傳_50_66_60
        /// </summary>
        /// <remarks>
        /// ExpectedObjects
        /// </remarks>
        [TestMethod()]
        public void GetSumForRevenueTest_4筆1組_應回傳_50_66_60()
        {
            List<int> expected = new List<int>() { 50, 66, 60 };

            //List<int> actual = _datas.GroupByAndSum(4, x => x.Revenue).ToList();
            SaleBo target = new SaleBo();
            List<int> actual = target.GetSumForRevenue(_datas, 4);

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        /// <summary>
        /// GroupAndSumTest_筆數為負值_回傳例外狀況_ArgumentOutOfRangeException
        /// </summary>
        /// <remarks>
        /// FluentAssertions
        /// </remarks>
        [TestMethod()]
        public void GroupAndSumTest_筆數為負值_回傳例外狀況_ArgumentOutOfRangeException()
        {
            List<int> expected = new List<int>() { 50, 66, 60 };
            
            //筆數為負值
            Func<List<int>> func = () => _datas.GroupByAndSum(-1, x => x.Revenue).ToList();
            Action act = () => func();

            act.ShouldThrow<ArgumentOutOfRangeException>();
        }

        /// <summary>
        /// GroupAndSumTest_未傳加總運算_回傳例外狀況_ArgumentNullException
        /// </summary>
        /// <remarks>
        /// FluentAssertions
        /// </remarks>
        [TestMethod()]
        public void GroupAndSumTest_未傳加總運算_回傳例外狀況_ArgumentNullException()
        {
            List<int> expected = new List<int>() { 50, 66, 60 };

            //筆數為負值
            Func<List<int>> func = () => _datas.GroupByAndSum(4, null).ToList();
            Action act = () => func();

            act.ShouldThrow<ArgumentNullException>();
        }

    }
}