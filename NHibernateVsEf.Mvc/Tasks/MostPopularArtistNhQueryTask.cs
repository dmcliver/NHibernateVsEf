using NHibernateVsEf.Core.Repositories.NHibernate;

namespace NHibernateVsEf.Mvc.Tasks
{
    public class MostPopularArtistNhQueryTask : IQueryTask
    {
        private readonly IArtistRepositoryNh _trackRepositoryNh;

        public MostPopularArtistNhQueryTask(IArtistRepositoryNh trackRepositoryNh)
        {
            _trackRepositoryNh = trackRepositoryNh;
        }

        public void Execute()
        {
            _trackRepositoryNh.GetMostPopularArtist();
        }
    }
}