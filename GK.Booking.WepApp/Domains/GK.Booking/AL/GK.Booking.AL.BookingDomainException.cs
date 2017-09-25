using System;

namespace GK.Booking.AL
{
    public class BookingDomainException : ApplicationException
    {
        public int ExceptionCode { get; }

        public BookingDomainException(int exceptionCode)
        {
            ExceptionCode = exceptionCode;
        }

        public BookingDomainException(int exceptionCode, string message) : base(message)
        {
            ExceptionCode = exceptionCode;
        }

        public BookingDomainException(int exceptionCode, string message, Exception innerException) : base(message, innerException)
        {
            ExceptionCode = exceptionCode;
        }
    }
}