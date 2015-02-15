namespace Allberg.ChangePictureName.Lib
{
    using System.Collections.Generic;

    public class PicFolderHolder
    {
        private readonly string pictureFolder;

        public IList<string> Pictures { get; private set; }

        public PicFolderHolder(string pictureFolder)
        {
            this.pictureFolder = pictureFolder;
        }

        public void ScanForPictures()
        {
            this.Pictures = new List<string>();
            var picFolder = new PicFolder(this.pictureFolder);
            picFolder.OnFoundPicture += this.picFolder_OnFoundPicture;
            picFolder.ScanForPictures();
        }

        void picFolder_OnFoundPicture(string path)
        {
            this.Pictures.Add(path);
        }
    }
}
