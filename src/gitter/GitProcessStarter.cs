using System;
using System.Collections.Generic;

namespace gitter
{
    public class GitProcessStarter
    {
        public ProcessStarter Starter { get; set; }

        public GitProcessStarter ()
        {
            Starter = new ProcessStarter ();
        }

        public void Run(string workingDirectory, string command, params string[] arguments)
        {
            var list = new List<string>();
            list.Add(command);
            list.AddRange(arguments);

            Run(workingDirectory, list.ToArray());
        }

        public void Run(string workingDirectory, params string[] arguments)
        {
            // TODO: Make this configurable and ensure it works on windows
            var gitExe = "git";

            var originalDirectory = Environment.CurrentDirectory;

            var argumentsString = String.Join (" ", arguments);

            var command = "/bin/sh -c '" +
                "cd " + workingDirectory + " && " +
                gitExe + " " + argumentsString + " && " +
                "cd " + originalDirectory +
                "'";

            Starter.Start(
                command
            );
        }
    }
}

