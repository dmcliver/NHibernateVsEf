using System;

namespace NHibernateVsEf.Core.IocAttributes
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface)]
    public class RepositoryAttribute : Attribute
    {
    }
}