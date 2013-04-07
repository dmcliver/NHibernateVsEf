using System.Diagnostics;
using NHibernateVsEf.Core.IocAttributes;
using NHibernateVsEf.Mvc.Tasks;

namespace NHibernateVsEf.Mvc.Controllers
{
    [IsInjected]
    public class TimerController : ITimerController
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public long ExecuteTimer(IQueryTask task)
        {
            _stopwatch.Start();
            task.Execute();
            _stopwatch.Stop();

            long time = _stopwatch.ElapsedMilliseconds;
            _stopwatch.Reset();
            return time;
        }
    }
}