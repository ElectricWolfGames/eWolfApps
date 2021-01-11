namespace RenameAllFilesInFolder
{
    public class ParseArgs
    {
        public ParseArgs(string[] args)
        {
            try
            {
                if (args.Length == 2)
                {
                    IsValid = true;
                }

                Replace = args[0];
                With = args[1];
            }
            catch
            {
                // fail safe
            }
        }

        public bool IsValid { get; private set; }

        public string Replace { get; private set; }

        public string With { get; private set; }
    }
}