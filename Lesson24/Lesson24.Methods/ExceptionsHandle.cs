using System;
using System.Globalization;
using System.IO;

namespace Lesson24.Methods
{
    public class ExceptionsHandle
    {
        public static void Logging(string exMessage, string exStackTrace)
        {
            string strPath = $@"C:\Users\audri\Documents\Code_Academy_mokymai\lesson24\Lesson24\Lesson24\ErrorLog.txt";
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(strPath))
            {
                sw.WriteLine("=============Error Logging ===========");
                sw.WriteLine("===========Start============= " + DateTime.Now);
                sw.WriteLine("Error Message: " + exMessage);
                sw.WriteLine("Stack Trace: " + exStackTrace);
                sw.WriteLine("===========End============= " + DateTime.Now);

            }
        }
        public static Guid FromStringToGuid(string str)
        {
            try
            {
                return Guid.Parse(str);
            }
            catch (ArgumentNullException ex)
            {
                Logging(ex.Message, ex.StackTrace);
                return default;
            }
            catch (FormatException ex)
            {
                Logging(ex.Message, ex.StackTrace);
                return default;
            }
        }
        public static double FromStringToDouble(string str)
        {
            try
            {
                return Double.Parse(str, CultureInfo.InvariantCulture);
            }
            catch (FormatException ex)
            {
                Logging(ex.Message, ex.StackTrace);
                return default;
            }
            catch (ArgumentNullException ex)
            {
                Logging(ex.Message, ex.StackTrace);
                return default;
            }
            catch (ArgumentException ex)
            {
                Logging(ex.Message, ex.StackTrace);
                return default;
            }
        }
        public static DateTime FromStringToDateTime(string str)
        {
            try
            {
                return DateTime.Parse(str);
            }
            catch (FormatException ex)
            {
                Logging(ex.Message, ex.StackTrace);
                return default;
            }
            catch (ArgumentNullException ex)
            {
                Logging(ex.Message, ex.StackTrace);
                return default;
            }
        }
    }
}
