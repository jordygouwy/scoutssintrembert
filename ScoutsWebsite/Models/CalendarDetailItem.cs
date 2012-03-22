using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScoutsWebsite.Enums;
using ScoutsWebsite.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ScoutsWebsite.Models
{
    public class CalendarDetailItem
    {
        private string GetTimeString(DateTime? date)
        {
            string hour = "00";
            string minute = "00";
            if (date.HasValue)
            {
                if (date.Value.Hour > 9)
                {
                    hour = date.Value.Hour.ToString();
                }
                else
                {
                    hour = ("0" + date.Value.Hour.ToString());
                }

                if (date.Value.Minute > 9)
                {
                    minute = date.Value.Minute.ToString();
                }
                else
                {
                    minute = ("0" + date.Value.Minute.ToString());
                }
            }
            return hour + ":" + minute;
        }
        public DateTime? SetCalendarItemFrom
        {
            set
            {
                this.CalendarItemFrom = value;
                if (value.HasValue)
                {
                    this.CalendarItemFromDay = value;
                    this.CalendarItemFromHour = GetTimeString(value);
                }
            }
        }

        public DateTime? SetCalenderItemUntil
        {
            set
            {
                this.CalendarItemUntil = value;
                if (value.HasValue)
                {
                    this.CalendarItemUntilDay = value;
                    this.CalendarItemUntilHour = GetTimeString(value);
                }
            }
        }

        public Guid CalendarItemID { get; set; }
        public string CalendarItemTitle { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CalendarItemFromDay { get; set; }
        public string CalendarItemFromHour { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CalendarItemUntilDay { get; set; }
        public string CalendarItemUntilHour { get; set; }

        public DateTime? CalendarItemFrom { get; set; }
        public DateTime? CalendarItemUntil { get; set; }
        public int? CalendarItemType { get; set; }
        public TakType CalendarItemTakType
        {
            get { return Utils.ToEnum((int)(this.CalendarItemType ?? 0), TakType.Groep); }
        }
        public string CalendarItemNeeds { get; set; }
        public string CalendarItemDescription { get; set; }

        public DateTimeDisplay CalendarItemFromDisplay
        {
            get
            {
                DateTimeDisplay result = new DateTimeDisplay();
                result.Time = (this.CalendarItemFrom ?? DateTime.Now);
                result.Title = "Van";
                return result;
            }
        }

        public DateTimeDisplay CalendarItemUntilDisplay
        {
            get
            {
                DateTimeDisplay result = new DateTimeDisplay();
                result.Time = (this.CalendarItemUntil ?? DateTime.Now);
                result.Title = "Tot";
                return result;
            }
        }
    }

    public class DateTimeDisplay
    {
        public string Title { get; set; }
        public DateTime Time { get; set; }

    }
}