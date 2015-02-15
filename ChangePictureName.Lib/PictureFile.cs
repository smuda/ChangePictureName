namespace Allberg.ChangePictureName.Lib
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class PictureFile
    {
        private const int TimeAttribute = 0x9003;
        private readonly PropertyItem[] propertyItems;
        public PictureFile(string path)
        {
            this.Path = path;

            using (var bmp = new Bitmap(this.Path))
            {
                this.propertyItems = bmp.PropertyItems.ToArray();
            }
        }

        public string Path { get; set; }

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

        private DateTime GetDateTimeItem(int key)
        {
            var valueBytes = this.GetItem(key);
            if (valueBytes == null || valueBytes.Length < 7)
            {
                return DateTime.MinValue;
            }

            var dateString = Encoding.ASCII.GetString(valueBytes).Replace("\0", "");

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


        private byte[] GetItem(int key)
        {
            var property = this.propertyItems.FirstOrDefault(item => item.Id == key);
            return property == null ? null : property.Value;
        }
    }
}
