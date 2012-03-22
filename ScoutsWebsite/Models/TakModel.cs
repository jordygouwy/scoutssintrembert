using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScoutsWebsite.Enums;

namespace ScoutsWebsite.Models
{
    public class TakModel
    {
        public TakType Type { get; set; }
        public string ClassName { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
    }
}