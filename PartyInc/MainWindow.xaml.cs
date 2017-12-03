using System.Windows;

using PartyInc.Core;
using PartyInc.Properties;

namespace PartyInc
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			this.InitializeComponent();
		}

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.test.Text = await Luis.requestTask(
                Settings.Default.SweetsOrderConsultantId,
                Settings.Default.SweetsOrderConsultantSubscriptionKey,
                "Hello!");
        }
    }
}
