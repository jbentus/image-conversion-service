``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19044
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=6.0.100
  [Host]     : .NET Core 6.0.0 (CoreCLR 6.0.21.52210, CoreFX 6.0.21.52210), X64 RyuJIT
  DefaultJob : .NET Core 6.0.0 (CoreCLR 6.0.21.52210, CoreFX 6.0.21.52210), X64 RyuJIT


```
|     Method |  FileName |      Mean |     Error |    StdDev |    Gen 0 |    Gen 1 |    Gen 2 |  Allocated |
|----------- |---------- |----------:|----------:|----------:|---------:|---------:|---------:|-----------:|
|  SkiaSharp | jfif.jfif | 12.954 ms | 0.2041 ms | 0.1704 ms |  15.6250 |        - |        - |   138.2 KB |
| ImageSharp | jfif.jfif |  9.662 ms | 0.0439 ms | 0.0390 ms | 312.5000 | 296.8750 | 296.8750 | 2202.06 KB |
