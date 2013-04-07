namespace NHibernateVsEf.Core.Domain
{
    public class ArtistTrackCount
    {
        public string Name { get; private set; }
        public int Count { get; private set; }

        public ArtistTrackCount(string key, int count)
        {
            Name = key;
            Count = count;
        }
    }
}