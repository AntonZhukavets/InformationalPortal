using System;
using InformationalPortal.business.Interfaces;

namespace InformationalPortal.business.Implementations
{
    public class OperationResult : IOperationResult
    {
        private bool resultOfOperation;
        private string message;
        private string redirect;        
        public bool ResultOfOperation
        {
            get { return resultOfOperation; }
            set { resultOfOperation = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        public string Redirect
        {
            get { return redirect; }
            set { redirect = value; }
        }
    }
}