namespace RussianForrest
{
    using System;

    public class AvlException : Exception
    {
        public AvlException()
        {
        }

        public AvlException(string msg)
            : base(msg) 
        {
        }
    }
}
