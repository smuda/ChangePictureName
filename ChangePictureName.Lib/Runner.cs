namespace Allberg.ChangePictureName.Lib
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    public class Runner
    {
        private readonly ProgramArguments settings;

        public Runner(ProgramArguments settings)
        {
            this.settings = settings;
        }

        public void RunNow()
        {
            Console.WriteLine("Starting to find pictures in " + this.settings.PictureFolder);
            var picFolder = new PicFolderHolder(this.settings.PictureFolder);
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
                    Console.WriteLine("Checked {0} and it's alredy done.");
                    continue;
                }

                var picture = new PictureFile(picturePath);
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
            }
        }

        private static string CreateFilename(DateTime takenDate)
        {
            return takenDate.ToString("yyyyMMdd_hhmmss");
        }
    }
}
