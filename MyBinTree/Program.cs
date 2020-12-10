using System;

namespace MyBinTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var ser = new Serializer();
            var dict = new BinTreeDict<int, int>();
            dict[-100] = 100;
            dict[0] = 0;
            dict[100] = -100;

            var serDict = ser.DataFromDict(dict, ';');
            Console.WriteLine(serDict);
        }
    }
}
