namespace NHibernateVsEf.Mvc.Models
{
    public class QueryTimerModel
    {
        public long MostPopluarArtistNhTime { get; set; }
        public long UsersFromNzNhTime { get; set; }
        public long UsersWithoutMusicNhTime { get; set; }
        public long MostPopluarArtistEfTime { get; set; }
        public long UsersFromNzEfTime { get; set; }
        public long UsersWithoutMusicEfTime { get; set; }

        public QueryTimerModel(){}

        public QueryTimerModel(long mostPopluarArtistNhTime, long usersFromNzNhTime, long usersWithoutMusicNhTime, long mostPopluarArtistEfTime, long usersFromNzEfTime, long usersWithoutMusicEfTime)
        {
            MostPopluarArtistNhTime = mostPopluarArtistNhTime;
            UsersFromNzNhTime = usersFromNzNhTime;
            UsersWithoutMusicNhTime = usersWithoutMusicNhTime;
            MostPopluarArtistEfTime = mostPopluarArtistEfTime;
            UsersFromNzEfTime = usersFromNzEfTime;
            UsersWithoutMusicEfTime = usersWithoutMusicEfTime;
        }
    }
}