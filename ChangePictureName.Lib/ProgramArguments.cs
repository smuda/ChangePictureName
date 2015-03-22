// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgramArguments.cs" company="John Allberg">
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
// --------------------------------------------------------------------------------------------------------------------

namespace JohnAllberg.ChangePictureName.Lib
{
    using System.Linq;

    /// <summary>
    /// Parses the arguments to the program.
    /// </summary>
    public class ProgramArguments
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramArguments"/> class.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public ProgramArguments(string[] args)
        {
            this.IsEmpty = args.Length == 0;
            if (this.IsEmpty)
            {
                return;
            }

            this.PictureFolders = args.ToArray();
        }

        /// <summary>
        /// Gets a value indicating whether the argument is empty.
        /// </summary>
        public bool IsEmpty { get; private set; }

        /// <summary>
        /// Gets the picture folders.
        /// </summary>
        public string[] PictureFolders { get; private set; }
    }
}
