using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace conApp_StructLab
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, object> cloneList = new Dictionary<string, object>();
            int a = 33;
            try
            {
                int ca = a; // a.Clone(); 
                cloneList.Add("a", a);

                // change a
                a = 44;
                //throw new ApplicationException("FAIL!");
            }
            catch
            {
                a = (int)cloneList["a"];
            }

            Console.WriteLine(a);

            //======================================
            
            int b = 27;
            RestoreWhenFail<int>(ref b, () =>
                {
                    b = 45;
                    throw new ApplicationException("FAIL!");
                });

            Console.WriteLine(b);

            //======================================

            UnsafeTest ut = new UnsafeTest();
            int aa = 76;
            string ss = "I am string";
            unsafe
            {
                ut.Add(aa, &aa, aa.GetType());
            }

            aa = 46;

            ut.Restore();

            Console.WriteLine(aa);

            //======================================

            string x = "hello";
            Ref<string> rx = new Ref<string>(() => x, (v) => { x = v; });
            rx.Value = "goodbye";
            Console.WriteLine(x); // goodbye
            //return rx;

            string y = "hello2";
            RestoreWhenFail<string> ry = new RestoreWhenFail<string>(y, (v) => y = v);
            y = "goodbye2";
            ry.Restore();
            Console.WriteLine(y); // goodbye

            string z = "hello3";
            RestoreWhenFail rz = new RestoreWhenFail(z, (v) => z = (string)v);
            z = "goodbye3";
            rz.Restore();
            Console.WriteLine(z); // goodbye

            int zz = 333;
            RestoreWhenFail rzz = new RestoreWhenFail(zz, (v) => zz = (int)v);
            zz = 444;
            rzz.Restore();
            Console.WriteLine(zz); // goodbye

            //AspectW.Define
            //    .Restore(new RestoreWhenFail<string>(() => x, (v) => x = v))
            //    .Restore(new RestoreWhenFail(x, (v) => x = (string)v)
            //    .RestoreWhenFail(x, (v) => x = (string)v)
            //    .Do(() =>
            //        {
            //
            //        });
                   
        }

        //======================================

        static void RestoreWhenFail<T>(ref T inst, Action work)
        {
            Dictionary<string, object> cloneList = new Dictionary<string, object>();
            try
            {
                T cloneInst = inst; // a.Clone(); 
                cloneList.Add("cloneInst", cloneInst);

                work();
            }
            catch
            {
                inst = (T)cloneList["cloneInst"];
            }
        }

        //======================================

        //static void RestoreRegister<T>(ref T inst)
        //{
        //    T cloneInst = inst; // a.Clone(); 
        //    s_cloneList.Add("cloneInst", cloneInst);
        //}

        //static void DoOrRestore(Action work)
        //{
        //    try
        //    {
        //        work();
        //    }
        //    catch
        //    {
        //        a = (int)cloneList["cloneInst"];
        //    }
        //}

        static Dictionary<string, object> s_cloneList = new Dictionary<string, object>();

        unsafe class UnsafeTest
        {
            struct ObjRefElement
            {
                public void* ptr;
                public Type type;
                public object clone;
            }

            ObjRefElement[] objList = new ObjRefElement[4];

            //int[] ar = new int[4];
            //void*[] ars = new void*[4];

            public void Add(object _obj, void* _ptr, Type _type)
            {
                objList[0] = new ObjRefElement();
                objList[0].type = _type;
                objList[0].ptr = _ptr;
                objList[0].clone = _obj;
            }

            public void Restore()
            {
                //*(int*)(objList[0].ptr) = (int)objList[0].clone;

                object oClone = Convert.ChangeType(objList[0].clone, objList[0].type);

                //*(int*)(objList[0].ptr) = (int)Convert.ChangeType(objList[0].clone, objList[0].type);
            }

        }

    }

    sealed class Ref<T> 
    {
        private Func<T> getter;
        private Action<T> setter;
        public Ref(Func<T> getter, Action<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }
        public T Value
        {
            get { return getter(); }
            set { setter(value); }
        }
    }

    sealed class RestoreWhenFail<T>
    {
        T clone;

        private Action<T> setter;
        public RestoreWhenFail(T v, Action<T> setter)
        {
            clone = v;
            this.setter = setter;
        }

        public void Restore()
        {
            setter(clone);
        }
    }

    sealed class RestoreWhenFail
    {
        object clone;

        private Action<object> setter;
        public RestoreWhenFail(object v, Action<object> setter)
        {
            clone = v;
            this.setter = setter;
        }

        public void Restore()
        {
            setter(clone);
        }
    }

    
    //Ref<string> M() 
    //{
    //    string x = "hello";
    //    Ref<string> rx = new Ref<string>(()=>x, v=>{x=v;});
    //    rx.Value = "goodbye";
    //    Console.WriteLine(x); // goodbye
    //    return rx;
    //}
}
