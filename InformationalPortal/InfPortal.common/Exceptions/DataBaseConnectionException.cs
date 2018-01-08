using System;


namespace InfPortal.common.Exceptions
{
    public class DataBaseConnectionException : Exception
    {
        public DataBaseConnectionException(string message)
            : base(message)
        { }       
        
    }
}
