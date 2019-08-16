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
                      update_schedule: "live"
                    */

                    var filename = Path.GetFileName(file);
                    var parentDir = Path.GetDirectoryName(file);
                    var relativeDir = string.Empty;

                    if (parentDir == null ||
                        parentDir.Length == path.Length)
                        {
                            relativeDir="/";
                        }
                        else
                        {
                            relativeDir = parentDir.Substring(path.Length).Replace('\\','/');
                        }
                    WriteLine( 
$@"  - package_manager: ""dotnet:nuget""
    directory: ""{relativeDir}"" #{filename}
    update_schedule: ""live""");
                }
            }
        }

        static void PrintBoilerPlate()
        {
            WriteLine(
@"version: 1

update_configs:");
        }

        static void Error()
        {
            WriteLine("Must specify a repo root directory as input");
        }
    }
}
