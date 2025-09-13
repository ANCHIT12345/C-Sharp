using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp15
{
    internal class Program15
    {
        public static void Run()
        {
            int i = 20;
            for (; i > 0; i--)
            { 
                if(i==15) continue;
                if (i == 5) break;
                Console.WriteLine(i);
            }
        }
    }
}
