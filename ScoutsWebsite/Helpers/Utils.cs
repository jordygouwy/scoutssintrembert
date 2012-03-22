using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Reflection;
using ScoutsWebsite.Enums;
using System.Web.Mvc;

namespace ScoutsWebsite.Helpers
{
    public static class Utils
    {
        public static string ConvertToXMLString<T>(this List<T> items, string itemlistname, string itemname)
        {
            if (items != null)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("<{0}>", itemlistname ?? "");
                
                foreach (T item in items)
                {
                    builder.AppendFormat("<{0} ", itemname ?? "");
                    Type type = item.GetType();
                    foreach (PropertyInfo propertyinfo in type.GetProperties())
                    {
                        builder.AppendFormat(" {0}='{1}' ", (propertyinfo.Name ?? "").ToLower(), ConvertToXMLValue(propertyinfo.GetValue(item, null)));
                    }
                    builder.AppendFormat("></{0}>", itemname ?? "");
                }

                builder.AppendFormat("</{0}>", itemlistname ?? "");
                return builder.ToString();
            }
            return "";
        }

        private static string ConvertToXMLValue(object value)
        {
            if (value != null)
            {
                Type type = value.GetType();
                if (type == typeof(DateTime))
                {
                    DateTime dvalue = (DateTime)value;
                    string year = dvalue.Year.ToString();
                    string month = (dvalue.Month > 9) ? (dvalue.Month).ToString() : "0" + dvalue.Month;
                    string day = (dvalue.Day > 9) ? (dvalue.Day).ToString() : "0" + dvalue.Day;
                    return year + "-" + month + "-" + day;
                }
                return value.ToString();
            }
            return "";
        }

        public static T ToEnum<T>(object value, T defaultValue)
        {
            T local;
            try
            {
                if (value == null)
                {
                    throw new Exception();
                }
                if (!Enum.IsDefined(typeof(T), value))
                {
                    throw new Exception();
                }
                return (T)value;
            }
            catch (Exception)
            {
                local = defaultValue;
            }
            return local;
        }

        public static string GetClassFromTakType(TakType type)
        {
            switch (type)
            {
                case TakType.Jins:
                    return "jin";
                    break;

                case TakType.Jongverkenners:
                    return "jvk";
                    break;

                case TakType.Kapoenen:
                    return "kap";
                    break;

                case TakType.Verkenners:
                    return "vks";
                    break;

                case TakType.Welpen:
                    return "wel";
                    break;
            }
            return "grp";
        }

        public static List<SelectListItem> GetTakTypeListItems()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            result.Add(new SelectListItem() { Text = "Groep", Value = ("" + (int)TakType.Groep) });
            result.Add(new SelectListItem() { Text = "Kapoenen", Value = ("" + (int)TakType.Kapoenen) });
            result.Add(new SelectListItem() { Text = "Welpen", Value = ("" + (int)TakType.Welpen) });
            result.Add(new SelectListItem() { Text = "Jongverkenners", Value = ("" + (int)TakType.Jongverkenners) });
            result.Add(new SelectListItem() { Text = "Verkenners", Value = ("" + (int)TakType.Verkenners) });
            result.Add(new SelectListItem() { Text = "Jins", Value = ("" + (int)TakType.Jins) });
            return result;
        }

        public static List<SelectListItem> GetYesNoListItems()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            result.Add(new SelectListItem() { Text = "Nee", Value = "False" });
            result.Add(new SelectListItem() { Text = "Ja", Value = "True" });
            return result;
        }
    }
}