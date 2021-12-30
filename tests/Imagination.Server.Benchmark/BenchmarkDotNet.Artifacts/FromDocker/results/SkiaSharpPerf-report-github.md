``` ini

BenchmarkDotNet=v0.12.1, OS=debian 11 (container)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=6.0.101
  [Host]     : .NET Core 6.0.1 (CoreCLR 6.0.121.56705, CoreFX 6.0.121.56705), X64 RyuJIT
  DefaultJob : .NET Core 6.0.1 (CoreCLR 6.0.121.56705, CoreFX 6.0.121.56705), X64 RyuJIT


```
|                           Method |  FileName |     Mean |    Error |   StdDev |   Median | Ratio | RatioSD |   Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------------------- |---------- |---------:|---------:|---------:|---------:|------:|--------:|--------:|------:|------:|----------:|
|                   SkiaSharpAsync | jfif.jfif | 12.96 ms | 0.332 ms | 0.979 ms | 13.07 ms |  1.00 |    0.00 | 31.2500 |     - |     - | 207.85 KB |
|         SkiaSharpAsyncMemoryPool | jfif.jfif | 12.77 ms | 0.339 ms | 1.000 ms | 12.47 ms |  0.99 |    0.10 | 15.6250 |     - |     - | 128.72 KB |
| SkiaSharpAsyncMemAndStreamPooled | jfif.jfif | 12.02 ms | 0.229 ms | 0.290 ms | 11.96 ms |  0.90 |    0.08 |       - |     - |     - |   1.72 KB |
