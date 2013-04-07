using NHibernate;
using NHibernate.Criterion;
using NHibernateVsEf.Core.Domain;
using NHibernateVsEf.Core.Domain.NHibernate;
using NHibernateVsEf.Core.IocAttributes;

namespace NHibernateVsEf.Core.Repositories.NHibernate
{
    [Repository]
    public class ArtistRepositoryNh : RepositoryBase, IArtistRepositoryNh
    {
        public ArtistRepositoryNh(ISessionFactory sessionFactory) : base(sessionFactory) { }

        public ArtistRepositoryNh() : this(new SessionFactoryBuilder().BuildSessionFactory("thread_static")) { }

        /// <summary>
        /// Finds the artist by name
        /// </summary>
        public Artist FindByName(string artistName)
        {
            ISession session = SessionFactory.GetCurrentSession();
            return session.CreateCriteria<Artist>()
                .Add(Restrictions.Eq("Name", artistName))
                .SetMaxResults(1)
                .UniqueResult<Artist>();
        }

        /// <summary>
        /// Saves the artist down to the local persistant store and syncs it with the database
        /// </summary>
        public void Save(Artist artist)
        {
            ISession session = SessionFactory.GetCurrentSession();
            session.Save(artist);
            session.Flush();
        }

        /// <summary>
        /// Gets the most popular artist
        /// </summary>
        public ArtistTrackCount GetMostPopularArtist()
        {
            ISession session = SessionFactory.GetCurrentSession();
            object[] rawResult = session.CreateCriteria<Track>("t")
                        .CreateAlias("Artist", "a")
                        .SetProjection
                        (
                            Projections.ProjectionList()
                                .Add(Projections.GroupProperty("a.Name"))
                                .Add(Projections.Count("t.Id"))
                        )
                        .AddOrder(Order.Desc(Projections.Count("t.Id")))
                        .SetMaxResults(1)
                        .UniqueResult() as object[];
            return new ArtistTrackCount(rawResult[0].ToString(), (int) rawResult[1]);
        }
    }
}