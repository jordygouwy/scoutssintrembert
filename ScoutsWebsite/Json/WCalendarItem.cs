using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScoutsWebsite.Enums;
using ScoutsWebsite.Helpers;

namespace ScoutsWebsite.Json
{
    public class WCalendarItem
    {
        public WCalendarItem()
        {

        }

        public Guid id { get; set; }
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string url { get; set; }
        public string classtype
        {
            get
            {
                return Utils.GetClassFromTakType(this.taktype);
            }
        }
        public TakType taktype { get; set; }
    }
}