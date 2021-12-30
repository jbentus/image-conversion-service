[MemoryDiagnoser]
public class SkiaSharpLargeFile
{
    private const string ResxPath = "resources";
    private readonly FileStreamOptions _openForReading = new()
    {
        Mode = FileMode.Open,
        Access = FileAccess.Read,
        Options = FileOptions.Asynchronous | FileOptions.SequentialScan
    };

    private readonly SkiaSharpProcessor _skiaSharp = new();

    [Params("big.jpg")]
    public string FileName;

    [Benchmark(Baseline = true)]
    public async Task<Stream> SkiaSharpAsyncMemoryPool()
    {
        var path = Path.Combine(PathInfo.SolutionPath, ResxPath, FileName);

        using var inputStream = new FileStream(path, _openForReading);
        return await _skiaSharp.ConvertAsyncMemoryPool(inputStream, CancellationToken.None);
    }

    [Benchmark]
    public async Task<Stream> SkiaSharpAsyncMemAndStreamPooled()
    {
        var path = Path.Combine(PathInfo.SolutionPath, ResxPath, FileName);

        using var inputStream = new FileStream(path, _openForReading);
        return await _skiaSharp.ConvertAsyncMemAndStreamPooled(inputStream, CancellationToken.None);
    }
}
