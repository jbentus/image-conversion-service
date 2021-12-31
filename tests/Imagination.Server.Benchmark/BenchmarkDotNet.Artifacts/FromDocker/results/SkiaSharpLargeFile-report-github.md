``` ini

BenchmarkDotNet=v0.12.1, OS=debian 11 (container)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=6.0.101
  [Host]     : .NET Core 6.0.1 (CoreCLR 6.0.121.56705, CoreFX 6.0.121.56705), X64 RyuJIT
  DefaultJob : .NET Core 6.0.1 (CoreCLR 6.0.121.56705, CoreFX 6.0.121.56705), X64 RyuJIT


```
|                              Method | FileName |    Mean |    Error |   StdDev |  Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 |   Allocated |
|------------------------------------ |--------- |--------:|---------:|---------:|--------:|------:|--------:|------:|------:|------:|------------:|
|            SkiaSharpAsyncMemoryPool |  big.jpg | 2.165 s | 0.0717 s | 0.2115 s | 2.122 s |  1.00 |    0.00 |     - |     - |     - | 32769.97 KB |
| SkiaSharpAsyncRecycableMemoryStream |  big.jpg | 2.165 s | 0.0652 s | 0.1923 s | 2.094 s |  1.01 |    0.13 |     - |     - |     - |    99.46 KB |
