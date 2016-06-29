using System;
using System.Collections.Generic;
using System.IO;

namespace gitter
{
    public class Gitter
    {
        public GitProcessStarter GitProcess { get;set; }
        public DirectoryMover DirectoryMover { get; set; }

        public Gitter()
        {
            GitProcess = new GitProcessStarter();
            DirectoryMover = new DirectoryMover ();
        }

        public void Clone(
            string sourceDir
        )
        {
            Clone(
                sourceDir,
                ConsoleArgumentFormatter.FixArgument(Environment.CurrentDirectory)
            );
        }

        public void Clone(
            string sourceDir,
            string destinationDir
        )
        {
            Clone(sourceDir, "", destinationDir);
        }

        public void Clone(
            string sourceDir,
            string branch,
            string destinationDir
        )
        {
            Console.WriteLine("");
            Console.WriteLine ("Cloning...");
            Console.WriteLine ("Source: " + sourceDir);
            Console.WriteLine ("Destination: " + destinationDir);

            // Create a temporary directory path to clone to
            // (the temporary folder works around the issue of cloning into existing directory)
            var tmpDir = Path.Combine(destinationDir, "_tmpclone");

            var relativeSourceDir = ConsoleArgumentFormatter.FixArgument (sourceDir);

            var relativeTmpDir = ConsoleArgumentFormatter.FixArgument (tmpDir);

            var args = new List<string> ();

            args.Add ("clone");
            if (!String.IsNullOrEmpty (branch)) {
                args.Add ("-b");
                args.Add (branch);
            }
            args.Add ("\"" + relativeSourceDir + "\"");
            args.Add ("\"" + relativeTmpDir + "\"");
            args.Add ("--verbose");

            GitProcess.Run (Environment.CurrentDirectory, args.ToArray());

            DirectoryMover.Move(tmpDir, destinationDir, true);

            Console.WriteLine("");
            Console.WriteLine("Complete");
            Console.WriteLine("");

        }

        public GitRepository Init()
        {
            return Init (Environment.CurrentDirectory);
        }

        public GitRepository Init(string workingDirectory)
        {
            Console.WriteLine ("Initializing repository");

            if (!Directory.Exists (workingDirectory))
                Directory.CreateDirectory (workingDirectory);

            GitProcess.Run (workingDirectory, "init");

            return Open (workingDirectory);
        }

        public GitRepository Open(string workingDirectory)
        {
            return new GitRepository (workingDirectory);
        }

        public bool IsRepository(string repositoryDirectory)
        {
            var gitDir = Path.Combine (repositoryDirectory, ".git");

            return Directory.Exists (gitDir);
        }
    }
}

