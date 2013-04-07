using System;
using System.ComponentModel;
using NHibernateVsEf.Annotations;
using NHibernateVsEf.Core.Domain.NHibernate;
using NHibernateVsEf.Core.Repositories.NHibernate;

namespace NHibernateVsEf.Importer
{
    public class ArtnameDataImporter : IDataImporter
    {
        private readonly ITrackRepositoryNh _trackRepositoryNh;
        private readonly IUserProfileRepositoryNh _userRepositoryNh;
        private readonly IArtistRepositoryNh _artistRepositoryNh;
        private readonly IUserTrackRepositoryNh _userTrackRepositoryNh;

        public ArtnameDataImporter([NotNull] ITrackRepositoryNh trackRepositoryNh, [NotNull] IUserProfileRepositoryNh userRepositoryNh, [NotNull] IArtistRepositoryNh artistRepositoryNh, [NotNull] IUserTrackRepositoryNh userTrackRepositoryNh)
        {
            if (trackRepositoryNh == null) throw new ArgumentNullException("trackRepositoryNh");
            if (userRepositoryNh == null) throw new ArgumentNullException("userRepositoryNh");
            if (artistRepositoryNh == null) throw new ArgumentNullException("artistRepositoryNh");
            if (userTrackRepositoryNh == null) throw new ArgumentNullException("userTrackRepositoryNh");

            _trackRepositoryNh = trackRepositoryNh;
            _userRepositoryNh = userRepositoryNh;
            _artistRepositoryNh = artistRepositoryNh;
            _userTrackRepositoryNh = userTrackRepositoryNh;
        }

        public ArtnameDataImporter() : this(new TrackRepositoryNh(), new UserProfileRepositoryNh(), new ArtistRepositoryNh(), new UserTrackRepositoryNh()) { }

        public void Import(string[] lines, BackgroundWorker worker)
        {
            int i = 0;

            foreach (var line in lines)
            {
                i++;
                string[] fields = line.Split(new[] { "\t" }, StringSplitOptions.None);

                if (fields.Length < 6)
                    throw new InvalidFieldLengthException("The amount of fields in the file should be at least 6");

                UserProfile user = _userRepositoryNh.FindById(fields[0]);

                Artist artist = _artistRepositoryNh.FindByName(fields[3]);
                if (artist == null)
                {
                    artist = new Artist { Name = fields[3] };
                    _artistRepositoryNh.Save(artist);
                }

                Track track = _trackRepositoryNh.FindByTitle(fields[5]);
                if (track == null)
                {
                    track = new Track { Title = fields[5], Artist = artist };
                    _trackRepositoryNh.Save(track);
                }

                UserTrack userTrack = new UserTrack { Track = track, UserProfile = user };
                _userTrackRepositoryNh.Save(userTrack);

                if (i%20 == 0)
                {
                    worker.ReportProgress(i);
                    _userTrackRepositoryNh.SyncDb();
                }

            }
        }
    }

}