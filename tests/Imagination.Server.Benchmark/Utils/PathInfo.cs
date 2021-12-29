using System.Runtime.CompilerServices;

internal static class PathInfo
{
    public static string SolutionPath;

    static PathInfo()
    {
        string _cSharpClassPath = GetSourceFilePathName();

        SolutionPath = Directory.GetParent(_cSharpClassPath)
                                !.Parent!.Parent!.Parent!.FullName;
    }

    private static string GetSourceFilePathName([CallerFilePath] string callerFilePath = null) 
        => callerFilePath ?? "";
}