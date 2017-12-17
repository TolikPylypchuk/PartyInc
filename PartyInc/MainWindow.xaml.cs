using System;
using System.Windows;

using PartyInc.Core;
using PartyInc.Properties;

using static PartyInc.Core.API;

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
				PartyOrganizer.HandleResponse);

			try
			{
				const string hi = "hi";

				this.AddLine(me, hi);
				this.AddLine(bot, await GetAsyncResult(Bot.RespondAsync(botInfo, hi)));

				const string organize = "I want to organize a party";

				this.AddLine(me, organize);
				this.AddLine(bot, await GetAsyncResult(Bot.RespondAsync(botInfo, organize)));
			} catch (Exception exp)
			{
				MessageBox.Show(
					exp.Message,
					"Error",
					MessageBoxButton.OK,
					MessageBoxImage.Error);
			}
		}

		private void AddLine(string party, string text = "")
		{
			this.test.Text += $"{party}: {text}\n";
		}
    }
}
