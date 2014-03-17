using NCCUCS.AspectW;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{    
    
    /// <summary>
    ///這是 AspectWTest 的測試類別，應該包含
    ///所有 AspectWTest 單元測試
    ///</summary>
    [TestClass()]
    public class AspectWTest
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

        ///// <summary>
        /////Do 的測試
        /////</summary>
        //[TestMethod()]
        //public void DoTest()
        //{
        //    AspectW target = new AspectW(); // TODO: 初始化為適當值
        //    Action work = null; // TODO: 初始化為適當值
        //    target.Do(work);
        //    Assert.Inconclusive("無法驗證不傳回值的方法。");
        //}

        /// <summary>
        ///Do 的測試
        ///</summary>
        [TestMethod()]
        public void DoTest()
        {
            int a = 3;
            int c = default(int);
            string s = string.Empty;

            AspectW.Define
                .Do(() =>
                    {
                        int b = 4;
                        a = 10;
                        c = FooFunc.Add(a, b);
                        s = "我是字串";
                    });

            Assert.AreEqual(10, a);
            Assert.AreEqual(14, c);
            Assert.AreEqual("我是字串", s);
        }

    }

    #region Testing Resource

    //struct FooStruct : IComparable
    //{
    //    int a;
    //    string b;
    //    decimal c;       
    //}

    class FooClass
    {
        //int a;
        //string b;
        //decimal c;
    }

    class FooFunc
    {
        public static int Add(int a, int b)
        {
            return a + b;
        }
    }

    #endregion
}
