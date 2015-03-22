// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PicFolder.cs" company="John Allberg">
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
//   The picture folder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JohnAllberg.ChangePictureName.Lib
{
    using System.IO;

    /// <summary>
    /// The picture folder.
    /// </summary>
    public class PicFolder
    {
        /// <summary>
        /// The picture folder.
        /// </summary>
        private readonly string pictureFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="PicFolder"/> class.
        /// </summary>
        /// <param name="pictureFolder">
        /// The picture folder.
        /// </param>
        public PicFolder(string pictureFolder)
        {
            this.pictureFolder = pictureFolder;
        }

        /// <summary>
        /// The found picture delegate.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        public delegate void FoundPicture(string path);

        /// <summary>
        /// Event called when a picture is found.
        /// </summary>
        public event FoundPicture OnFoundPicture;

        /// <summary>
        /// Scan the folder for pictures.
        /// </summary>
        public void ScanForPictures()
        {
            foreach (var directory in Directory.GetDirectories(this.pictureFolder))
            {
                var subFolder = new PicFolder(directory);
                subFolder.OnFoundPicture += this.CallOnFoundPicture;
                subFolder.ScanForPictures();
            }

            var supportedExtensions = new[] { "jpg", "gif" };

            foreach (var extension in supportedExtensions)
            {
                foreach (var file in Directory.GetFiles(this.pictureFolder, "*." + extension))
                {
                    this.CallOnFoundPicture(file);
                }
            }
        }

        /// <summary>
        /// Call the event for a found picture.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        private void CallOnFoundPicture(string path)
        {
            var theEvent = this.OnFoundPicture;
            if (theEvent != null)
            {
                theEvent(path);
            }
        }
    }
}
