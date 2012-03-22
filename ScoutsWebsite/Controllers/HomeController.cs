using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScoutsWebsite.Json;
using System.Text;
using ScoutsWebsite.Helpers;
using ScoutsWebsite.IRepositories;
using ScoutsWebsite.Repositories;
using ScoutsWebsite.Enums;
using ScoutsWebsite.Models;

namespace ScoutsWebsite.Controllers
{
    public class HomeController : Controller
    {

        private ICalendarRepository _repoCalendar;
        private IPostRepository _repoNewsItem;
        private ILeaderRepository _repoLeader;

        public HomeController(ICalendarRepository repoCalendar, IPostRepository repoNewsItem,
            ILeaderRepository repoLeader)
        {
            _repoCalendar = repoCalendar;
            _repoNewsItem = repoNewsItem;
            _repoLeader = repoLeader;
        }

        public ActionResult Index()
        {
            var listitems = _repoNewsItem.GetLatestNewsItems();           
            return View(listitems);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult Album()
        {
            return View();
        }

        public ActionResult Event(string id)
        {
            Guid guid = Guid.Empty;
            if (Guid.TryParse(id, out guid))
            {
                return View(_repoCalendar.GetCalendarItem(guid));
            }
            return View();
        }

        public ActionResult Takken()
        {
            List<TakModel> takken = new List<TakModel>();

            #region KAPOENEN
            TakModel kapoenen = new TakModel();
            kapoenen.ClassName = "kapoenen";
            kapoenen.Title = "Kapoenen";
            kapoenen.Type = TakType.Kapoenen;
            takken.Add(kapoenen);
            #endregion

            #region WELPEN
            TakModel welpen = new TakModel();
            welpen.ClassName = "welpen";
            welpen.Title = "Welpen";
            welpen.Type = TakType.Welpen;
            takken.Add(welpen);
            #endregion

            #region JONGVERKENNERS
            TakModel jongverkenners = new TakModel();
            jongverkenners.ClassName = "jongverkenners";
            jongverkenners.Title = "Jongverkenners";
            jongverkenners.Type = TakType.Jongverkenners;
            takken.Add(jongverkenners);
            #endregion

            #region VERKENNERS
            TakModel verkenners = new TakModel();
            verkenners.ClassName = "verkenners";
            verkenners.Title = "Verkenners";
            verkenners.Type = TakType.Verkenners;
            takken.Add(verkenners);
            #endregion

            #region JINS
            TakModel jins = new TakModel();
            jins.ClassName = "jins";
            jins.Title = "Jins";
            jins.Type = TakType.Jins;
            takken.Add(jins);
            #endregion

            return View(takken);
        }

        public ActionResult Leaders()
        {
            return View(_repoLeader.GetLeaders());
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetLatestCalendarItems()
        {
            List<CalendarShortItem> items = _repoCalendar.GetCalendaritemsCurrentMonth();
            //items.Add(new CalendarShortItem() { Title = "12 februari" });
            //items.Add(new CalendarShortItem() { Title = "13 febdfgsdgdfsgruari" });
            //items.Add(new CalendarShortItem() { Title = "14 februari" });
            //items.Add(new CalendarShortItem() { Title = "15 dfd" });
            //items.Add(new CalendarShortItem() { Title = "16 febdfgdsfgdfgdsgdsfgruari" });
            //items.Add(new CalendarShortItem() { Title = "17 februari" });
            return PartialView("CalendarItemList", items);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetEvents(string startdate, string enddate)
        {
            string result = "";
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;

            if (DateTime.TryParse(startdate ?? "", out start) && DateTime.TryParse(enddate ?? "", out end))
            {
                List<WCalendarItem> calenderitems = _repoCalendar.GetCalendarItems(start, end);

            
                result = calenderitems.ConvertToXMLString("events", "event");
            }
            return this.Content(result, "text/xml");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FindTak()
        {
            TakModel tak = new TakModel();
            tak.FullDescription = "HIER KOMT DE OMSCHRIJVING VAN DE TAK";
            tak.ClassName = "kapoenen";
            tak.Title = "DIT IS DE TITEL";
            return PartialView("TakViewModel", tak);
        }
    }
}
