using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScoutsWebsite.Enums;
using ScoutsWebsite.Helpers;

namespace ScoutsWebsite.Models
{
    public class CalendarShortItem
    {
        public CalendarShortItem()
        {
            this.Type = (int)Enums.TakType.Groep;
        }

        public Guid ID { get; set; }
        public string Title { get; set; }
        public int? Type { get; set; }
        public TakType TakType
        {
            get
            {
                return Utils.ToEnum((int)(this.Type ?? 0), TakType.Groep);
            }

        }
    }
}