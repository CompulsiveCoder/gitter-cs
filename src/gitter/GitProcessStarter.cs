using System;
using System.Collections.Generic;

namespace gitter
{
    public class GitProcessStarter
    {
        public ProcessStarter Starter { get; set; }
        
        public string GitExecutable = "git";

        public GitProcessStarter ()
        {
            Starter = new ProcessStarter ();
        }

        public string Run(string workingDirectory, string command, params string[] arguments)
        {
            var list = new List<string>();
            list.Add(command);
            list.AddRange(arguments);

            return Run(workingDirectory, list.ToArray());
        }

        public string Run(string workingDirectory, params string[] arguments)
        {
            var originalDirectory = Environment.CurrentDirectory;

            var argumentsString = String.Join (" ", arguments);

            var command = "/bin/sh -c '" +
                "cd " + workingDirectory + " && " +
                GitExecutable + " " + argumentsString + " && " +
                "cd " + originalDirectory +
                "'";

            Starter.Start(
                command
            );
            
            return Starter.Output;
        }
    }
}

