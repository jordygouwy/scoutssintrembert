using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScoutsWebsite.Json;
using ScoutsWebsite.Models;

namespace ScoutsWebsite.IRepositories
{
    public interface ICalendarRepository
    {
        void AddCalendarItem();

        List<CalendarShortItem> GetCalendaritemsCurrentMonth();
        List<WCalendarItem> GetCalendarItems(DateTime startdate, DateTime enddate);
        CalendarDetailItem GetCalendarItem(Guid id);

        List<CalendarDetailItem> GetCalendarItems();
        void AddOrUpdateCalendar(CalendarDetailItem model);
        void DeleteCalendar(Guid id);

    }
}