using System;
using System.Runtime.Serialization;

namespace CashpointWPF.ATMLogic
{
    [Serializable]
    public class WithdrawException : Exception
    {
        public ATMError Error { get; set; }

        public WithdrawException(ATMError error)
        {
            Error = error;
        }

        public WithdrawException(ATMError error, string message) : base(message)
        {
            Error = error;
        }

        public WithdrawException(ATMError error,
            string message,
            Exception inner) : base(message, inner)
        {
            Error = error;
        }

        protected WithdrawException(
          SerializationInfo info,
          StreamingContext context) : base(info, context)
        {

        }
    }
}