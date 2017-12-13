using System.Windows;

using PartyInc.Properties;

using PartyInc.Core.Bots;

namespace PartyInc
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			this.InitializeComponent();
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LuisBot.LuisBot bot;
            bot = new SweetsOrderConsultantBot.SweetsOrderConsultantBot(
                Settings.Default.SweetsOrderConsultantId,
                Settings.Default.SweetsOrderConsultantSubscriptionKey);

            this.test.Text = bot.Response("Order a cake"); //I think, about 1.5 kg
        }
    }
}
