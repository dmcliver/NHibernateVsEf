using NHibernateVsEf.Mvc.Tasks;

namespace NHibernateVsEf.Mvc.Controllers
{
    public interface ITimerController
    {
        long ExecuteTimer(IQueryTask task);
    }
}