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
            
            /*
			var botInfo = new BotInfo<PartyOrganizerState>(
				Settings.Default.PartyOrganizerId,
				Settings.Default.PartyOrganizerSubscriptionKey,
				PartyOrganizer.HandleResponse);
            */

			try
			{
                /*
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

				const string dateTime = "next sunday at 18:00";
				this.AddLine(me, dateTime);
				(response, state) = await GetAsyncResult(
					Bot.RespondAsync(botInfo, state, dateTime));
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
                */

                var sweetsBotInfo = new BotInfo<SweetsOrderConsultantState>(
                Settings.Default.SweetsOrderConsultantId,
                Settings.Default.SweetsOrderConsultantSubscriptionKey,
                SweetsOrderConsultant.HandleResponse);
                
                this.AddLine(
                    bot,
                    "Welcome to Sweet Order Consultant! We'll help you fill " +
                    "all your tables with various sweets! You can choose a " +
                    "cake, some candies and cookies. What do you want to " +
                    "order?");

                const string orderSomeCandies = "I want to order some candies.";
                this.AddLine(me, orderSomeCandies);
                var (sweetsBotResponse, sweetsBotState) = await GetAsyncResult(
                    Bot.RespondAsync(
                        sweetsBotInfo, 
                        SweetsOrderConsultantStateModule.Initial,
                        orderSomeCandies));
                this.AddLine(bot, sweetsBotResponse);

                const string priceLimit = "Less than $100.";
                this.AddLine(me, priceLimit);
                (sweetsBotResponse, sweetsBotState) = await GetAsyncResult(
                    Bot.RespondAsync(
                        sweetsBotInfo,
                        sweetsBotState,
                        priceLimit));
                this.AddLine(bot, sweetsBotResponse);

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
