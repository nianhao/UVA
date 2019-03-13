using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UVA
{
    class test
    {
        public static void test_copy()
        {
            Queue<string> b = new Queue<string> ();
            for(int i=0;i<10;i++)
            {
                string[] a=new string [5];
                a[0]= "s"+i.ToString()+"ss";
                b.Enqueue(a[0]);
            }
            foreach(string tt in b)
            {
                Console.WriteLine(tt);
            }
        }

            
    }
}
