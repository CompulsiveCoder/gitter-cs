using System;
using System.Collections.Generic;

namespace gitter
{
    public class GitRepository
    {
        public GitProcessStarter GitProcess;

        public DirectoryLocation Location;

        public GitRepository (string workingDirectory)
        {
            Location = new DirectoryLocation (workingDirectory);
            GitProcess = new GitProcessStarter ();
        }


        public void Add(string file)
        {
            // TODO: Reimplement or remove
            //var relativePath = PathUtility.EnsureRelative (file, Environment.CurrentDirectory);

            Console.WriteLine ("");
            Console.WriteLine ("Adding file to git:");
            Console.WriteLine ("  " + file);
            Console.WriteLine ("Working directory:");
            Console.WriteLine ("  " + Environment.CurrentDirectory);
            Console.WriteLine ("");

            GitProcess.Run (Location.WorkingDirectory, "add", "\"" + ConsoleArgumentFormatter.FixArgument(file) + "\"");
        }

        public void AddRemote(string name, string path)
        {
            Console.WriteLine("");
            Console.WriteLine("Adding git remote...");
            Console.WriteLine("Current directory:" + name);
            Console.WriteLine("Name:" + name);
            Console.WriteLine("Path:" + path);
            Console.WriteLine("");

            GitProcess.Run(
                Location.WorkingDirectory,
                "remote",
                "add",
                name,
                ConsoleArgumentFormatter.FixArgument(path)
            );
        }

        public bool Pull(string remote)
		{
			Console.WriteLine ("Git Pull");
			Console.WriteLine ("Remote: " + remote);

			var output = GitProcess.Run (Location.WorkingDirectory, "pull", remote);

			// If the "up-to-date" text is found then return false because no changes were detected 
			return !output.Contains ("Already up-to-date");
        }

        public bool Pull()
        {
			var output = GitProcess.Run (Location.WorkingDirectory, "pull", "--all");

			// If the "up-to-date" text is found then return false because no changes were detected 
			return !output.Contains ("Already up-to-date");
        }
        
        public void Push(string remote)
        {
            GitProcess.Run ("push", remote);
        }

        public void Push(string remote, string branch)
        {
            GitProcess.Run ("push", remote, branch);
        }

        public void Push(string remote, string branch, params string[] arguments)
        {
            var list = new List<string>();
            list.Add("push");
            list.Add(remote);
            list.Add(branch);
            list.AddRange(arguments);
            GitProcess.Run (Location.WorkingDirectory, list.ToArray());
        }

        public void Commit ()
        {
            Commit ("Committing added/changed files");
        }

        public void Commit (string message)
        {
            Console.WriteLine ("Committing added/changed files...");

            GitProcess.Run (Location.WorkingDirectory, "commit", "-a", "-m:'" + message + "'");
        }
        public void Move(string fromPath, string toPath)
        {
            GitProcess.Run("mv", ConsoleArgumentFormatter.FixArgument(fromPath), ConsoleArgumentFormatter.FixArgument(toPath));
        }

        public void Reset(params string[] arguments)
        {
            GitProcess.Run("reset", arguments);
        }

        public void Branch(string branchName)
        {
            Branch (branchName, false);
        }

        public void Branch(string branchName, bool checkoutNewBranch)
        {
            GitProcess.Run ("branch " + branchName);
            if (checkoutNewBranch)
                Checkout(branchName);
        }

        public void Checkout(string branchName)
        {
            GitProcess.Run ("checkout", branchName);
        }

    }
}

