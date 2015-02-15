namespace Allberg.ChangePictureName
{
    using System;

    using Allberg.ChangePictureName.Lib;

    public class Program
    {
        static void Main(string[] args)
        {
            WriteHeaderAndLicense();
            var arguments = new ProgramArguments(args);
            if (arguments.IsEmpty)
            {
                WriteUsage();
                return;
            }

            var runner = new Runner(arguments);
            runner.RunNow();
        }

        private static void WriteHeaderAndLicense()
        {
            Console.WriteLine("Allberg ChangePictureName. Copyright 2015 John Allberg");
            Console.WriteLine("Licensed with GPL v2. https://github.com/smuda/ChangePictureName");
            Console.WriteLine("");
        }

        private static void WriteUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("ChangePictureName <directory>");
        }
    }
}
