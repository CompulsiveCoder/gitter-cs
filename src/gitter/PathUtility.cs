using System;
using System.IO;

namespace gitter
{
    // TODO: Remove or reimplement
    static public class PathUtility
    {
        static public string EnsureRelative(string path)
        {
            return EnsureRelative (path, Environment.CurrentDirectory);
        }

        static public string EnsureRelative(string path, string workingDirectory)
        {
            if (path == Path.GetFullPath(path))
                return ToRelative (path, workingDirectory);
            else
                return path;
        }

        static public string ToRelative(string path)
        {
            return ToRelative (path, Environment.CurrentDirectory);
        }

        static public string ToRelative(string path, string workingDirectory)
        {
            return MakeRelativePath (workingDirectory, path);
            /*// Put "/" on the ends of the paths to ensure proper comparisons
            if (!workingDirectory.EndsWith (Path.DirectorySeparatorChar.ToString()))
                workingDirectory += Path.DirectorySeparatorChar;
            
            if (!path.EndsWith (Path.DirectorySeparatorChar.ToString()))
                path += Path.DirectorySeparatorChar;

            if (path.Contains (workingDirectory)) {
                var newPath = path.Replace (workingDirectory, "").Trim (Path.DirectorySeparatorChar);

                if (String.IsNullOrEmpty (newPath))
                    newPath = ".";

                return newPath;
            } else {
                // TODO: Clean up
                //throw new ArgumentException ("The specified path is not within the specified working directory. Cannot continue.");
                return GetOutsideRelativePath(path, workingDirectory);
            }*/
        }

        static public string GetOutsideRelativePath(string path, string workingDirectory)
        {
            throw new NotImplementedException ();
        }
        static public string MakeRelativePath(string workingDirectory, string fullPath)
        {
            string result = string.Empty;
            int offset;

            // this is the easy case.  The file is inside of the working directory.
            if( fullPath.StartsWith(workingDirectory) )
            {
                return fullPath.Substring(workingDirectory.Length + 1);
            }

            // the hard case has to back out of the working directory
            string[] baseDirs = workingDirectory.Split(new char[] { ':', '\\', '/' });
            string[] fileDirs = fullPath.Split(new char[] { ':', '\\', '/' });

            // if we failed to split (empty strings?) or the drive letter does not match
            if( baseDirs.Length <= 0 || fileDirs.Length <= 0 || baseDirs[0] != fileDirs[0] )
            {
                // can't create a relative path between separate harddrives/partitions.
                return fullPath;
            }

            // skip all leading directories that match
            for (offset = 1; offset < baseDirs.Length; offset++)
            {
                if (baseDirs[offset] != fileDirs[offset])
                    break;
            }

            // back out of the working directory
            for (int i = 0; i < (baseDirs.Length - offset); i++)
            {
                result += "..\\";
            }

            // step into the file path
            for (int i = offset; i < fileDirs.Length-1; i++)
            {
                result += fileDirs[i] + "\\";
            }

            // append the file
            result += fileDirs[fileDirs.Length - 1];

            return result;
        }
    }
}

