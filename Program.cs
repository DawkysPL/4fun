using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var factory = new ExampleFactoryExample();
            factory.Action();

            // var deska = new GenericServices();
            // deska.Act();

            Func<string, string, int, int> kek = Haha1;
            var xd = Hehe(5, Haha1);

            var xddd = Hehe(5, (x, y, z) =>
            {
                x = x + y;
                y = y + z;

                return 5;
            }); */
            var solution = new Solution();
            solution.Action();

           

            Console.ReadLine();
        }

        public static int Hehe(int hah, Func<string,string,int,int> kekw)
        {
            var cos = kekw("cos", "cos1", 3);

            return cos;
        }

        public static int Haha1(string ha1, string ha2, int ha3)
        {
            return 5;
        }
    }
    
}
