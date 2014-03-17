using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//using System.Reflection;
//using System.Threading;
//using System.Collections;
//using System.Text;

namespace NCCUCS.AspectW
{
    partial class AspectWExtensions
    {
        ///// <summary>
        ///// 執行時滑鼠狀態變成“等待中”。
        ///// </summary>
        //[DebuggerStepThrough]
        //public static AspectW WaitCursor(this AspectW aspect, Form target)
        //{
        //    return aspect.OnEnterLeave(() => target.Cursor = Cursors.WaitCursor, () => target.Cursor = Cursors.Default);
        //}

        /// <summary>
        /// 執行時讓滑鼠狀態變成“等待中”；也讓部份功能暫時失能。
        /// </summary>
        [DebuggerStepThrough]
        public static AspectW WaitCursor(this AspectW aspect, Form target, params Control[] controls)
        {
            return aspect.OnEnterLeave(
                () => { 
                        target.Cursor = Cursors.WaitCursor; 
                        foreach(Control c in controls) c.Enabled = false; 
                      },
                () => { 
                        foreach (Control c in controls) c.Enabled = true; 
                        target.Cursor = Cursors.Default; 
                      });
        }

        /// <summary>
        /// 再次確認意圖，是否真要執行？
        /// </summary>
        [DebuggerStepThrough]
        public static AspectW ReconfirmIntent(this AspectW aspect)
        {
            //## 再確認是否真要執行。
            return aspect.WhenTrue(DialogResult.Yes == MessageBox.Show("再確認要執行嗎？", "再確認一次", MessageBoxButtons.YesNo, MessageBoxIcon.Information));

            //string message = "再確認要執行嗎？";
            //DialogResult dlo = MessageBox.Show(message, "再確認一次", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //return aspect.WhenTrue(dlo == DialogResult.Yes);
        }

        [DebuggerStepThrough]
        public static AspectW Validate(this AspectW aspect, bool checkResult, string errorMessage)
        {
            return aspect.WhenTrue(Validate(checkResult, errorMessage));
        }

        [DebuggerStepThrough]
        public static AspectW ValidateNonNull(this AspectW aspect, object[] args, string errorMessage)
        {
            return aspect.WhenTrue(MustBeNonNull(args, errorMessage));
        }

        ///// <summary>
        ///// 此功能屬 validation 的其中一類，考慮移除。 
        ///// </summary>
        //[DebuggerStepThrough]
        //public static AspectW MustBeNonNull(this AspectW aspect, params object[] args)
        //{
        //    return aspect.Combine((work) =>
        //    {
        //        for (int i = 0; i < args.Length; i++)
        //        {
        //            if (args[i] == null)
        //                throw new ArgumentNullException(string.Format("Parameter at index {0} is null!", i));
        //        }
        //
        //        work();
        //    });
        //}

        ///// <summary>
        ///// 此功能屬 validation 的其中一類，考慮移除。 
        ///// </summary>
        //[DebuggerStepThrough]
        //public static AspectW MustBeNonDefault<T>(this AspectW aspect, params T[] args)
        //    where T : IComparable
        //{
        //    return aspect.Combine((work) =>
        //    {
        //        T defaultValue = default(T);
        //        for (int i = 0; i < args.Length; i++)
        //        {
        //            T arg = args[i];
        //            if (arg == null || arg.Equals(defaultValue))
        //                throw new ArgumentException(string.Format("Parameter at index {0} is null or default!", i));
        //        }
        //
        //        work();
        //    });
        //}

        /// 有不可預期的狀況 且 應該不須要。
        //[DebuggerStepThrough]
        //public static AspectW MustBeNonDefault(this AspectW aspect, params object[] args)
        //{
        //    return aspect.Combine((work) =>
        //    {
        //        for (int i = 0; i < args.Length; i++)
        //        {
        //            if (args[i] == null)
        //                throw new ArgumentNullException(string.Format("Parameter at index {0} is null!", i));
        //
        //            object defaultValue = Activator.CreateInstance(args[i].GetType());
        //            if (args[i] == defaultValue)
        //                throw new ArgumentException(string.Format("Parameter at index {0} is default!", i));
        //        }
        //
        //        work();
        //    });
        //}

        [DebuggerStepThrough]
        public static bool Validate(bool checkResult, string errorMessage)
        {
            if (!checkResult)
                MessageBox.Show(errorMessage, "驗證失敗!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            return checkResult;
        }

        /// <summary>
        /// 此功能屬 validation 的其中一類，考慮移除。 
        /// </summary>
        [DebuggerStepThrough]
        public static bool MustBeNonNull(object[] args, string errorMessage)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == null)
                {
                    MessageBox.Show(errorMessage, "驗證失敗!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }
            }

            return true;
            //throw new ArgumentNullException(string.Format("Parameter at index {0} is null!", i));
        }

        //private bool ValidateInt(int i, int min, int max, string message)
        //{
        //    bool condition = min <= i && i <= max;
        //    if (!condition)
        //        MessageBox.Show(message);
        //
        //    return condition;
        //}

        /// <summary>
        /// 用於顯示進程
        /// </summary>
        [DebuggerStepThrough]
        public static void DoStepByStep<TCursor>(this AspectW aspect, IEnumerable<TCursor> entities, IShowProgress progressForm, Action<TCursor> work)
        {
            try
            {
                progressForm.Init((float)entities.Count());
                progressForm.ShowProgress();
                foreach (var c in entities)
                {
                    // to "Do" work for currnet entity.
                    aspect.Do(() => work(c));

                    // increse and show progress
                    progressForm.Step();
                    progressForm.ShowProgress();
                }
            }
            finally
            {
                progressForm.Finish();
            }            
        }

    }

    /// <summary>
    /// 或直接指定 ProgressForm
    /// </summary>
    public interface IShowProgress
    {
        //float progress { get; }
        //float progressMax { get; }
        void Init(float max);
        void Step();
        void ShowProgress();
        void Finish();
    }

    //public interface ILogger
    //{
    //
    //}

}
