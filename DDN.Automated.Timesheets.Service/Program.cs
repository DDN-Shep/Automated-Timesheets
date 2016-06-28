using System.ServiceProcess;

namespace DDN.Automated.Timesheets.Service
{
    public static class Program
    {
        public static void Main()
        {
            ServiceBase.Run(new ServiceBase[]
            {
                new TimesheetsService()
            });
        }
    }
}