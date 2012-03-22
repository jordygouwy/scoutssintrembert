using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScoutsWebsite.IRepositories;
using ScoutsWebsite.Json;
using ScoutsWebsite.Models;
using ScoutsWebsite.Helpers;
using ScoutsWebsite.Enums;
using System.Text.RegularExpressions;

namespace ScoutsWebsite.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private ScoutsDataDataContext _db;

        public CalendarRepository(ScoutsDataDataContext db)
        {
            this._db = db;
        }

        public void AddCalendarItem()
        {
            //throw new NotImplementedException();
        }

        public List<WCalendarItem> GetCalendarItems(DateTime startdate, DateTime enddate)
        {
            List<WCalendarItem> result = new List<WCalendarItem>();

            result = (from x in _db.CalendarItems
                      where x.CalendarItemFrom.HasValue && (x.CalendarItemFrom.Value >= startdate || x.CalendarItemFrom <= enddate)
                      select new WCalendarItem()
                      {
                          title = x.CalendarItemTitle ?? "",
                          start = x.CalendarItemFrom.Value,
                          end = x.CalendarItemUntil.Value,
                          taktype = Utils.ToEnum((int)(x.CalendarItemType ?? 0), TakType.Groep),
                          id = x.CalendarItemID,
                          url = ""
                      }).ToList();


            return result;
        }


        public CalendarDetailItem GetCalendarItem(Guid id)
        {
            var result = (from x in _db.CalendarItems
                          where x.CalendarItemID == id
                          select new CalendarDetailItem()
                          {
                              CalendarItemDescription = x.CalendarItemDescription,
                              CalendarItemFrom = x.CalendarItemFrom,
                              CalendarItemID = x.CalendarItemID,
                              CalendarItemNeeds = x.CalendarItemNeeds,
                              CalendarItemTitle = x.CalendarItemTitle,
                              CalendarItemType = x.CalendarItemType,
                              CalendarItemUntil = x.CalendarItemUntil,
                              SetCalendarItemFrom = x.CalendarItemFrom,
                              SetCalenderItemUntil = x.CalendarItemUntil

                          }).FirstOrDefault();
            return result;
        }


        public List<CalendarShortItem> GetCalendaritemsCurrentMonth()
        {
            var result = (from x in _db.CalendarItems
                          where x.CalendarItemFrom.HasValue && (x.CalendarItemFrom.Value.Month == DateTime.Now.Month)
                          select new CalendarShortItem()
                          {
                              ID = x.CalendarItemID,
                              Type = x.CalendarItemType,
                              Title = x.CalendarItemTitle
                          }).ToList();
            return result;
        }


        public List<CalendarDetailItem> GetCalendarItems()
        {
            var result = (from x in _db.CalendarItems
                          select new CalendarDetailItem()
                          {
                              CalendarItemDescription = x.CalendarItemDescription,
                              CalendarItemFrom = x.CalendarItemFrom,
                              CalendarItemID = x.CalendarItemID,
                              CalendarItemNeeds = x.CalendarItemNeeds,
                              CalendarItemTitle = x.CalendarItemTitle,
                              CalendarItemType = x.CalendarItemType,
                              CalendarItemUntil = x.CalendarItemUntil,
                              SetCalendarItemFrom = x.CalendarItemFrom,
                              SetCalenderItemUntil = x.CalendarItemUntil

                          }).ToList();
            return result;
        }


        public void AddOrUpdateCalendar(CalendarDetailItem model)
        {
            var calendaritem = (from x in _db.CalendarItems where x.CalendarItemID == model.CalendarItemID select x).FirstOrDefault();
            if (calendaritem == null)
            {
                calendaritem = new CalendarItem();
                calendaritem.CalendarItemID = Guid.NewGuid();
                _db.CalendarItems.InsertOnSubmit(calendaritem);
            }

            calendaritem.CalendarItemDescription = model.CalendarItemDescription ?? "";
            calendaritem.CalendarItemFrom = GetDateTime(model.CalendarItemFromDay, model.CalendarItemFromHour);
            calendaritem.CalendarItemUntil = GetDateTime(model.CalendarItemUntilDay, model.CalendarItemUntilHour);
            calendaritem.CalendarItemTitle = model.CalendarItemTitle ?? "";
            calendaritem.CalendarItemType = model.CalendarItemType;
            _db.SubmitChanges();
        }

        private DateTime GetDateTime(DateTime? day, string hourstring)
        {
            if (!day.HasValue)
            {
                day = DateTime.Now;
            }


            int hour = 0;
            int minute = 0;

            List<string> hoursplit = Regex.Split(hourstring, ":").ToList();
            if (hoursplit.Count == 2)
            {
                if (int.TryParse((hoursplit[0] ?? "").ToString(), out hour))
                {
                    if (hour < 0 || hour > 23)
                    {
                        hour = 0;
                    }
                }

                if (int.TryParse((hoursplit[1] ?? "").ToString(), out minute))
                {
                    if (minute < 0 || minute > 59)
                    {
                        minute = 0;
                    }
                }               
            }

            return new DateTime(day.Value.Year, day.Value.Month, day.Value.Day, hour, minute, 0);

        }

        public void DeleteCalendar(Guid id)
        {
            var calendar = (from x in _db.CalendarItems
                            where x.CalendarItemID == id
                            select x).FirstOrDefault();
            if (calendar != null)
            {
                _db.CalendarItems.DeleteOnSubmit(calendar);
                _db.SubmitChanges();
            }
        }
    }
}