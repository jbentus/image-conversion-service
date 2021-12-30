[MemoryDiagnoser]
public class ImageProcessor
{
    private const string ResxPath = "resources";
    private readonly FileStreamOptions _openForReading = new()
    {
        Mode = FileMode.Open,
        Access = FileAccess.Read,
        Options = FileOptions.Asynchronous | FileOptions.SequentialScan
    };

    private readonly SkiaSharpProcessor _skiaSharp = new();
    private readonly ImageSharpProcessor _imageSharp = new();

    [Params("jfif.jfif")]
    public string FileName;

    [Benchmark]
    public Stream SkiaSharp()
    {
        var path = Path.Combine(PathInfo.SolutionPath, ResxPath, FileName);

        using var inputStream = new FileStream(path, _openForReading);
        return _skiaSharp.Convert(inputStream, CancellationToken.None);
    }

    [Benchmark]
    public Stream ImageSharp()
    {
        var path = Path.Combine(PathInfo.SolutionPath, ResxPath, FileName);

        using var inputStream = new FileStream(path, _openForReading);
        return _imageSharp.Convert(inputStream, CancellationToken.None);
    }
}
