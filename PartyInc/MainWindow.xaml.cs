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
			const string me = " me";
			const string bot = "bot";

			var botInfo = new BotInfo(
				Settings.Default.PartyOrganizerId,
				Settings.Default.PartyOrganizerSubscriptionKey,
				PartyOrganizer.ManageResponse);

			const string hi = "hi";

			this.AddLine(me, hi);
			this.AddLine(bot, await Bot.RespondAsync(botInfo, hi));
			
			const string organize = "I want to organize a party";

			this.AddLine(me, organize);
			this.AddLine(bot, await Bot.RespondAsync(botInfo, organize));
		}

		private void AddLine(string party, string text = "")
		{
			this.test.Text += $"{party}: {text}\n";
		}
    }
}
