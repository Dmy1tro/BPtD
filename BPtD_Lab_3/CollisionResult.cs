namespace BPtD_Lab_3
{
    public class CollisionResult
    {
        public CollisionResult(string str, long tries)
        {
            CollisionString = str;
            CountOfTries = tries;
            Complete = true;
        }

        public CollisionResult()
        {
            Complete = false;
        }

        public string CollisionString { get; set; }

        public long CountOfTries { get; set; }

        public bool Complete { get; set; }
    }
}
