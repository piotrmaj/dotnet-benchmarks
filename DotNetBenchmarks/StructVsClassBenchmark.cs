using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace DotNetBenchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class StructVsClassBenchmark
    {
        [Benchmark]
        public void TestClassBenchmark()
        {
            var classInstance = new TestClass
            {
                Id = 1,
                Bytes = new Byte[512],
            };
        }

        [Benchmark]
        public void TestStructBenchmark()
        {
            Span<byte> bytes = stackalloc byte[512];
            var stuctInstance = new TestStruct
            {
                Id = 1,
                Bytes = bytes.ToArray(),
            };
        }

        [Benchmark]
        public void TestRefStructBenchmark()
        {
            Span<byte> bytes = stackalloc byte[512];
            var stuctInstance = new TestRefStruct
            {
                Id = 1,
                Bytes = bytes
            };
        }


        private class TestClass
        {
            public int Id { get; set; }
            public byte[]? Bytes { get; set; }
        }

        public struct TestStruct
        {
            public int Id { get; set; }
            public byte[] Bytes { get; set; }
        }

        public ref struct TestRefStruct
        {
            public int Id { get; set; }
            public ReadOnlySpan<byte> Bytes { get; set; }
        }
    }
}
