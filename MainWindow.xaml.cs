using System;
using System.Windows;
using Newtonsoft.Json.Linq;
using CodeForcesDRP;

namespace CodeForcesDRP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private api.Problem _problem;
        private bool isPrivateSolution;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void FindContestProblem_Click(object sender, RoutedEventArgs e)
        {
            set_discord_status.IsEnabled = false;

            if (problem_letter.Text.Length == 0)
            {
                api.ApiImplementation.InvokeError("CodeForces", "Please select problem id.");
                return;
            }
            if (contest_name.Text.Length == 0)
            {
                api.ApiImplementation.InvokeError("CodeForces", "Please select contest id.");
                return;
            }
            api.ApiImplementation apiImplementation = new();
            JObject response = await api.ApiImplementation.GetProblemsSet();
            if(response != null)
            {
                this._problem = apiImplementation.GetContestProblemInfo(response, Int32.Parse(contest_name.Text), problem_letter.Text[0]);
                if (this._problem.Type != null)
                {
                    set_discord_status.IsEnabled = true;
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

        private async void SetDiscordStatus_Click(object sender, RoutedEventArgs e)
        {
            await discord.Drp.SetStatus(_problem, problem_letter.Text[0], this.isPrivateSolution);
        }
    }
}
