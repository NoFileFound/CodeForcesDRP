using System.Windows;

namespace CodeForcesDRP.api
{
    public partial class ErrorWindow : Window
    {
        public string Message { get; set; }

        public ErrorWindow()
        {
            InitializeComponent();
            this.Message = "";
            DataContext = this;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}