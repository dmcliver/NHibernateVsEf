using NHibernate;
using NHibernate.Criterion;
using NHibernateVsEf.Core.Domain;
using NHibernateVsEf.Core.Domain.NHibernate;
using NHibernateVsEf.Core.IocAttributes;
using NHibernateVsEf.Core.Repositories.EntityFramework;

namespace NHibernateVsEf.Core.Repositories.NHibernate
{
    [Repository]
    public class ArtistRepositoryNh : RepositoryBase, IArtistRepositoryNh
    {
        public ArtistRepositoryNh(ISessionFactory sessionFactory) : base(sessionFactory) { }

        public ArtistRepositoryNh() : this(new SessionFactoryBuilder().BuildSessionFactory("thread_static")) { }

        public Artist FindByName(string s)
        {
            ISession session = SessionFactory.GetCurrentSession();
            return session.CreateCriteria<Artist>()
                .Add(Restrictions.Eq("Name", s))
                .SetMaxResults(1)
                .UniqueResult<Artist>();
        }

        public void Save(Artist artist)
        {
            ISession session = SessionFactory.GetCurrentSession();
            session.Save(artist);
            session.Flush();
        }

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