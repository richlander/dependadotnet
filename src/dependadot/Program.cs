using System;
using System.IO;
using static System.Console;

namespace dependadot
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = string.Empty;

            if (Console.IsInputRedirected)
            {
                var line = In.ReadLine();
                if (line is object)
                {
                    path = line;
                }
                else
                {
                    Error();
                    return;
                }
            }
            else if (args.Length == 1 &&
                Directory.Exists(args[0]))
            {
                path = args[0];
            }
            else
            {
                Error();
                return;
            }

            var printboilerplate = true;

            foreach (var file in Directory.EnumerateFiles(path,"*.*",SearchOption.AllDirectories))
            {
                if (Project.IsProject(file))
                {
                    if (printboilerplate)
                    {
                        PrintBoilerPlate();
                        printboilerplate = false;
                    }

                    /* pattern:
                    - package_manager: "dotnet:nuget"
                      directory: "/one"
                      update_schedule: "daily"
                    */

                    var relativeDir = file.Substring(path.Length);
                    WriteLine( "  - package_manager: \"dotnet:nuget\"");
                    WriteLine($"    directory: {relativeDir}");
                    WriteLine( "    update_schedule: \"daily\"");
                }
            }
        }

        static void PrintBoilerPlate()
        {
            var boilerplate = "version: 1\n\nupdate_configs:";
            WriteLine(boilerplate);
        }

        static void Error()
        {
            WriteLine("Must specify a repo root directory as input");
        }
    }
}
