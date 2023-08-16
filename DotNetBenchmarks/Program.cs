using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Perfolizer.Horology;

namespace DotNetBenchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new FastRunConfig());
        }
    }

    public class FastRunConfig : ManualConfig
    {
        public FastRunConfig()
        {
            Add(DefaultConfig.Instance); // *** add default loggers, reporters etc? ***

            AddJob(Job.Default
                .WithLaunchCount(1)     // benchmark process will be launched only once
                .WithIterationTime(new TimeInterval(100, TimeUnit.Millisecond)) // 100ms per iteration
                .WithWarmupCount(1)     // 1 warmup iteration
                                        //.WithTargetCount(2)     // 2 target iteration
            );
        }
    }
}