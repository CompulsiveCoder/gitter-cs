using System;
using System.Collections.Generic;
using System.IO;

namespace gitter
{
    public class Gitter
    {
        private string workingDirectory;
        public string WorkingDirectory
        {
            get{
                if (!String.IsNullOrEmpty (workingDirectory))
                    return workingDirectory;
                else
                    return Environment.CurrentDirectory;
            }
            set{
                workingDirectory = value;
            }
        }
        public ProcessStarter ProcessStarter { get;set; }
        public DirectoryMover DirectoryMover { get; set; }

		public string GitExecutable = "git";

        public Gitter ()
        {
            ProcessStarter = new ProcessStarter();
            DirectoryMover = new DirectoryMover ();
        }

        public string Git(string command, params string[] arguments)
        {
            var list = new List<string>();
            list.Add(command);
            list.AddRange(arguments);

            return Git(list.ToArray());
        }

        public string Git(params string[] arguments)
        {
            ProcessStarter.Start(
				GitExecutable,
                arguments
            );

			return ProcessStarter.Output;
        }

		public string GitIn(string workingDirectory, params string[] arguments)
        {
            var originalDir = Environment.CurrentDirectory;

            Environment.CurrentDirectory = workingDirectory;

            return Git(arguments);

            Environment.CurrentDirectory = originalDir;
        }

        public void Add(string file)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Adding file to git:");
            Console.WriteLine ("  " + file);
            Console.WriteLine ("Working directory:");
            Console.WriteLine ("  " + Environment.CurrentDirectory);
            Console.WriteLine ("");

            Git ("add", "\"" + ProcessStarter.FixArgument(file) + "\"");
        }

        public void AddTo(string path, string file)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Adding file to git:");
            Console.WriteLine (file);
            Console.WriteLine ("");

            GitIn (path, "add", ProcessStarter.FixArgument(file));
        }

        public void AddRemote(string name, string path)
        {
            Console.WriteLine("");
            Console.WriteLine("Adding git remote...");
            Console.WriteLine("Current directory:" + name);
            Console.WriteLine("Name:" + name);
            Console.WriteLine("Path:" + path);
            Console.WriteLine("");

            Git(
                "remote",
                "add",
                name,
                ProcessStarter.FixArgument(path)
            );
        }

        public void AddRemoteTo(string directory, string name, string path)
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
                ProcessStarter.FixArgument(path)
            );
        }

        public void Clone(
            string sourceDir
        )
        {
            Clone(
                sourceDir,
                ProcessStarter.FixArgument(Environment.CurrentDirectory)
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
            Console.WriteLine ("Working directory: " + WorkingDirectory);

            // Create a temporary directory path to clone to
            // (the temporary folder works around the issue of cloning into existing directory)
            var tmpDir = Path.Combine(destinationDir, "_tmpclone");

            var relativeSourceDir = ProcessStarter.FixArgument (sourceDir);

            var relativeTmpDir = ProcessStarter.FixArgument (tmpDir);

            var args = new List<string> ();

            args.Add ("clone");
            if (!String.IsNullOrEmpty (branch)) {
                args.Add ("-b");
                args.Add (branch);
            }
            args.Add ("\"" + relativeSourceDir + "\"");
            args.Add ("\"" + relativeTmpDir + "\"");
            args.Add ("--verbose");

            Git (args.ToArray());

            DirectoryMover.Move(tmpDir, destinationDir, true);

            Console.WriteLine("");
            Console.WriteLine("Complete");
            Console.WriteLine("");

        }

        public void Commit ()
        {
            Commit ("Committing added/changed files");
        }

        public void Commit (string message)
        {
            Console.WriteLine ("Committing added/changed files...");

            Git ("commit", "-a", "-m:'" + message + "'");
        }

        public void CommitTo (string directory)
        {
            CommitTo ("");
        }

        public void CommitTo (string directory, string message)
        {
            Console.WriteLine ("Committing added/changed files...");

            GitIn (directory, "commit", "-a", "-m:'" + message + "'");
        }

        public void Init()
        {
            Console.WriteLine ("Initializing repository:");
            Console.WriteLine ("Path: " + Environment.CurrentDirectory);

            Git ("init");
        }

        public void Init(string path)
        {
            Console.WriteLine ("Initializing repository:");
            Console.WriteLine ("Path: " + path);

            Environment.CurrentDirectory = path;

            Git ("init");
        }

        public bool Pull(string remote)
        {
			var output = Git ("pull", remote);

			// If the "up-to-date" text is found then return false because no changes were detected 
			return !output.Contains ("Already up-to-date");
        }

        public bool Pull()
        {
            var output = Git ("pull", "-all");

			// If the "up-to-date" text is found then return false because no changes were detected 
			return !output.Contains ("Already up-to-date");
        }

        public void PullTo(string directory, string remote)
        {
            GitIn (directory, "pull", remote, "master"); // Should branch be left out?
        }

        public void Push(string remote)
        {
            Git ("push", remote);
        }

        public void Push(string remote, string branch)
        {
            Git ("push", remote, branch);
        }

        public void Push(string remote, string branch, params string[] arguments)
        {
            var list = new List<string>();
            list.Add("push");
            list.Add(remote);
            list.Add(branch);
            list.AddRange(arguments);
            Git (list.ToArray());
        }

        public void PushFrom(string directory, string remote)
        {
            var originalDirectory = Environment.CurrentDirectory;

            Environment.CurrentDirectory = directory;

            Push(remote);

            Environment.CurrentDirectory = originalDirectory;
        }

        public void PushFromDirectoryToDirectory (string directory, string destination)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Pushing git from:");
            Console.WriteLine (directory);
            Console.WriteLine ("To:");
            Console.WriteLine (destination);
            Console.WriteLine ("");

            GitIn (directory, "push", ProcessStarter.FixArgument(destination));
        }

        public void Move(string fromPath, string toPath)
        {
            Git("mv", ProcessStarter.FixArgument(fromPath), ProcessStarter.FixArgument(toPath));
        }

        public void Reset(params string[] arguments)
        {
            Git("reset", arguments);
        }

		public string Branch()
		{
			return Branch ("", false);
		}

        public void Branch(string branchName)
        {
            Branch (branchName, false);
        }

        public string Branch(string branchName, bool checkoutNewBranch)
        {
            var output = Git ("branch " + branchName);
            if (checkoutNewBranch)
                Checkout(branchName);
			return output;
        }

        public void Checkout(string branchName)
        {
            Git ("checkout", branchName);
        }

		public bool IsInitialized(string directoryPath)
		{
			var gitFolderPath = Path.Combine (directoryPath, ".git");

			return Directory.Exists (gitFolderPath);
		}
    }
}

