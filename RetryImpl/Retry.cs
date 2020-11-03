using System;
using System.Threading;

// TODO: Support a filter on type exceptions that should be used to cause a retry
// TODO: Implement task cancellation in case of async APIS

namespace RetryImpl
{
    public static class Retry
    {
        public static void Execute(Action action, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            if (action == null) throw new ArgumentNullException();

            bool shouldRetry = false;

            for (int i = 0; i < retryCount; i++)
            {
                if (shouldRetry)
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(waitTimeBeforeRetry(i-1)));
                }
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    if (i < retryCount)
                    {
                        shouldRetry = true;
                        continue;
                    }
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static void Execute<T1>(Action<T1> action, T1 arg1, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            Execute(() => action(arg1), waitTimeBeforeRetry, retryCount);
        }

        public static void Execute<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            Execute(() => action(arg1, arg2), waitTimeBeforeRetry, retryCount);
        }

        public static void Execute<T1, T2, T3>(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            Execute(() => action(arg1, arg2, arg3), waitTimeBeforeRetry, retryCount);
        }

        public static void Execute<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            Execute(() => action(arg1, arg2, arg3, arg4), waitTimeBeforeRetry, retryCount);
        }

        public static TResult Execute<TResult>(Func<TResult> func, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            throw new NotImplementedException();
        }

        public static TResult Execute<T1, TResult>(Func<T1, TResult> func, T1 arg1, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            throw new NotImplementedException();
        }

        public static TResult Execute<T1, T2, TResult>(Func<T1, T2, TResult> func, T1 arg1, T2 arg2, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            throw new NotImplementedException();
        }

        public static TResult Execute<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, T1 arg1, T2 arg2, T3 arg3, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            throw new NotImplementedException();
        }

        public static TResult Execute<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            throw new NotImplementedException();
        }
    }
}