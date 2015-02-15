namespace Allberg.ChangePictureName.Lib
{
    using System.Collections.Generic;

    public class ProgramArguments
    {
        public ProgramArguments(string[] args)
        {
            this.IsEmpty = args.Length == 0;
            if (this.IsEmpty)
            {
                return;
            }

            var pictureFolders = new List<string>();

            foreach (var argument in args)
            {
                pictureFolders.Add(argument);
            }

            this.PictureFolders = pictureFolders.ToArray();
        }

        public bool IsEmpty { get; private set; }

        public string[] PictureFolders { get; private set; }
    }
}
