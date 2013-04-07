using System;
using System.Web.Mvc;
using NHibernateVsEf.Core.Repositories.EntityFramework;
using NHibernateVsEf.Core.Repositories.NHibernate;
using NHibernateVsEf.Mvc.Models;
using NHibernateVsEf.Mvc.Tasks;

namespace NHibernateVsEf.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArtistRepositoryNh _artistRepositoryNh;
        private readonly IUserProfileRepositoryNh _userProfileRepositoryNh;
        private readonly IArtistRepositoryEf _artistRepositoryEf;
        private readonly IUserProfileRepositoryEf _userProfileRepositoryEf;
        private readonly ITimerController _timerController;

        public HomeController(IArtistRepositoryNh artistRepositoryNh, IUserProfileRepositoryNh userProfileRepositoryNh, IArtistRepositoryEf artistRepositoryEf, IUserProfileRepositoryEf userProfileRepositoryEf, ITimerController timerController)
        {
            if (artistRepositoryNh == null) throw new ArgumentNullException("artistRepositoryNh");
            if (userProfileRepositoryNh == null) throw new ArgumentNullException("userProfileRepositoryNh");
            if (artistRepositoryEf == null) throw new ArgumentNullException("artistRepositoryEf");
            if (userProfileRepositoryEf == null) throw new ArgumentNullException("userProfileRepositoryEf");
            if (timerController == null) throw new ArgumentNullException("timerController");

            _artistRepositoryNh = artistRepositoryNh;
            _userProfileRepositoryNh = userProfileRepositoryNh;
            _artistRepositoryEf = artistRepositoryEf;
            _userProfileRepositoryEf = userProfileRepositoryEf;
            _timerController = timerController;
        }

        public ActionResult Index()
        {
            long mostPopluarArtistNhTime = _timerController.ExecuteTimer(new MostPopularArtistNhQueryTask(_artistRepositoryNh));
            long usersFromNzNhTime = _timerController.ExecuteTimer(new UsersFromNzNhQueryTask(_userProfileRepositoryNh));
            long usersWithoutMusicNhTime = _timerController.ExecuteTimer(new UsersWithoutMusicNhQueryTask(_userProfileRepositoryNh));

            long mostPopluarArtistEfTime = _timerController.ExecuteTimer(new MostPopularArtistEfQueryTask(_artistRepositoryEf));
            long usersFromNzEfTime = _timerController.ExecuteTimer(new UsersFromNzEfQueryTask(_userProfileRepositoryEf));
            long usersWithoutMusicEfTime = _timerController.ExecuteTimer(new UsersWithoutMusicEfQueryTask(_userProfileRepositoryEf));

            return View(new QueryTimerModel(mostPopluarArtistNhTime, usersFromNzNhTime, usersWithoutMusicNhTime, mostPopluarArtistEfTime, usersFromNzEfTime, usersWithoutMusicEfTime));
        }
    }
}
