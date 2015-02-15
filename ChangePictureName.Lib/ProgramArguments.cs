namespace Allberg.ChangePictureName.Lib
{
    public class ProgramArguments
    {
        public ProgramArguments(string[] args)
        {
            this.IsEmpty = args.Length == 0;
            if (this.IsEmpty)
            {
                return;
            }

            this.PictureFolder = args[0];
        }

        public bool IsEmpty { get; private set; }

        public string PictureFolder { get; private set; }
    }
}
