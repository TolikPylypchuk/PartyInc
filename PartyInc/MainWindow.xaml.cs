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
            this.test.Text = await Luis.request(
                Settings.Default.SweetsOrderConsultantId,
                Settings.Default.SweetsOrderConsultantSubscriptionKey,
                "I really like chocolate and bananas!");

            Luis.Response response = Luis.parseResponse(this.test.Text);
            this.test.Text = response.ToString();
        }
    }
}
