using System;
using System.IO;

namespace gitter
{
    static public class PathUtility
    {
        static public string EnsureRelative(string path, string workingDirectory)
        {
            if (path == Path.GetFullPath(path))
                return ToRelative (path, workingDirectory);
            else
                return path;
        }

        static public string ToRelative(string path, string workingDirectory)
        {
            if (path.Contains (workingDirectory))
                return path.Replace (workingDirectory, "").Trim(Path.DirectorySeparatorChar);
            else
                throw new ArgumentException ("The specified path is not within the specified working directory. Cannot continue.");
        }
    }
}

