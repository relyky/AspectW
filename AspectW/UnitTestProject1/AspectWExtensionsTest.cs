using NCCUCS.AspectW;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    
    
    /// <summary>
    ///這是 AspectWExtensionsTest 的測試類別，應該包含
    ///所有 AspectWExtensionsTest 單元測試
    ///</summary>
    [TestClass()]
    public class AspectWExtensionsTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///取得或設定提供目前測試回合的相關資訊與功能
        ///的測試內容。
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

        #region 其他測試屬性
        // 
        //您可以在撰寫測試時，使用下列的其他屬性:
        //
        //在執行類別中的第一項測試之前，先使用 ClassInitialize 執行程式碼
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //在執行類別中的所有測試之後，使用 ClassCleanup 執行程式碼
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //在執行每一項測試之前，先使用 TestInitialize 執行程式碼
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //在執行每一項測試之後，使用 TestCleanup 執行程式碼
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///Ignore 的測試
        ///</summary>
        [TestMethod()]
        public void IgnoreTest()
        {
            try
            {
                AspectW.Define
                    .Ignore()
                    .Do(() =>
                        {
                            throw new ApplicationException("FAIL!");
                        });
            }
            catch
            {
                Assert.Fail("不該有任何 exception 送到這來！");
            }

            // success
        }

        /// <summary>
        ///RetryOnce 的測試
        ///</summary>
        [TestMethod()]
        public void RetryOnceTest()
        {
            // monitor status
            DateTime? begin = null; //  DateTime.Now;
            DateTime? end = null; // DateTime.Now;
            DateTime? fail = null;
            int failCount = 0; // 
            bool success = false;

            //## testing logic 
            bool makeFail = true;
            DateTime beginOutside = DateTime.Now;
            AspectW.Define
                .RetryOnce(1000) // wait 1 second when fail.
                .Do(() =>
                {
                    begin = DateTime.Now;

                    // make fail once.
                    if (makeFail)
                    {
                        makeFail = false;
                        fail = DateTime.Now;
                        failCount++;
                        throw new ApplicationException("FAIL!");
                    }

                    // monitor status
                    success = true;
                    end = DateTime.Now;
                });

            // monitor status
            TimeSpan duration = end.Value.Subtract(begin.Value);
            TimeSpan durationOutside = end.Value.Subtract(beginOutside);

            Assert.IsTrue((int)durationOutside.TotalSeconds == 1, "執行時間差１秒。"); // 執行時間近似１秒。
            Assert.AreEqual(1, failCount, "Exception 應該要發生１次。");
            Assert.IsTrue(success); // 必定要成功
        }

        /// <summary>
        ///Restore 的測試
        ///</summary>
        [TestMethod()]
        public void RestoreTest()
        {
            FooClass foo = new FooClass();


            //AspectW.Define
            //    .Restore(

            //AspectW aspect = null; // TODO: 初始化為適當值
            //object v = null; // TODO: 初始化為適當值
            //Action<object> setter = null; // TODO: 初始化為適當值
            //AspectW expected = null; // TODO: 初始化為適當值
            //AspectW actual;
            //actual = AspectWExtensions.Restore(aspect, v, setter);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("驗證這個測試方法的正確性。");
        }
    }
}
