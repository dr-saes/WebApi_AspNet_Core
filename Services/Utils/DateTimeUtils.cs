
using System.Globalization;

namespace WebApi_AspNet_Core;

public class DatetimeUtils
{
    public static DateTime getCurrentDateTime()
    {
        DateTime dateTime = DateTime.UtcNow;
        TimeZoneInfo hrBrasilia = null;
        try
        {
            hrBrasilia = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            hrBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        }
        return TimeZoneInfo.ConvertTimeFromUtc(dateTime, hrBrasilia);
    }

    public static DateTime ConvertStringToDate(string strDate)
    {
        var cultureInfo = new CultureInfo("pt-BR");
        return DateTime.Parse(strDate, cultureInfo);
    }

    public static string ConvertDateToString(DateTime date)
    {
        return date.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
    }

    public static string ConvertDateToStringAllFormats(string date)
    {
        string newFormat = "yyyy-MM-dd";
        return ConvertDateToStringAllFormats(date, newFormat);
    }

    public static string ConvertDateToStringAllFormats(string date, string newFormat)
    {

        string[] formats = { "dd-MM-yyyy", "dd/MM/yyyy", "yyyy-MM-dd", "yyyy/MM/dd" };


        if (DateTime.TryParseExact(date, formats, null, DateTimeStyles.None, out DateTime dataConvertida))
        {
            return dataConvertida.ToString(newFormat);
        }
        else
        {
            throw new ArgumentException("Formato de data inválido");
        }
    }

    public static void DateTreatment(ref string minDate, ref string maxDate, out DateTime initialDate, out DateTime finalDate)
    {
        if (minDate != string.Empty && minDate != null)
        {
            minDate = ConvertDateToStringAllFormats(minDate);
            initialDate = ConvertStringToDate(minDate);
        }
        else initialDate = DateTime.MinValue;

        if (maxDate == string.Empty && maxDate == null)
        {
            maxDate = ConvertDateToStringAllFormats(maxDate);
            finalDate = ConvertStringToDate(maxDate);
        }
        else finalDate = getCurrentDateTime().AddDays(1);
    }
}
