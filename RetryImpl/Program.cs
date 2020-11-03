using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Channels;
using Polly;
using Polly.Retry;

namespace RetryImpl
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Retry.Execute<int>(DoSomething, i, (int y) => 1000, 1);
            }
        }

        static void DoSomething(int i)
        {
            if (i%2 == 1)
            {
                throw new Exception("Odd Number Exception");
            }
            Console.WriteLine($"Output: {i}");
        }
    }
}
