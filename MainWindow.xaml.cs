using System;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace CodeForcesDRP
{
    public partial class MainWindow : Window
    {
        private api.Problem _problem;
        private bool isPrivateSolution;
        private parser.ConfigInfo _config;

        public MainWindow()
        {
            var parser = new parser.Config();
            this._config = parser.ReadConfig();
            if (this._config.ApplicationID == "")
            {
                parser.WriteNewConfig();
                api.ApiImplementation.InvokeError("CodeForcesDRP", "Please open again.");
                Environment.Exit(0);
            }
            InitializeComponent();
        }

        private async void FindContestProblem_Click(object sender, RoutedEventArgs e)
        {
            this.set_discord_status.IsEnabled = false;

            if (this.problem_letter.Text.Length == 0)
            {
                api.ApiImplementation.InvokeError("CodeForces", "Please select a problem id.");
                return;
            }
            if (this.contest_name.Text.Length == 0)
            {
                api.ApiImplementation.InvokeError("CodeForces", "Please select a contest id.");
                return;
            }
            api.ApiImplementation apiImplementation = new();
            JObject response = await api.ApiImplementation.GetProblemsSet();
            if(response != null)
            {
                this._problem = apiImplementation.GetContestProblemInfo(response, Int32.Parse(contest_name.Text), problem_letter.Text[0]);
                if (this._problem.Type != null)
                {
                    this.set_discord_status.IsEnabled = true;
                }
            }
        }

        private void SetPrivateSolution_Checked(object sender, RoutedEventArgs e)
        {
            this.isPrivateSolution = true;
        }

        private void SetPrivateSolution_Unchecked(object sender, RoutedEventArgs e)
        {
            this.isPrivateSolution = false;
        }

        private void SetDiscordStatus_Click(object sender, RoutedEventArgs e)
        {
            discord.Drp.InitializationRPC(this._config, this._problem, this.problem_letter.Text[0], this.isPrivateSolution);
            this.set_discord_status.IsEnabled = false;
        }
    }
}
