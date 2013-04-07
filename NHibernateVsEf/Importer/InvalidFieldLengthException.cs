using System;

namespace NHibernateVsEf.Importer
{
    public class InvalidFieldLengthException : Exception
    {
        public InvalidFieldLengthException(string message) : base(message) {}
    }
}