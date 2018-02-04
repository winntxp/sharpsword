using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace ConsoleApplication5
{

    public interface IA<out T>
    {
        void W();
    }

    public class A<T> : IA<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public void W()
        {
            Console.WriteLine(typeof(T));
        }

    }

    public class a
    {

    }

    public class b : a
    {

    }

    class App
    {
        public static void Main()
        {
            var x = new System.Collections.Generic.List<string>();

            var B = (IA<a>)new A<b>();

            B.W();

            Console.Read();
        }
    }
}

