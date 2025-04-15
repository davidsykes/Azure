namespace DatabaseAccessInterfaces
{
    public class SQLiteLibraryException : Exception
    {
        public SQLiteLibraryException()
        {
        }

        public SQLiteLibraryException(string message)
            : base(message)
        {
        }

        public SQLiteLibraryException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}