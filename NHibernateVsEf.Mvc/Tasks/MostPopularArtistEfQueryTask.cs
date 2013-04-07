using System;
using NHibernateVsEf.Core.Repositories.EntityFramework;

namespace NHibernateVsEf.Mvc.Tasks
{
    public class MostPopularArtistEfQueryTask : IQueryTask
    {
        private readonly IArtistRepositoryEf _artistRepositoryEf;

        public MostPopularArtistEfQueryTask(IArtistRepositoryEf artistRepositoryEf)
        {
            if (artistRepositoryEf == null) throw new ArgumentNullException("artistRepositoryEf");
            _artistRepositoryEf = artistRepositoryEf;
        }

        public void Execute()
        {
            _artistRepositoryEf.GetMostPopularArtist();
        }
    }
}