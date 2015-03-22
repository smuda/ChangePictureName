// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PicFolderHolder.cs" company="John Allberg">
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
//   The picture folder holder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JohnAllberg.ChangePictureName.Lib
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// The picture folder holder.
    /// </summary>
    public class PicFolderHolder
    {
        /// <summary>
        /// The picture folder.
        /// </summary>
        private readonly string pictureFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="PicFolderHolder"/> class.
        /// </summary>
        /// <param name="pictureFolder">
        /// The picture folder.
        /// </param>
        public PicFolderHolder(string pictureFolder)
        {
            this.pictureFolder = pictureFolder;
        }

        /// <summary>
        /// Gets the pictures.
        /// </summary>
        public IList<string> Pictures { get; private set; }

        /// <summary>
        /// Scan the folder for pictures.
        /// </summary>
        public void ScanForPictures()
        {
            this.Pictures = new List<string>();
            var picFolder = new PicFolder(this.pictureFolder);
            picFolder.OnFoundPicture += this.PicFolderFoundPicture;
            picFolder.ScanForPictures();
        }

        /// <summary>
        /// Called by the event that a picture was found.
        /// </summary>
        /// <param name="path">
        /// The path to the picture.
        /// </param>
        private void PicFolderFoundPicture(string path)
        {
            var filename = Path.GetFileName(path);
            if (filename == null || filename.StartsWith("."))
            {
                return;
            }

            this.Pictures.Add(path);
        }
    }
}
