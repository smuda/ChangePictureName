// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PictureFile.cs" company="John Allberg">
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
//   Represents a picture file.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JohnAllberg.ChangePictureName.Lib
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents a picture file.
    /// </summary>
    public class PictureFile
    {
        /// <summary>
        /// The time attribute.
        /// </summary>
        private const int TimeAttribute = 0x9003;

        /// <summary>
        /// The property items.
        /// </summary>
        private readonly PropertyItem[] propertyItems;

        /// <summary>
        /// Initializes a new instance of the <see cref="PictureFile"/> class.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        public PictureFile(string path)
        {
            this.Path = path;

            using (var bmp = new Bitmap(this.Path))
            {
                this.propertyItems = bmp.PropertyItems.ToArray();
            }
        }

        /// <summary>
        /// Gets the path.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Gets the date when the picture was taken.
        /// </summary>
        public DateTime TakenDate 
        {
            get
            {
                try
                {
                    return this.GetDateTimeItem(TimeAttribute);
                }
                catch (Exception)
                {
                    return DateTime.MinValue;
                }
            }
        }

        /// <summary>
        /// Get a date time item.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        private DateTime GetDateTimeItem(int key)
        {
            var valueBytes = this.GetItem(key);
            if (valueBytes == null || valueBytes.Length < 7)
            {
                return DateTime.MinValue;
            }

            var dateString = Encoding.ASCII.GetString(valueBytes).Replace("\0", string.Empty);

            var regex = new Regex("^([\\d:]*) ([\\d:]*)$");
            if (!regex.IsMatch(dateString))
            {
                // What?
                return DateTime.MinValue;
            }

            var match = regex.Match(dateString);
            var dateGroup = match.Groups[1].Value;
            var timeGroup = match.Groups[2].Value;

            var newDateString = dateGroup.Replace(":", "-") + " " + timeGroup;

            return DateTime.Parse(newDateString);
        }

        /// <summary>
        /// Get the raw bytes for an item.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/> array.
        /// </returns>
        private byte[] GetItem(int key)
        {
            var property = this.propertyItems.FirstOrDefault(item => item.Id == key);
            return property == null ? null : property.Value;
        }
    }
}
