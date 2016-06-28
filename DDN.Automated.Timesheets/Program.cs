using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System;
using System.IO;
using System.Threading;

namespace DDN.Automated.Timesheets
{
    public class Program
    {
        private static IWebDriver _WebDriver;

        private class UIMap
        {
            public string Frame { get; set; }

            public string New { get; set; }

            public string Save { get; set; }

            public string Cancel { get; set; }

            public string EmployeeName { get; set; }

            public string ProjectName { get; set; }

            public string ProjectConsole { get; set; }

            public string StartDate { get; set; }

            public string[] Days { get; set; }

            public string Overtime { get; set; }

            public string Comments { get; set; }
        }

        public static IWebDriver WebDriver
        {
            get
            {
                if (_WebDriver == null)
                {
                    _WebDriver = new InternetExplorerDriver(new InternetExplorerOptions
                    {
                        IgnoreZoomLevel = true,
                        InitialBrowserUrl = Config.Url,
                        RequireWindowFocus = false,
                        UnexpectedAlertBehavior = InternetExplorerUnexpectedAlertBehavior.Dismiss
                    });
                }

                return _WebDriver;
            }
        }

        public static void Main(string[] args)
        {
            var ui = default(UIMap);
            
            using (var driver = WebDriver)
            {
                driver.Manage().Window.Maximize();

                using (var stream = new StreamReader(Config.MapFile))
                {
                    ui = JsonConvert.DeserializeObject<UIMap>(stream.ReadToEnd());
                }

                driver.FindElement(By.CssSelector(ui.New)).Click();
                Thread.Sleep(5000);

                var frame = driver.FindElement(By.CssSelector(ui.Frame));

                driver.SwitchTo().Frame(frame);

                var employee = driver.FindElement(By.CssSelector(ui.EmployeeName));

                employee.SendKeys("Andrew Sheppard");
                employee.SendKeys(Keys.Enter);

                var project = driver.FindElement(By.CssSelector(ui.ProjectName));

                project.SendKeys(ui.ProjectConsole);
                project.SendKeys(Keys.Tab);

                var start = driver.FindElement(By.CssSelector(ui.StartDate));
                var date = DateTime.Now.Date;

                if (date.DayOfWeek > DayOfWeek.Monday)
                {
                    date = date.AddDays(-((int)date.DayOfWeek - 1));
                }
                else if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    date = date.AddDays(-7);
                }

                start.SendKeys(date.ToString("dd/MM/yyyy"));
                start.SendKeys(Keys.Tab);

                foreach (var day in ui.Days)
                {
                    var element = driver.FindElement(By.CssSelector(day));

                    element.Clear();
                    element.SendKeys("7");
                }

                driver.FindElement(By.CssSelector(ui.Save)).Click();
                Thread.Sleep(5000);

                driver.SwitchTo().DefaultContent();
                driver.Quit();
            }
        }
    }
}