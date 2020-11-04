using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading.Channels;
using Polly;
using Polly.Retry;

namespace RetryImpl
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // var t = GetName();
            Console.Write("Hello ");
            // Console.WriteLine(t.Result);

            var tr = await Retry.ExecuteAsync(() => GetNameAsync(), ExceptionFilter, (i) => 1000, 5);
            Console.WriteLine(tr);
        }

        static bool ExceptionFilter(Exception ex)
        {
            return ex.GetType() == typeof(ArgumentException);
        }

        static async Task<string> GetNameAsync()
        {
            await Task.Delay(1000).ConfigureAwait(false);
            throw new DivideByZeroException("DivideByZeroException");
            return "Akhilesh";
        }
    }
}
