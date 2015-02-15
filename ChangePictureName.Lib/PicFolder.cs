namespace Allberg.ChangePictureName.Lib
{
    using System.IO;

    public class PicFolder
    {
        private readonly string pictureFolder;

        public PicFolder(string pictureFolder)
        {
            this.pictureFolder = pictureFolder;
        }

        public delegate void FoundPicture(string path);

        public event FoundPicture OnFoundPicture;


        private void CallOnFoundPicture(string path)
        {
            var theEvent = this.OnFoundPicture;
            if (theEvent != null)
            {
                theEvent(path);
            }
        }

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
                foreach (var file in Directory.GetFiles(this.pictureFolder , "*." + extension))
                {
                    this.CallOnFoundPicture(file);
                }
            }
        }
    }
}
