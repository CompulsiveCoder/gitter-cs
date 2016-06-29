using System;

namespace gitter
{
    public class DirectoryLocation
    {
        private string workingDirectory;
        public string WorkingDirectory { get { return workingDirectory; } }

        public DirectoryLocation (string workingDirectory)
        {
            this.workingDirectory = workingDirectory;
        }
    }
}

