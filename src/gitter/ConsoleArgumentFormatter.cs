using System;
using System.Collections.Generic;

namespace gitter
{
    public class ConsoleArgumentFormatter
    {
        static public string[] Format(string[] arguments)
        {
            List<string> argsList = new List<string>();

            if (arguments != null && arguments.Length > 0)
                argsList.AddRange(arguments);

            for (int i = 0; i < argsList.Count; i++)
            {
                if (!String.IsNullOrEmpty (argsList[i]))
                {
                    argsList[i] = FixArgument(argsList[i]);
                }
            }

            return argsList.ToArray();
        }

        static public string FixArgument(string argument)
        {
            if (argument.Contains(" ")
                && argument.IndexOf("\"") != 0)
                return @"""" + argument + @"""";
            else
                return argument;
        }
    }
}

