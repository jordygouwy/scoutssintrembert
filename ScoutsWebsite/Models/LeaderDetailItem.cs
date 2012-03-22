using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScoutsWebsite.Enums;
using ScoutsWebsite.Helpers;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ScoutsWebsite.Models
{
    public class LeaderDetailItem : IValidatableObject
    {
        public LeaderDetailItem()
        {

        }

        public string CheckTakLeader
        {
            get
            {
                if (this.LeaderTakLeader)
                {
                    return " (Takleider)";
                }
                return "";
            }
        }

        [Display(Name = "Groep")]
        public int Type { get; set; }
        [Display(Name = "Tak")]
        public TakType TakType
        {
            get { return Utils.ToEnum((int)Type, TakType.Groep); }
        }
        public Guid LeaderID { get; set; }
        
        [Display(Name = "Voornaam")]
        public string LeaderFirstName { get; set; }
        [Display(Name = "Naam")]
        public string LeaderLastName { get; set; }
        [Display(Name = "E-mail")]
        public string LeaderEmail { get; set; }
        [Display(Name = "Telefoon")]
        public string LeaderPhone { get; set; }
        [Display(Name = "Takleider")]
        public bool LeaderTakLeader { get; set; }
        [Display(Name= "Afbeelding")]
        public HttpPostedFileBase File{get;set;}

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> result = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.LeaderFirstName))
            {
                result.Add(new ValidationResult("Geen voornaam ingevuld"));
            }

            if (string.IsNullOrEmpty(this.LeaderLastName))
            {
                result.Add(new ValidationResult("Geen achternaam ingevuld"));
            }

            if (string.IsNullOrEmpty(this.LeaderEmail))
            {
                result.Add(new ValidationResult("Geen e-mail ingevuld"));
            }

            List<string> extensions = new List<string>() {".gif", ".png", ".bmp", ".jpg", ".jpeg" };
            if (this.File != null && this.File.ContentLength > 0)
            {
                if (!extensions.Contains(Path.GetExtension(this.File.FileName)))
                {
                    result.Add(new ValidationResult("Gekozen bijlage is geen afbeelding!"));
                }
            }

            return result;
        }
    }
}