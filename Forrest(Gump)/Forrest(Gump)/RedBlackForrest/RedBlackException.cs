namespace RedBlackForrest
{
    using System;

    public class RedBlackException : Exception
    {
        public RedBlackException()
        {
        }
        
        public RedBlackException(string msg) : base(msg) 
        {
        }
    }
}
