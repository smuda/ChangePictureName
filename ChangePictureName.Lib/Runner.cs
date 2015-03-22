// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Runner.cs" company="John Allberg">
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
//   The runner.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JohnAllberg.ChangePictureName.Lib
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The runner.
    /// </summary>
    public class Runner
    {
        /// <summary>
        /// The settings.
        /// </summary>
        private readonly ProgramArguments settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="Runner"/> class.
        /// </summary>
        /// <param name="settings">
        /// The settings.
        /// </param>
        public Runner(ProgramArguments settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// Run now.
        /// </summary>
        public void RunNow()
        {
            foreach (var pictureFolder in this.settings.PictureFolders)
            {
                this.RunNow(pictureFolder);
            }
        }

        /// <summary>
        /// Run now with a specific input folder.
        /// </summary>
        /// <param name="inputFolder">
        /// The input folder.
        /// </param>
        public void RunNow(string inputFolder)
        {
            Console.WriteLine("Starting to find pictures in " + inputFolder);
            var picFolder = new PicFolderHolder(inputFolder);
            picFolder.ScanForPictures();

            Console.WriteLine("Found {0} pictures.", picFolder.Pictures.Count);

            var regex = new Regex("^[\\d]{8,8}_[\\d]{1,8}");
            foreach (var picturePath in picFolder.Pictures)
            {
                if (picturePath == null)
                {
                    // This really shouldn't happen...
                    continue;
                }

                var filename = Path.GetFileNameWithoutExtension(picturePath);
                var directory = Path.GetDirectoryName(picturePath);
                if (directory == null)
                {
                    continue;
                }

                if (regex.IsMatch(filename))
                {
                    Console.WriteLine("Checked {0} and it's alredy done.", filename);
                    continue;
                }

                if (!File.Exists(picturePath))
                {
                    continue;
                }

                if (IsOnlineOnly(picturePath))
                {
                    Console.WriteLine("This picture isn't available offline: " + picturePath);
                    continue;
                }

                PictureFile picture;
                try
                {
                    picture = new PictureFile(picturePath);
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error while opening picture: " + exception.Message + " - " + picturePath);
                    continue;
                }

                if (picture.TakenDate.Equals(DateTime.MinValue))
                {
                    Console.WriteLine("File doesn't have a date for taking the picture: " + picturePath);
                    continue;
                }

                var newFilename = Path.Combine(
                    directory,
                    CreateFilename(picture.TakenDate) + Path.GetExtension(picturePath));

                var counter = 0;
                if (File.Exists(newFilename))
                {
                    while (File.Exists(newFilename.Replace(".", "_" + counter + ".")))
                    {
                        counter++;
                    }

                    newFilename = newFilename.Replace(".", "_" + counter + ".");
                }

                File.Move(picturePath, newFilename);
                Console.WriteLine("Changed name from {0} to {1}", picturePath, newFilename);
            }
        }

        /// <summary>
        /// Check if a file on available online only.
        /// </summary>
        /// <param name="picturePath">
        /// The picture path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool IsOnlineOnly(string picturePath)
        {
            var attributes = File.GetAttributes(picturePath);
            return attributes.HasFlag(FileAttributes.Hidden) && attributes.HasFlag(FileAttributes.Archive) && attributes.HasFlag(FileAttributes.SparseFile);
        }

        /// <summary>
        /// Create a filename from a date.
        /// </summary>
        /// <param name="takenDate">
        /// The taken date.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string CreateFilename(DateTime takenDate)
        {
            return takenDate.ToString("yyyyMMdd_hhmmss");
        }
    }
}
