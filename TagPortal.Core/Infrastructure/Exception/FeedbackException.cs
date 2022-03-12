using System;

namespace TagPortal.Core
{
    public class FeedbackException : Exception
    {
        public FeedbackException() : base()
        {
        }
        public FeedbackException(string message) : base(message)
        {
        }
    }
}
