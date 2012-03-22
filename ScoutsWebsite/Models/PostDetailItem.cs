using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ScoutsWebsite.Models
{
    public class PostDetailItem
    {
        public Guid PostId { get; set; }
        [Display(Name = "Onderwerp")]
        public string PostSubject { get; set; }
        [Display(Name = "Bericht")]
        public string PostContent { get; set; }
        [Display(Name = "Datum")]
        public DateTime? PostDate { get; set; }

    }
}