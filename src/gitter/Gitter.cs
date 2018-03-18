using System;
using System.Collections.Generic;
using System.IO;

namespace gitter
{
    public class Gitter
    {
        public GitProcessStarter GitProcess { get;set; }
        public DirectoryMover DirectoryMover { get; set; }

		public string GitExecutable = "git";

        public Gitter ()
        {
            GitProcess = new GitProcessStarter();
            DirectoryMover = new DirectoryMover ();
        }

        /*public string Git(string command, params string[] arguments)
        {
            var list = new List<string>();
            list.Add(command);
            list.AddRange(arguments);

            return Git(list.ToArray());
        }

        public string Git(params string[] arguments)
        {
            GitProcess.Start(
				GitExecutable,
                arguments
            );

			return ProcessStarter.Output;
        }*/

		/*public string GitIn(string workingDirectory, params string[] arguments)
        {
            var originalDir = Environment.CurrentDirectory;

            Environment.CurrentDirectory = workingDirectory;

            return GitProcess.Run(arguments);

            Environment.CurrentDirectory = originalDir;
        }*/

        /*public void Add(string file)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Adding file to git:");
            Console.WriteLine ("  " + file);
            Console.WriteLine ("Working directory:");
            Console.WriteLine ("  " + Environment.CurrentDirectory);
            Console.WriteLine ("");

            Git ("add", "\"" + GitProcess.Starter.FixArgument(file) + "\"");
        }

        public void AddTo(string path, string file)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Adding file to git:");
            Console.WriteLine (file);
            Console.WriteLine ("");

            GitIn (path, "add", GitProcess.Starter.FixArgument(file));
        }*/

        /*public void AddRemote(string name, string path)
        {
            Console.WriteLine("");
            Console.WriteLine("Adding git remote...");
            Console.WriteLine("Current directory:" + name);
            Console.WriteLine("Name:" + name);
            Console.WriteLine("Path:" + path);
            Console.WriteLine("");

            GitProcess.Run(
                "remote",
                "add",
                name,
                GitProcess.Starter.FixArgument(path)
            );
        }*/

        /*public void AddRemoteTo(string directory, string name, string path)
        {
            Console.WriteLine("");
            Console.WriteLine("Adding git remote...");
            Console.WriteLine("Current directory:" + directory);
            Console.WriteLine("Name:" + name);
            Console.WriteLine("Path:" + path);
            Console.WriteLine("");

            GitIn (
                directory,
                "remote",
                "add",
                name,
                GitProcess.Starter.FixArgument(path)
            );
        }*/

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

		public bool IsInitialized(string directoryPath)
		{
			var gitFolderPath = Path.Combine (directoryPath, ".git");

			return Directory.Exists (gitFolderPath);
		}
    }
}

