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

			var botInfo = new BotInfo<PartyOrganizerState>(
				Settings.Default.PartyOrganizerId,
				Settings.Default.PartyOrganizerSubscriptionKey,
				PartyOrganizer.HandleResponse);

			try
			{
				this.AddLine(
					bot,
					"Welcome to Party Inc! We'll help you organize " +
					"a mind-blowing party for kids!");

				const string hi = "hi";

				this.AddLine(me, hi);
				var (response, state) = await GetAsyncResult(
					Bot.RespondAsync(botInfo, PartyOrganizerStateModule.Initial, hi));
				this.AddLine(bot, response);

				const string organize = "I want to organize a party";

				this.AddLine(me, organize);
				(response, state) = await GetAsyncResult(
					Bot.RespondAsync(botInfo, state, organize));
				this.AddLine(bot, response);

				const string name = "Tolik's Awesome party";

				this.AddLine(me, name);
				(response, state) = await GetAsyncResult(
					Bot.RespondAsync(botInfo, state, name));
				this.AddLine(bot, response);

				const string dateTime = "2017/12/30 at 18:00";

				this.AddLine(me, dateTime);
				(response, state) = await GetAsyncResult(
					Bot.RespondAsync(botInfo, state, dateTime));
				this.AddLine(bot, response);

				const string dateTime2 = "next sunday at 18:00";

				this.AddLine(me, dateTime2);
				(response, state) = await GetAsyncResult(
					Bot.RespondAsync(botInfo, state, dateTime2));
				this.AddLine(bot, response);

				const string address = "Kyivska st. 21";

				this.AddLine(me, address);
				(response, state) = await GetAsyncResult(
					Bot.RespondAsync(botInfo, state, address));
				this.AddLine(bot, response);

				const string minAge = "5";

				this.AddLine(me, minAge);
				(response, state) = await GetAsyncResult(
					Bot.RespondAsync(botInfo, state, minAge));
				this.AddLine(bot, response);

				const string maxAge = "9";

				this.AddLine(me, maxAge);
				(response, _) = await GetAsyncResult(
					Bot.RespondAsync(botInfo, state, maxAge));
				this.AddLine(bot, response);

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
