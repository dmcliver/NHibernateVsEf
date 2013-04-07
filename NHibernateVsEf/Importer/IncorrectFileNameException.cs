using System;

namespace NHibernateVsEf.Importer
{
    public class IncorrectFileNameException : Exception
    {
        public IncorrectFileNameException(string format, ArgumentNullException ex) : base(format, ex){}
    }
}