``` ini

BenchmarkDotNet=v0.12.1, OS=debian 11 (container)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=6.0.101
  [Host]     : .NET Core 6.0.1 (CoreCLR 6.0.121.56705, CoreFX 6.0.121.56705), X64 RyuJIT
  DefaultJob : .NET Core 6.0.1 (CoreCLR 6.0.121.56705, CoreFX 6.0.121.56705), X64 RyuJIT


```
|     Method |  FileName |     Mean |    Error |   StdDev |    Gen 0 |    Gen 1 |    Gen 2 |  Allocated |
|----------- |---------- |---------:|---------:|---------:|---------:|---------:|---------:|-----------:|
|  SkiaSharp | jfif.jfif | 13.50 ms | 0.268 ms | 0.648 ms |  15.6250 |        - |        - |  133.65 KB |
| ImageSharp | jfif.jfif | 10.46 ms | 0.090 ms | 0.080 ms | 328.1250 | 312.5000 | 312.5000 | 2199.12 KB |
