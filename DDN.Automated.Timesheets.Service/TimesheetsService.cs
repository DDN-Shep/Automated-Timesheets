using System.ServiceProcess;

namespace DDN.Automated.Timesheets.Service
{
    public partial class TimesheetsService : ServiceBase
    {
        public TimesheetsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Process.Start() // Diagnostics(?)
        }

        protected override void OnStop()
        {
        }
    }
}
