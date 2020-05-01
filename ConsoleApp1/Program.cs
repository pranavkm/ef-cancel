using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var ctx = new SampleContext();
            var tcs = new TaskCompletionSource<bool>();
            var cts = new CancellationTokenSource();

            var t1 = Task.Run(async () =>
            {
                var task = ctx.LongTask(cts.Token);
                tcs.TrySetResult(true);
                await task;
            });

            var t2 = Task.Run(async () =>
            {
                await tcs.Task;
                cts.Cancel();
                await ctx.LongTask();
            });

            await Task.WhenAll(t1, t2);
        }
    }
}
