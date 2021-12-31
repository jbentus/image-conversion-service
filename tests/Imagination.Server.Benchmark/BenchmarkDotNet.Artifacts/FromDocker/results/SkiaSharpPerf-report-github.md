``` ini

BenchmarkDotNet=v0.12.1, OS=debian 11 (container)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=6.0.101
  [Host]     : .NET Core 6.0.1 (CoreCLR 6.0.121.56705, CoreFX 6.0.121.56705), X64 RyuJIT
  DefaultJob : .NET Core 6.0.1 (CoreCLR 6.0.121.56705, CoreFX 6.0.121.56705), X64 RyuJIT


```
|                              Method |  FileName |     Mean |    Error |   StdDev |   Median | Ratio | RatioSD |   Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------------ |---------- |---------:|---------:|---------:|---------:|------:|--------:|--------:|------:|------:|----------:|
|                      SkiaSharpAsync | jfif.jfif | 13.34 ms | 0.363 ms | 1.070 ms | 13.06 ms |  1.00 |    0.00 | 31.2500 |     - |     - | 207.85 KB |
|            SkiaSharpAsyncMemoryPool | jfif.jfif | 13.90 ms | 0.363 ms | 1.049 ms | 13.90 ms |  1.05 |    0.11 | 15.6250 |     - |     - | 128.72 KB |
| SkiaSharpAsyncRecycableMemoryStream | jfif.jfif | 14.07 ms | 0.394 ms | 1.155 ms | 13.76 ms |  1.06 |    0.12 |       - |     - |     - |   3.76 KB |
