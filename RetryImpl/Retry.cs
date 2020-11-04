using System;
using System.Threading;
using System.Threading.Tasks;
    
// TODO: Support a filter on type exceptions that should be used to cause a retry
// TODO: Implement support for cancellation

namespace RetryImpl
{
    public static class Retry
    {
        public static void Execute(Action action, Predicate<Exception> exceptionFilter, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            if (action == null) throw new ArgumentNullException();

            bool shouldRetry = false;

            for (int i = 0; i <= retryCount; i++)
            {
                if (shouldRetry)
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(waitTimeBeforeRetry(i-1)));
                }
                try
                {
                    action();
                }
                catch(Exception ex) when(exceptionFilter(ex))
                {
                    if (i < retryCount) shouldRetry = true;
                    else throw;
                }
            }
        }

        public static void Execute<T1>(Action<T1> action, T1 arg1, Predicate<Exception> exceptionFilter, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            Execute(() => action(arg1), exceptionFilter, waitTimeBeforeRetry, retryCount);
        }

        public static void Execute<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2, Predicate<Exception> exceptionFilter, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            Execute(() => action(arg1, arg2), exceptionFilter, waitTimeBeforeRetry, retryCount);
        }

        public static void Execute<T1, T2, T3>(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3, Predicate<Exception> exceptionFilter, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            Execute(() => action(arg1, arg2, arg3), exceptionFilter, waitTimeBeforeRetry, retryCount);
        }

        public static void Execute<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Predicate<Exception> exceptionFilter, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            Execute(() => action(arg1, arg2, arg3, arg4), exceptionFilter, waitTimeBeforeRetry, retryCount);
        }

        public static TResult Execute<TResult>(Func<TResult> func, Predicate<Exception> exceptionFilter, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            if (func == null) throw new ArgumentNullException();

            bool shouldRetry = false;

            for (int i = 0; i <= retryCount; i++)
            {
                if (shouldRetry) 
                    Thread.Sleep(TimeSpan.FromMilliseconds(waitTimeBeforeRetry(i-1)));

                try
                {
                    return func();
                }
                catch(Exception ex) when(exceptionFilter(ex))
                {
                    if (i < retryCount) shouldRetry = true;
                    else throw;
                }
            }

            return default(TResult);
        }

        public static TResult Execute<T1, TResult>(Func<T1, TResult> func, T1 arg1, Predicate<Exception> exceptionFilter, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            return Execute(() => func(arg1), exceptionFilter, waitTimeBeforeRetry, retryCount);
        }

        public static TResult Execute<T1, T2, TResult>(Func<T1, T2, TResult> func, T1 arg1, T2 arg2, Predicate<Exception> exceptionFilter, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            return Execute(() => func(arg1, arg2) , exceptionFilter, waitTimeBeforeRetry, retryCount);
        }

        public static TResult Execute<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, T1 arg1, T2 arg2, T3 arg3, Predicate<Exception> exceptionFilter, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            return Execute(() => func(arg1, arg2, arg3) , exceptionFilter, waitTimeBeforeRetry, retryCount);
        }

        public static TResult Execute<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Predicate<Exception> exceptionFilter, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            return Execute(() => func(arg1, arg2, arg3, arg4) , exceptionFilter, waitTimeBeforeRetry, retryCount);
        }

        public static async Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> func, Predicate<Exception> exceptionFilter, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            if (func == null) throw new ArgumentNullException();

            bool shouldRetry = false;

            for (int i = 0; i <= retryCount; i++)
            {
                if (shouldRetry)
                    await Task.Delay(TimeSpan.FromMilliseconds(waitTimeBeforeRetry(i - 1))).ConfigureAwait(false);

                try
                {
                    return await func();
                }
                catch(Exception ex) when(exceptionFilter(ex))
                {
                    if (i < retryCount) shouldRetry = true;
                    else throw;
                }
            }

            return default(TResult);
        }

        public static async Task<TResult> ExecuteAsync<T1, TResult>(Func<T1, Task<TResult>> func, T1 arg1,
            Predicate<Exception> exceptionFilter, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            return await ExecuteAsync(() => func(arg1), exceptionFilter, waitTimeBeforeRetry, retryCount);
        }
        
        public static async Task<TResult> ExecuteAsync<T1, T2, TResult>(Func<T1, T2, Task<TResult>> func, T1 arg1, T2 arg2,
            Predicate<Exception> exceptionFilter, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            return await ExecuteAsync(() => func(arg1, arg2), exceptionFilter, waitTimeBeforeRetry, retryCount);
        }
        
        public static async Task<TResult> ExecuteAsync<T1, T2, T3, TResult>(Func<T1, T2, T3, Task<TResult>> func, T1 arg1, T2 arg2, T3 arg3,
            Predicate<Exception> exceptionFilter, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            return await ExecuteAsync(() => func(arg1, arg2, arg3), exceptionFilter, waitTimeBeforeRetry, retryCount);
        }
        
        public static async Task<TResult> ExecuteAsync<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, Task<TResult>> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4,
            Predicate<Exception> exceptionFilter, Func<int, double> waitTimeBeforeRetry, short retryCount = 1)
        {
            return await ExecuteAsync(() => func(arg1, arg2, arg3, arg4), exceptionFilter, waitTimeBeforeRetry, retryCount);
        }
    }
}