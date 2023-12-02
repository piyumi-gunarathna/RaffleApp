namespace RaffleApp
{
    public class RaffleException : Exception
    {
        public RaffleException() : base()
        {
        }

        public RaffleException(string message) : base(message)
        {
        }

        public RaffleException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
