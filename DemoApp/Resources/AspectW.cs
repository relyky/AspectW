using System;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Reflection;

//應用目標是 Catch-And-Retry
//實作方法採用AspectF的精神，但會重寫以支援Catch-And-Retry。
//實作內容有
//  1、catch exception
//  2、retry
//  3、skip/ignore
//  4、restore
//  5、re-schedule/register   (option)
//這是現在的範圍。

namespace NCCUCS.AspectW
{
    public class AspectW
    {
        /// <summary>
        /// Chain of aspects to invoke
        /// </summary>
        internal Action<Action> Chain = null;

        /// <summary>
        /// The acrual work delegate that is finally called
        /// </summary>
        internal Delegate WorkDelegate;

        /// <summary>
        /// Create a composition of function e.g. f(g(x))
        /// </summary>
        /// <param name="newAspectDelegate">A delegate that offers an aspect's behavior. 
        /// It's added into the aspect chain</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        internal AspectW Combine(Action<Action> newAspectDelegate)
        {
            if (this.Chain == null)
            {
                this.Chain = newAspectDelegate;
            }
            else
            {
                Action<Action> existingChain = this.Chain;
                Action<Action> callAnother = (work) => existingChain(() => newAspectDelegate(work));
                this.Chain = callAnother;
            }
            return this;
        }

        /// <summary>
        /// Execute your real code applying the aspects over it
        /// </summary>
        /// <param name="work">The actual code that needs to be run</param>
        [DebuggerStepThrough]
        public void Do(Action work)
        {
            Debug.WriteLine("ON : Do");
            if (this.Chain == null)
            {
                work();
            }
            else
            {
                this.Chain(work);
            }
        }

        /// <summary>
        /// Execute your real code applying aspects over it.
        /// </summary>
        /// <typeparam name="TReturnType"></typeparam>
        /// <param name="work">The actual code that needs to be run</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public TReturnType Return<TReturnType>(Func<TReturnType> work)
        {
            this.WorkDelegate = work;

            if (this.Chain == null)
            {
                return work();
            }
            else
            {
                TReturnType returnValue = default(TReturnType);
                this.Chain(() =>
                {
                    Func<TReturnType> workDelegate = WorkDelegate as Func<TReturnType>;
                    returnValue = workDelegate();
                });
                return returnValue;
            }
        }

        /// <summary>
        /// Handy property to start writing aspects using fluent style
        /// </summary>
        public static AspectW Define
        {
            [DebuggerStepThrough]
            get
            {
                Debug.WriteLine("ON : Define");
                return new AspectW();
            }
        }

        //------------------------

        #region 經考慮，此DoOrRestore指令決定移除。因為較新版的“Restore機制”已不錯，維持方法唯一性，以免變成無謂的複雜化。
        //[DebuggerStepThrough]
        //public void DoOrRestore<T1>(ref T1 o1, Action work)
        //{
        //    // clone / copy
        //    ICloneable c1 = o1 as ICloneable;
        //    T1 b1 = (c1 == null) ? o1 : (T1)c1.Clone();
        //    try
        //    {
        //        // work();
        //        if (this.Chain == null)
        //            work();
        //        else
        //            this.Chain(work);
        //    }
        //    catch
        //    {
        //        // restore
        //        o1 = b1;
        //    }
        //}

        //[DebuggerStepThrough]
        //public void DoOrRestore<T1, T2>(ref T1 o1, ref T2 o2, Action work)
        //{
        //    // clone / copy
        //    ICloneable c1 = o1 as ICloneable;
        //    ICloneable c2 = o2 as ICloneable;
        //    T1 b1 = (c1 == null) ? o1 : (T1)c1.Clone();
        //    T2 b2 = (c2 == null) ? o2 : (T2)c2.Clone();
        //    try
        //    {
        //        // work();
        //        if (this.Chain == null)
        //            work();
        //        else
        //            this.Chain(work);
        //    }
        //    catch
        //    {
        //        // restore
        //        o1 = b1;
        //        o2 = b2;
        //    }
        //}

        //[DebuggerStepThrough]
        //public void DoOrRestore<T1, T2, T3>(ref T1 o1, ref T2 o2, ref T3 o3, Action work)
        //{
        //    // clone / copy
        //    ICloneable c1 = o1 as ICloneable;
        //    ICloneable c2 = o2 as ICloneable;
        //    ICloneable c3 = o3 as ICloneable;
        //    T1 b1 = (c1 == null) ? o1 : (T1)c1.Clone();
        //    T2 b2 = (c2 == null) ? o2 : (T2)c2.Clone();
        //    T3 b3 = (c3 == null) ? o3 : (T3)c3.Clone();
        //    try
        //    {
        //        // work();
        //        if (this.Chain == null)
        //            work();
        //        else
        //            this.Chain(work);
        //    }
        //    catch
        //    {
        //        // restore
        //        o1 = b1;
        //        o2 = b2;
        //        o3 = b3;
        //    }
        //}
        #endregion

    }

    public static partial class AspectWExtensions
    {
        [DebuggerStepThrough]
        public static AspectW OnEnterLeave(this AspectW aspect, Action enterAction, Action leaveAction)
        {
            return aspect.Combine((work) =>
            {
                try
                {
                    if (enterAction != null)
                        enterAction();

                    work();
                }
                finally
                {
                    if (leaveAction != null)
                        leaveAction();
                }
            });
        }

        [DebuggerStepThrough]
        public static AspectW OnEnter(this AspectW aspect, Action enterAction)
        {
            return aspect.Combine((work) =>
            {
                enterAction();
                work();
            });
        }

        [DebuggerStepThrough]
        public static AspectW OnLeave(this AspectW aspect, Action leaveAction)
        {
            return aspect.Combine((work) =>
            {
                try
                {
                    work();
                }
                finally
                {
                    leaveAction();
                }
            });
        }

        [DebuggerStepThrough]
        public static AspectW Trace(this AspectW aspect, Action prefixTrace, Action postfixTrace, Action<Exception> traceException)
        {
            return aspect.Combine((work) =>
            {
                if (prefixTrace != null)
                    prefixTrace();

                try
                {
                    work();
                }
                catch (Exception ex)
                {
                    if (traceException != null)
                        traceException(ex);
                    throw;
                }

                if (postfixTrace != null)
                    postfixTrace();
            });
        }

        [DebuggerStepThrough]
        public static AspectW Trace(this AspectW aspect, Action prefixTrace, Action<TimeSpan> postfixTrace, Action<TimeSpan,Exception> traceException)
        {
            return aspect.Combine((work) =>
            {
                if (prefixTrace != null)
                    prefixTrace();

                TimeSpan howLong;
                DateTime start = DateTime.Now;

                try
                {
                    work();
                }
                catch (Exception ex)
                {
                    howLong = DateTime.Now - start;
                    if (traceException != null)
                        traceException(howLong, ex);

                    throw;
                }

                howLong = DateTime.Now - start;
                if (postfixTrace != null)
                    postfixTrace(howLong);
            });
        }

        [DebuggerStepThrough]
        public static AspectW Trace(this AspectW aspect, Action<DateTime> prefixTrace, Action<DateTime, TimeSpan> postfixTrace, Action<DateTime,TimeSpan,Exception> traceException)
        {
            return aspect.Combine((work) =>
            {
                DateTime start = DateTime.Now;
                if (prefixTrace != null)
                    prefixTrace(start); // start time

                try
                {
                    work();
                }
                catch (Exception ex)
                {
                    DateTime failTime = DateTime.Now;
                    if (traceException != null)
                        traceException(failTime, failTime - start, ex); // fail time, and how-long

                    throw;
                }

                DateTime finish = DateTime.Now;
                if (postfixTrace != null)
                    postfixTrace(finish, finish - start); // finish time, and how-long
            });
        }

        [DebuggerStepThrough]
        public static AspectW Trace(this AspectW aspect, Action prefixTrace, Action postfixTrace)
        {
            return aspect.Combine((work) =>
            {
                prefixTrace();
                work();
                postfixTrace();
            });
        }

        [DebuggerStepThrough]
        public static AspectW Trace(this AspectW aspect, Action prefixTrace, Action<TimeSpan> postfixTrace)
        {
            return aspect.Combine((work) =>
            {
                prefixTrace();
                DateTime start = DateTime.Now;
                work();
                TimeSpan howLong = DateTime.Now - start;
                postfixTrace(howLong);
            });
        }

        [DebuggerStepThrough]
        public static AspectW TraceBegin(this AspectW aspect, Action prefixTrace)
        {
            return aspect.Combine((work) =>
            {
                prefixTrace();
                work();
            });
        }

        [DebuggerStepThrough]
        public static AspectW TraceBegin(this AspectW aspect, Action<string, object[]> prefixTrace, string message, params object[] args)
        {
            return aspect.Combine((work) =>
            {
                prefixTrace(message, args);
                work();
            });
        }

        [DebuggerStepThrough]
        public static AspectW TraceEnd(this AspectW aspect, Action postfixTrace)
        {
            return aspect.Combine((work) =>
            {
                work();
                postfixTrace();
            });
        }

        [DebuggerStepThrough]
        public static AspectW TraceEnd(this AspectW aspect, Action<TimeSpan> postfixTrace)
        {
            return aspect.Combine((work) =>
            {
                DateTime start = DateTime.Now;
                work();
                TimeSpan howLong = DateTime.Now - start;
                postfixTrace(howLong);
            });
        }

        [DebuggerStepThrough]
        public static AspectW TraceEnd(this AspectW aspect, Action<string, object[]> postfixTrace, string message, params object[] args)
        {
            return aspect.Combine((work) =>
            {
                work();
                postfixTrace(message, args);
            });
        }

        [DebuggerStepThrough]
        public static AspectW TraceEnd(this AspectW aspect, Action<string, TimeSpan, object[]> postfixTrace, string message, params object[] args)
        {
            return aspect.Combine((work) =>
            {
                DateTime start = DateTime.Now;
                work();
                TimeSpan howLong = DateTime.Now - start;
                postfixTrace(message, howLong, args);
            });
        }

        #region backup
        //[DebuggerStepThrough]
        //public static AspectW TraceException(this AspectW aspect, Action<Exception> traceException)
        //{
        //    return aspect.Combine((work) =>
        //    {
        //        try
        //        {
        //            work();
        //        }
        //        catch (Exception ex)
        //        {
        //            traceException(ex);
        //            throw;
        //        }
        //    });
        //}
        #endregion

        [DebuggerStepThrough]
        public static AspectW TraceException(this AspectW aspect, Action<Exception> traceException)
        {
            return aspect.TraceException<Exception>(traceException);
        }

        [DebuggerStepThrough]
        public static AspectW TraceException<TException>(this AspectW aspect, Action<Exception> traceException)
            where TException : Exception
        {
            return aspect.Combine((work) =>
            {
                try
                {
                    work();
                }
                catch (TException ex)
                {
                    traceException(ex);
                    throw;
                }
            });
        }

        /// <summary>
        /// conditional to run.
        /// </summary>
        [DebuggerStepThrough]
        public static AspectW WhenTrue(this AspectW aspect, bool condition)
        {
            return aspect.Combine((work) =>
            {
                if (!condition)
                    return;
                work();
            });
        }

        /// <summary>
        /// conditional to run.
        /// </summary>
        [DebuggerStepThrough]
        public static AspectW WhenTrue(this AspectW aspect, params bool[] conditions)
        {
            return aspect.Combine((work) =>
            {
                foreach (bool cond in conditions)
                    if (!cond)
                        return;
                work();
            });
        }

        /// <summary>
        /// continue testing until the result is true, and then execute work().
        /// </summary>
        [DebuggerStepThrough]
        public static AspectW Until(this AspectW aspect, Func<bool> testing)
        {
            return aspect.Combine((work) =>
            {
                while (!testing()) ;

                work();
            });
        }

        ///// <summary>
        ///// 執行前做些什麼。※考慮移除中。因找不到應用又不易使用。必需另外先定義共通的 customized delegate 才能正常運作。
        ///// </summary>
        //[DebuggerStepThrough]
        //public static AspectW BeforeProceed(this AspectW aspect, Delegate func, params object[] args)
        //{
        //    return aspect.Combine((work) =>
        //    {
        //        func.DynamicInvoke(args);
        //        work();
        //    });
        //}

        ///// <summary>
        ///// 執行後做些什麼。※考慮移除中。因找不到應用又不易使用。必需另外先定義共通的 customized delegate 才能正常運作。
        ///// </summary>
        //[DebuggerStepThrough]
        //public static AspectW AfterProceed(this AspectW aspect, Delegate func, params object[] args)
        //{
        //    return aspect.Combine((work) =>
        //    {
        //        work();
        //        func.DynamicInvoke(args);
        //    });
        //}

        /// <summary>
        /// retry once when fail.
        /// </summary>
        [DebuggerStepThrough]
        public static AspectW RetryOnce(this AspectW aspect, int retryDuration)
        {
            return aspect.Combine((work) =>
            {
                try
                {
                    work();
                }
                catch
                {
                    Thread.Sleep(retryDuration);
                    work();
                }
            });
        }

        /// <summary>
        /// retry once when fail.
        /// </summary>
        [DebuggerStepThrough]
        public static AspectW RetryOnce(this AspectW aspect, int retryDuration, Action reassignParam)
        {
            return aspect.Combine((work) =>
            {
                try
                {
                    work();
                }
                catch
                {
                    Thread.Sleep(retryDuration);
                    reassignParam();
                    work();
                }
            });
        }

        #region remark，因已變成冗餘設計。
        ///// <summary>
        ///// retry once when fail.
        ///// </summary>
        //[DebuggerStepThrough]
        //public static AspectW RetryOnce(this AspectW aspect, int retryDuration, params RestoreWhenFail[] restoreArgs)
        //{
        //    return aspect.Combine((work) =>
        //    {
        //        try
        //        {
        //            work();
        //        }
        //        catch
        //        {
        //            Thread.Sleep(retryDuration);
        //
        //            //# rsetore
        //            foreach (RestoreWhenFail r in restoreArgs)
        //                r.Restore();
        //
        //            work();
        //        }
        //    });
        //}
        #endregion

        #region backup
        ///// <summary>
        ///// retry N times when fail.
        ///// </summary>
        //[DebuggerStepThrough]
        //public static AspectW Retry(this AspectW aspect, int retryDuration, int retryCount)
        //{
        //    return aspect.Combine((work) =>
        //    {
        //        for (; ; )
        //        {
        //            try
        //            {
        //                work();
        //                break; // success & leave
        //            }
        //            catch
        //            {
        //                if (retryCount-- > 0) // decide to retry or not.
        //                {
        //                    Thread.Sleep(retryDuration); // wait for a short time and then re-exec.
        //                    continue; // retry
        //                }
        //                throw; // fail & leave
        //            }
        //        } // end of : for (; ; )
        //    });
        //}
        #endregion backup

        /// <summary>
        /// retry N times when fail.
        /// </summary>
        [DebuggerStepThrough]
        public static AspectW Retry(this AspectW aspect, int retryDuration, int retryCount)
        {
            return Retry<Exception>(aspect, retryDuration, retryCount);
        }

        /// <summary>
        /// retry N times when fail.
        /// </summary>
        [DebuggerStepThrough]
        public static AspectW Retry<TException>(this AspectW aspect, int retryDuration, int retryCount)
            where TException : Exception
        {
            return aspect.Combine((work) =>
            {
                for (; ; )
                {
                    try
                    {
                        work();
                        break; // success & leave
                    }
                    catch(TException)
                    {
                        if (retryCount-- > 0) // decide to retry or not.
                        {
                            Thread.Sleep(retryDuration); // wait for a short time and then re-exec.
                            continue; // retry
                        }
                        throw; // fail & leave
                    }
                } // end of : for (; ; )
            });
        }

        [DebuggerStepThrough]
        public static AspectW Retry<TException>(this AspectW aspect, int retryDuration, int retryCount
            , Action<TException> recoveryAction)
            where TException : Exception
        {
            return Retry<TException>(aspect, retryDuration, retryCount, recoveryAction, (Action<TException>)null);
        }

        [DebuggerStepThrough]
        public static AspectW Retry<TException>(this AspectW aspect, int retryDuration, int retryCount
            , Action<TException> recoveryAction
            , Action<TException> failAction)
            where TException : Exception
        {
            return aspect.Combine((work) =>
            {
                for (; ; )
                {
                    try
                    {
                        work();
                        break; // success & leave
                    }
                    catch (TException ex)
                    {
                        if (retryCount-- > 0) // decide to retry.
                        {
                            Thread.Sleep(retryDuration); // wait for a short time and then re-execute.

                            // recovery 只有在想要 retry時才有意義。
                            if (recoveryAction != null)
                                recoveryAction(ex);

                            continue; // retry
                        }
                        else if (failAction != null) // had retried, but still fail!
                        {
                            failAction(ex); // fail handling
                            throw; // fail & leave
                        }
                        else // no fail handler
                        {
                            throw; // fail & leave
                        }
                    }
                } // end of : for (; ; )
            });
        }
        
        [DebuggerStepThrough]
        public static AspectW RetryX<TException>(this AspectW aspect, int retryDuration, int retryCount
            , Action<TException> recoveryAction
            , Func<Exception, ExceptionProceedMethod> failHandler)
            where TException : Exception
        {
            return aspect.Combine((work) =>
            {
                for (; ; )
                {
                    try
                    {
                        work();
                        break; // success & leave
                    }
                    catch(TException ex)
                    {
                        if (retryCount-- > 0) // decide to retry.
                        {
                            Thread.Sleep(retryDuration); // wait for a short time and then re-exec.

                            // recovery 只有在想要 retry時才有意義。
                            if (recoveryAction != null)
                                recoveryAction(ex); 

                            continue; // retry
                        }
                        else if (failHandler != null) // had retried, but still fail!
                        {
                            switch (failHandler(ex))
                            {
                                case ExceptionProceedMethod.Ignore: // just do nothing.
                                    break;
                                case ExceptionProceedMethod.Retry:
                                    continue; // jump to for;
                                //case ExceptionProceedMethod.ThrowOut:
                                default:
                                    throw; // throw out exception.
                            }
                        }
                        else // no fail handler
                        {
                            throw; // fail & leave
                        }
                    }
                } // end of : for (; ; )
            });
        }

        /// <summary>
        /// parameterized retry
        /// </summary>
        [DebuggerStepThrough]
        public static AspectW RetryParam(this AspectW aspect, int retryDuration, params Action[] reassignParam)
        {
            return RetryParam<Exception>(aspect, retryDuration, reassignParam);
        }

        /// <summary>
        /// parameterized retry
        /// </summary>
        [DebuggerStepThrough]
        public static AspectW RetryParam<TException>(this AspectW aspect, int retryDuration, params Action[] reassignParam)
            where TException : Exception
        {
            return aspect.Combine((work) =>
            {
                for (int idx = 0; idx <= reassignParam.Length; idx++)
                {
                    try
                    {
                        work();
                        break; // success & leave
                    }
                    catch(TException)
                    {
                        if (idx < reassignParam.Length) // decide to retry or not.
                        {
                            Thread.Sleep(retryDuration); // wait for a short time and then re-exec.
                            reassignParam[idx](); // reassign parameters.
                            continue; // retry
                        }

                        throw; // fail & leave
                    }
                } // end of : for
            });
        }

        #region backup
        //[DebuggerStepThrough]
        //public static AspectW HandleException(this AspectW aspect, Func<Exception, ExceptionProceedMethod> exceptionHandler)
        //{
        //    return aspect.Combine((work) =>
        //    {
        //        for (; ; )
        //        {
        //            try
        //            {
        //                work();
        //                break; // success & leave
        //            }
        //            catch (Exception ex)
        //            {
        //                switch (exceptionHandler(ex))
        //                {
        //                    case ExceptionProceedMethod.Ignore: // just do nothing.
        //                        break;
        //                    case ExceptionProceedMethod.Retry:
        //                        continue; // jump to for;
        //                    //case ExceptionProceedMethod.ThrowOut:
        //                    default:
        //                        throw; // throw exception out.
        //                }
        //            }
        //        } // end of : for (; ; )
        //    });
        //}
        #endregion

        [DebuggerStepThrough]
        public static AspectW HandleException(this AspectW aspect, Func<Exception, ExceptionProceedMethod> failHandler)
        {
            return HandleException<Exception>(aspect, failHandler);
        }

        [DebuggerStepThrough]
        public static AspectW HandleException<TException>(this AspectW aspect, Func<TException, ExceptionProceedMethod> failHandler)
            where TException : Exception
        {
            return aspect.Combine((work) =>
            {
                for (; ; )
                {
                    try
                    {
                        work();
                        break; // success & leave
                    }
                    catch (TException ex)
                    {
                        switch (failHandler(ex))
                        {
                            case ExceptionProceedMethod.Ignore: // just do nothing.
                                break;
                            case ExceptionProceedMethod.Retry:
                                continue; // jump to for;
                            //case FailProceedMethod.ThrowOut:
                            default:
                                throw; // throw exception out.
                        }
                    }
                } // end of : for (; ; )
            });
        }
        
        //[DebuggerStepThrough]
        //public static void Retry
        //    (int retryDuration
        //    , int retryCount
        //    , Action<Exception> errorHandler
        //    , Action<IEnumerable<Exception>> retryFailed
        //    , Action work
        //    , ILogger logger)
        //{
        //    List<Exception> errors = null;
        //    do
        //    {
        //        try
        //        {
        //            work();
        //            return;
        //        }
        //        catch (Exception x)
        //        {
        //            if (null == errors)
        //                errors = new List<Exception>();
        //            errors.Add(x);
        //            logger.LogException(x);
        //            errorHandler(x);
        //            System.Threading.Thread.Sleep(retryDuration);
        //        }
        //    } while (retryCount-- > 0);
        //    retryFailed(errors);
        //}

        [DebuggerStepThrough]
        public static AspectW Ignore(this AspectW aspect)
        {
            return aspect.Combine((work) =>
            {
                try
                {
                    work();
                }
                catch {}
            });
        }

        [DebuggerStepThrough]
        public static AspectW Ignore<TException>(this AspectW aspect) 
            where TException : Exception
        {
            return aspect.Combine((work) =>
            {
                try
                {
                    work();
                }
                catch(TException)
                { }
            });
        }

        [DebuggerStepThrough]
        public static AspectW Ignore<TException1, TException2>(this AspectW aspect) 
            where TException1 : Exception
            where TException2 : Exception
        {
            return aspect.Combine((work) =>
            {
                try
                {
                    work();
                }
                catch (TException1)
                { }
                catch (TException2)
                { }
            });
        }

        [DebuggerStepThrough]
        public static AspectW Ignore<TException1, TException2, TException3>(this AspectW aspect)
            where TException1 : Exception
            where TException2 : Exception
            where TException3 : Exception
        {
            return aspect.Combine((work) =>
            {
                try
                {
                    work();
                }
                catch (TException1)
                { }
                catch (TException2)
                { }
                catch (TException3)
                { }
            });
        }

        #region ## remark：因當有 exception 且打算 retry 多次時，會有多次冗餘的 clone 動作，故取消此設計。backup 只需一次即可。

        ///// <summary>
        ///// restore when fail!.
        ///// </summary>
        //[DebuggerStepThrough]
        //public static AspectW Restore(this AspectW aspect, object v, Action<object> setter)
        //{
        //    Debug.WriteLine("ON : Restore");
        //    return aspect.Combine((work) =>
        //    {
        //        RestoreWhenFail r = null;
        //        try
        //        {
        //            Debug.WriteLine("\t Restore->init...");
        //            r = new RestoreWhenFail(v, setter);
        //            Debug.WriteLine("\t Restore->work()...");
        //            work();
        //        }
        //        catch
        //        {
        //            Debug.WriteLine("\t Restore->restore...");
        //            r.Restore(); // rsetore
        //            throw;
        //        }
        //    });
        //}

        ///// <summary>
        ///// restore when fail!.
        ///// </summary>
        //[DebuggerStepThrough]
        //public static AspectW Restore(this AspectW aspect, object v1, Action<object> setter1, object v2, Action<object> setter2)
        //{
        //    Debug.WriteLine("ON : Restore");
        //    return aspect.Combine((work) =>
        //    {
        //        RestoreWhenFail r1 = null, r2 = null;
        //        try
        //        {
        //            Debug.WriteLine("\t Restore->init...");
        //            r1 = new RestoreWhenFail(v1, setter1);
        //            r2 = new RestoreWhenFail(v2, setter2);
        //            Debug.WriteLine("\t Restore->work()...");
        //            work();
        //        }
        //        catch
        //        {
        //            Debug.WriteLine("\t Restore->restore...");
        //            r1.Restore(); // rsetore
        //            r2.Restore();
        //            throw;
        //        }
        //    });
        //}

        ///// <summary>
        ///// restore when fail!.
        ///// </summary>
        //[DebuggerStepThrough]
        //public static AspectW Restore(this AspectW aspect, object v1, Action<object> setter1, object v2, Action<object> setter2, object v3, Action<object> setter3)
        //{
        //    Debug.WriteLine("ON : Restore");
        //    return aspect.Combine((work) =>
        //    {
        //        RestoreWhenFail r1 = null, r2 = null, r3 = null;
        //        try
        //        {
        //            Debug.WriteLine("\t Restore->init...");
        //            r1 = new RestoreWhenFail(v1, setter1);
        //            r2 = new RestoreWhenFail(v2, setter2);
        //            r3 = new RestoreWhenFail(v2, setter3);
        //            Debug.WriteLine("\t Restore->work()");
        //            work();
        //        }
        //        catch
        //        {
        //            Debug.WriteLine("\t Restore->restore");
        //            r1.Restore(); // rsetore
        //            r2.Restore();
        //            r3.Restore();
        //            throw;
        //        }
        //    });
        //}

        #endregion

        /// <summary>
        /// restore when fail!.
        /// </summary>
        [DebuggerStepThrough]
        public static AspectW Restore(this AspectW aspect, params RestoreWhenFail[] restoreArgs)
        {
            Debug.WriteLine("ON : Restore");
            return aspect.Combine((work) =>
            {
                try
                {
                    Debug.WriteLine("\t Restore->work()");
                    work();
                }
                catch
                {
                    //# rsetore
                    Debug.WriteLine("\t Restore->restore");
                    foreach (RestoreWhenFail r in restoreArgs)
                        r.Restore();

                    //# still throw exception.
                    throw;
                }
            });
        }

        ///// <summary>
        ///// restore when fail!.
        ///// </summary>
        ///// <remarks>
        ///// 錯誤：無法在匿名方法、Lambda 運算式、查詢運算式內部使用 ref 或 out 參數。
        ///// </remarks>
        //[DebuggerStepThrough]
        //public static AspectW Restore2<T>(this AspectW aspect, ref T v)
        //{
        //    return aspect.Combine((work) =>
        //    {
        //        //RestoreWhenFail r = null;
        //        T c = default(T);
        //        try
        //        {
        //            // backup 
        //            Debug.WriteLine("\tRestore2->backup...");
        //            c = v.GetType().IsSerializable ? v.DeepClone()
        //              : v is ICloneable ? (T)((ICloneable)v).Clone()
        //              : v;
        //
        //            work();
        //        }
        //        catch
        //        {
        //            // restore 
        //            v = c;
        //            throw;
        //        }
        //    });
        //}

    }

    /// <summary>
    /// 定義 exception 處理方法
    /// </summary>
    public enum ExceptionProceedMethod
    {
        ThrowOut = 0, // default
        Retry,
        Ignore
    }

    //interface IDeepCloneable<T>
    //{
    //    T DeepClone();
    //}

    /// <summary>
    /// 明確指定做 Deep Clone。
    /// </summary>
    interface IDeepCloneable
    {
        object DeepClone();
    }

    /// <summary>
    /// helper class for [AspectW.Restore()].
    /// backup and restore when fail!
    /// </summary>
    public class RestoreWhenFail
    {
        protected object clone; // backup instance
        protected Action<object> setter; // to restore

        protected RestoreWhenFail() // 鷹架碼 
        { } 

        public RestoreWhenFail(object v, Action<object> setter)
        {
            //// backup value
            //clone = Backup(v);
            Debug.WriteLine(string.Format("RestoreWhenFail ctor <=> v:{0}", v));

            // backup value
            if (v.GetType().IsSerializable) // && !v.GetType().IsArray)
                clone = v.DeepClone(); // 'deep clone'.
            else if (v is IDeepCloneable)
                clone = ((IDeepCloneable)v).DeepClone(); // 'deep clone'.
            else if (v is ICloneable)
                clone = ((ICloneable)v).Clone(); // maybe 'shaddow clone' or 'deep clone'.
            else
                clone = v; // maybe 'copy by value' or 'copy by reference'.

            // [restore setter] is used to restore when fail!
            this.setter = setter;
        }

        public virtual void Restore()
        {
            setter(clone);
        }

        //private static object Backup(object v)
        //{
        //    if (v.GetType().IsSerializable)
        //        return v.DeepClone(); // 'deep clone'.
        //    else if (v is IDeepCloneable)
        //        return ((IDeepCloneable)v).DeepClone(); // 'deep clone'.
        //    else if (v is ICloneable)
        //        return ((ICloneable)v).Clone(); // maybe 'shaddow clone' or 'deep clone'.
        //    //else
        //    return v; // maybe 'copy by value' or 'copy by reference'.
        //}
    }
    
    /// <summary>
    /// helper class for [AspectW.Restore()].
    /// </summary>
    public sealed class RestoreArrayWhenFail : RestoreWhenFail
    {
        public RestoreArrayWhenFail(Array v, Action<object> setter)
        {
            // backup value
            clone = CloneArray(v);

            // restore setter
            this.setter = setter;
        }

        private static Array CloneArray(Array src)
        {
            // backup value
            Array clone = (Array)src.Clone(); // just member-wise clone. Orz.

            // do "clone" again.
            // clone element by element if it can. 
            for (long i = 0; i < src.LongLength; i++)
            {
                object element = src.GetValue(i);
                if (element.GetType().IsSerializable)
                    clone.SetValue(element.DeepClone(), i); // deep clone.
                else if (element is IDeepCloneable)
                    clone.SetValue(((IDeepCloneable)element).DeepClone(), i); // 'deep clone'.
                else if (element is Array)
                    clone.SetValue(CloneArray((Array)element), i); // to clone multi-dimension array. 
                else if (element is ICloneable)
                    clone.SetValue(((ICloneable)element).Clone(), i); // clone element.
            }

            return clone;
        }
    }

    #region REF - 沒在用 remark。
    ///// <example>
    ///// int a = 33;
    ///// REF<int> ra = new REF<int>(() => a, (v) => a = v); // 模擬ref
    ///// Console.WriteLine(string.Format("a = {0}", a)); // 33
    ///// Console.WriteLine(string.Format("ra = {0}", ra.Value)); // 33
    ///// 
    ///// ra.Value = 22;
    ///// Console.WriteLine(string.Format("a = {0}", a)); // 22
    ///// Console.WriteLine(string.Format("ra = {0}", ra.Value)); // 22
    ///// </example>
    //internal sealed class REF<T>
    //{
    //    private Func<T> getter;
    //    private Action<T> setter;
    //
    //    public REF(Func<T> getter, Action<T> setter)
    //    {
    //        this.setter = setter;
    //        this.getter = getter;
    //    }
    //
    //    public T Value
    //    {
    //        get { return getter(); }
    //        set { setter(value); }
    //    }
    //}
    #endregion

    /// <summary>
    /// how to deep-clone instance.
    /// </summary>
    /// <seealso cref="http://stackoverflow.com/questions/129389/how-do-you-do-a-deep-copy-an-object-in-net-c-specifically"/>
    public static class ExtensionMethods
    {
        /// <remarks>
        /// which extends any class that's been marked as [Serializable] with a DeepClone method
        /// </remarks>
        internal static T DeepClone<T>(this T obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                return (T) formatter.Deserialize(ms);
            }
        }

        // 失敗！不可用。
        //public static REF<T> MakeREF<T>(this T obj)
        //{
        //    return new REF<T>(() => obj, (v) => obj = v);
        //}

        // 失敗！不可用。
        //public static REF MakeREF<T>(this T obj)
        //{
        //    return new REF(() => obj, (v) => obj = (T)v);
        //}

        /////<remark>
        ///// this way is a few times faster than BinarySerialization AND this does not require the [Serializable] attribute
        ///// 但是要用到 Reflection 所以放棄不用。
        /////</remark>
        //public static object CopyObject(object input)
        //{
        //    if (input != null)
        //    {
        //        object result = Activator.CreateInstance(input.GetType());
        //        foreach (FieldInfo field in input.GetType().GetFields(Consts.AppConsts.FullBindingList))
        //        {
        //            if (field.FieldType.GetInterface("IList", false) == null)
        //            {
        //                field.SetValue(result, field.GetValue(input));
        //            }
        //            else
        //            {
        //                IList listObject = (IList)field.GetValue(result);
        //                if (listObject != null)
        //                {
        //                    foreach (object item in ((IList)field.GetValue(input)))
        //                    {
        //                        listObject.Add(CopyObject(item));
        //                    }
        //                }
        //            }
        //        }
        //        return result;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }

}
