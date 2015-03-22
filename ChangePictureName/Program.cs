// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="John Allberg">
//   Copyright (c) 2015 John Allberg
//   
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//   
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//   
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JohnAllberg.ChangePictureName
{
    using System;

    using JohnAllberg.ChangePictureName.Lib;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
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

        /// <summary>
        /// Write header and license.
        /// </summary>
        private static void WriteHeaderAndLicense()
        {
            Console.WriteLine("Allberg ChangePictureName. Copyright 2015 John Allberg");
            Console.WriteLine("Licensed with MIT License. https://github.com/smuda/ChangePictureName");
            Console.WriteLine(string.Empty);
        }

        /// <summary>
        /// Write usage.
        /// </summary>
        private static void WriteUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("ChangePictureName <directory>");
        }
    }
}
