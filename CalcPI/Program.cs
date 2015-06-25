using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalcPi
{
    class Program
    {
        static void Main(string[] args)
        {
            double eightDivPi = 0;
            double oldEightDivPi =0;
            double delta =0;
            int i = 0;
            do
            //for (int i=0;i<2000;i+=2)
            {
                int a = 2 * i + 1;
                int b = 2 * i + 3;
                oldEightDivPi = eightDivPi;
                eightDivPi = eightDivPi + 1 / (a * b * 1.0);
                delta = eightDivPi - oldEightDivPi;
                string message = string.Format("x{0},{1}\tPi = {2}\tDelta {3}", a, b, eightDivPi * 8, delta);
                //System.Diagnostics.Debug.WriteLine(message);
                Console.WriteLine(message);
                i = i + 2;
            } while ((i < 10) || delta > 0.00000000000009);

            string report = string.Format("Round {0} , Pi = {1}", i / 2, eightDivPi);
            Console.WriteLine(report);
        }


    }
}

/*
 * 
    1/ab +1/cd +1/ef => (ab+cd)/abcd + 1/ef => (ab+cd)ef + ef / abcdef
            
 */

// 1/3 + 1/(5*7) == 1/3 + 1/35  = 35/105 + 3/105 = 38/105--> 2.895238095238095 
// 36/105 + 1/99 =   (105*36+1) /10395 = 3781/10395 = > 30248/10395 --> 2.90986050986051
//1,3 2.66666666666667
//5,7 2.8952380952381
//9,11 2.97604617604618