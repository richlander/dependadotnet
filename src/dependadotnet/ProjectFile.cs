public static class Project
{
    private static string[] s_patterns = new string[] 
        {".csproj", ".fsproj", ".vbproj"};
    public static bool IsProject(string filename)
    {
        var p = s_patterns;
        return 
            filename.EndsWith(p[0]) | 
            filename.EndsWith(p[1]) |
            filename.EndsWith(p[2]);
    }
}