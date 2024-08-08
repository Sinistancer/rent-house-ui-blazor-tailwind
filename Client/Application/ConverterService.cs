using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace daryon_house_ui.Client.Application
{
    public class ConverterService
    {
        public ConverterService()
        {

        }

        public string CurrencyString(decimal d)
        {
            var cultureString = "id-ID";
            CultureInfo culture = new CultureInfo(cultureString, false);
            NumberFormatInfo numberFormatInfo = (NumberFormatInfo)culture.NumberFormat.Clone();
            numberFormatInfo.CurrencyPositivePattern = 2;
            numberFormatInfo.CurrencySymbol = "Rp";

            string formattedAmount = string.Format(numberFormatInfo, "{0:C2}", d);
            formattedAmount = formattedAmount.Replace('.', '#').Replace(',', '.').Replace('#', ',');

            return formattedAmount;
        }

        public string MonthName(int month)
        {
            var cultureString = "id-ID";

            CultureInfo culture = new CultureInfo(cultureString, false);
            return culture.DateTimeFormat.GetMonthName(month);
        }

        public string ShowMonthYearString(string data)
        {
            string[] subs = data.Split('|');

            return $"{MonthName(int.Parse(subs[0]))} - {subs[1]}";
        }

        public MarkupString StringCommaToPoint(string? data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return new MarkupString();
            }

            string[]? parts = data?.Split(new string[] { ", " }, StringSplitOptions.None);

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append("<ul class=\"list-disc pl-1\">\n");

            foreach (string part in parts)
            {
                htmlBuilder.AppendFormat("    <li>{0}</li>\n", part.Trim());
            }

            htmlBuilder.Append("</ul>");

            return new MarkupString(htmlBuilder.ToString());


        }
    }
}
