namespace BPtD_Lab_3.Files
{
    public class SourceCode
    {
        public SourceCode()
        {
            Source = "Source";
        }

        public string GetSomeString()
        {
            return Source + 123;
        }

        public string Source { get; set; }
    }
}
