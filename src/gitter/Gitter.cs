using System;
using System.Collections.Generic;
using System.IO;

namespace gitter
{
    public class Gitter
    {
        public ProcessStarter Starter { get;set; }
        public DirectoryMover DirectoryMover { get; set; }

        public Gitter ()
        {
            Starter = new ProcessStarter();
            DirectoryMover = new DirectoryMover ();
        }

        public void Git(string command, params string[] arguments)
        {
            var list = new List<string>();
            list.Add(command);
            list.AddRange(arguments);

            Git(list.ToArray());
        }

        public void Git(params string[] arguments)
        {
            // TODO: Make this configurable and ensure it works on windows
            var gitExe = "git";

            Starter.Start(
                gitExe,
                arguments
            );
        }

        public void GitIn(string workingDirectory, params string[] arguments)
        {
            var originalDir = Environment.CurrentDirectory;

            Environment.CurrentDirectory = workingDirectory;

            Git(arguments);

            Environment.CurrentDirectory = originalDir;
        }

        public void Add(string file)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Adding file to git:");
            Console.WriteLine (file);
            Console.WriteLine ("");

            Git ("add", Starter.FixArgument(file));
        }

        public void AddTo(string path, string file)
        {

            Console.WriteLine ("");
            Console.WriteLine ("Adding file to git:");
            Console.WriteLine (file);
            Console.WriteLine ("");

            GitIn (path, "add", Starter.FixArgument(file));
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
                Starter.FixArgument(path)
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
                Starter.FixArgument(path)
            );
        }

        public void Clone(
            string sourceDir
        )
        {
            Clone(
                sourceDir,
                Starter.FixArgument(Environment.CurrentDirectory)
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

            var args = new List<string> ();

            args.Add ("clone");
            if (!String.IsNullOrEmpty (branch)) {
                args.Add ("-b");
                args.Add (branch);
            }
            args.Add (Starter.FixArgument (sourceDir));
            args.Add (Starter.FixArgument (tmpDir));
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

        public void Pull(string remote)
        {
            Git ("pull", remote);
        }

        public void Pull()
        {
            Git ("pull", "-all");
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

            GitIn (directory, "push", Starter.FixArgument(destination));
        }

        public void Move(string fromPath, string toPath)
        {
            Git("mv", Starter.FixArgument(fromPath), Starter.FixArgument(toPath));
        }

        public void Reset(params string[] arguments)
        {
            Git("reset", arguments);
        }

        public void Branch(string branchName)
        {
            Branch (branchName, false);
        }

        public void Branch(string branchName, bool checkoutNewBranch)
        {
            Git ("branch " + branchName);
            if (checkoutNewBranch)
                Checkout(branchName);
        }

        public void Checkout(string branchName)
        {
            Git ("checkout", branchName);
        }
    }
}

