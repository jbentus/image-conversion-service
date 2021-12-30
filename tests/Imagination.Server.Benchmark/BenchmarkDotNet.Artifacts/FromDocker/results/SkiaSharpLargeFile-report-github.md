``` ini

BenchmarkDotNet=v0.12.1, OS=debian 11 (container)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=6.0.101
  [Host]     : .NET Core 6.0.1 (CoreCLR 6.0.121.56705, CoreFX 6.0.121.56705), X64 RyuJIT
  DefaultJob : .NET Core 6.0.1 (CoreCLR 6.0.121.56705, CoreFX 6.0.121.56705), X64 RyuJIT


```
|                           Method | FileName |    Mean |    Error |   StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 |   Allocated |
|--------------------------------- |--------- |--------:|---------:|---------:|------:|--------:|------:|------:|------:|------------:|
|         SkiaSharpAsyncMemoryPool |  big.jpg | 2.132 s | 0.0660 s | 0.1947 s |  1.00 |    0.00 |     - |     - |     - | 32769.97 KB |
| SkiaSharpAsyncMemAndStreamPooled |  big.jpg | 2.124 s | 0.0573 s | 0.1690 s |  1.00 |    0.12 |     - |     - |     - |    69.19 KB |
