
namespace LenovoBacklightImproved
{
    internal class DllNotSupportedException : Exception
    {
        public DllNotSupportedException()
        {
        }

        public DllNotSupportedException(string message)
            : base(message)
        {
        }

        public DllNotSupportedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
