using System.Configuration;

namespace DDN.Automated.Timesheets
{
    public static class Config
    {
        public static string Url => ConfigurationManager.AppSettings["Url"];

        public static string MapFile => ConfigurationManager.AppSettings["MapFile"];
    }
}