using System;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Medihelp.Graph.Core.Utils.Extensions
{
    public static class StringFormatExtension
    {
        public static DateTime? NULL_DATE = null;

        public const String DATE_FORMAT = "yyyy/MM/dd";
        public const String DATE_TIME_FORMAT = "yyyy/MM/dd HH:mm";
        public const String CURRENCY = "C";
        public const String EXCEL_RAND_FORMAT = @"_(  \R* #,##0.00_);_(-\R* #,##0.00;_(  \R* #,##0.00_);_(@_)";
        public const String EXCEL_TEXT_FORMAT = "@";
        public const String PERC_FORMAT = @"##0%";
        private const String DecimalSparator = ".";

        public static string FormatRands(string amount)
        {
            string randValue = string.Empty;

            try
            {
                randValue = !string.IsNullOrEmpty(System.Text.RegularExpressions.Regex.Replace(amount.Replace(" ", string.Empty), "[A-Za-z]", string.Empty)) ? string.Format(new CultureInfo("af-ZA", false), "{0:c}", FormatDecimal(System.Text.RegularExpressions.Regex.Replace(amount.Replace(" ", string.Empty), "[A-Za-z]", string.Empty))) : string.Format(new System.Globalization.CultureInfo("af-ZA", false), "{0:c}", 0);
            }
            catch (Exception)
            {
                randValue = string.Format(new CultureInfo("af-ZA", false), "{0:c}", 0);
            }

            return randValue;
        }

        public static decimal? FormatDecimal(string value)
        {
            if (!value.Contains("."))
                value = System.Text.RegularExpressions.Regex.Replace(value.Replace(" ", string.Empty), "[A-Za-z]", string.Empty);
            else
                value = System.Text.RegularExpressions.Regex.Replace(value.Replace(".", ",").Replace(" ", string.Empty), "[A-Za-z]", string.Empty);

            return Convert.ToDecimal(value, new CultureInfo("af-ZA", false));
        }

        public static string FormatModifiers(string modifier)
        {
            string retval = "";

            switch (modifier.ToLower())
            {
                case "t":
                    retval = string.Empty;
                    break;
                case "m":
                    retval = string.Empty;
                    break;
                default:
                    retval = modifier;
                    break;
            }

            return retval;
        }

        public static string FormatMoney(this decimal amount)
        {
            return amount.ToString(CURRENCY, new System.Globalization.CultureInfo("af-ZA", false));
        }


        public static string FormatMoney(this decimal? amount)
        {
            return amount.HasValue ? amount.Value.FormatMoney() : new decimal(0).FormatMoney();
        }


        public static string FormatDate(this DateTime date)
        {
            return !date.Year.Equals("1900") ? date.ToString(DATE_FORMAT) : string.Empty;
        }


        public static string FormatDate(this DateTime? date)
        {
            return date.HasValue ? date.Value.FormatDate() : string.Empty;
        }


        public static string FormatDateTime(this DateTime date)
        {
            return !date.Year.Equals("1900") ? date.ToString(DATE_TIME_FORMAT) : string.Empty;
        }


        public static string FormatDateTime(this DateTime? date)
        {
            return date.HasValue ? date.Value.FormatDateTime() : string.Empty;
        }


        public static string FormatSentenceCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            string sentence = s.ToLower();

            return sentence[0].ToString().ToUpper() + sentence.Substring(1);
        }


        public static string FormatTitleCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;

            return textInfo.ToTitleCase(s.ToLower());
        }


        public static string FormatTextOnly(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            return System.Text.RegularExpressions.Regex.Replace(s, "[^0-9a-zA-Z]+", string.Empty);
        }

        public static string FormatSurname(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            s = s.ToLower();

            if (s.Split(' ').Length > 1)
            {
                string surname = s.ToLower().Replace(s.Split(' ').Last(), FormatTitleCase(s.Split(' ').Last()));

                if (surname.Split(' ').First() == "mc")
                    surname = surname.Replace("mc", "Mc");

                return surname;
            }
            else
                return FormatTitleCase(s);
        }


        public static string FormatNumbersOnly(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            return System.Text.RegularExpressions.Regex.Replace(s, "[^0-9]", string.Empty);
        }

        public static string TranslateAgeGenderRestrictions(string restricion)
        {
            if (!String.IsNullOrEmpty(restricion))
                return restricion.ToLower().Replace("age", "Ouderdom").Replace("gender", "Geslag").Replace("gen", "Geslag");
            else
                return "";
        }

        public static string FormaAcronym(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;

            return textInfo.ToTitleCase(s.ToUpper());
        }
    }
}
