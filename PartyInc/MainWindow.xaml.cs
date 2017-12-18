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
				this.AddLine(
					bot, await GetAsyncResult(Bot.RespondAsync(botInfo, hi)));

				const string organize = "I want to organize a party";

				this.AddLine(me, organize);
				this.AddLine(
					bot, await GetAsyncResult(Bot.RespondAsync(botInfo, organize)));

                botInfo = new BotInfo(
					Settings.Default.SweetsOrderConsultantId,
					Settings.Default.SweetsOrderConsultantSubscriptionKey,
					SweetsOrderConsultant.HandleResponse);

                this.AddLine(me, hi);
                this.AddLine(
					bot, await GetAsyncResult(Bot.RespondAsync(botInfo, hi)));

                const string orderCake = "I want to order a cake.";
                this.AddLine(me, orderCake);
                this.AddLine(
					bot, await GetAsyncResult(Bot.RespondAsync(botInfo, orderCake)));

                const string yesPreferences = "Yes, let's discuss ingredients.";
                this.AddLine(me, yesPreferences);
                this.AddLine(
					bot, await GetAsyncResult(Bot.RespondAsync(botInfo, yesPreferences)));

                const string yesBananasStrawberry = "Yes, I like bananas and strawberry.";
                this.AddLine(me, yesBananasStrawberry);
                this.AddLine(
					bot,
					await GetAsyncResult(Bot.RespondAsync(botInfo, yesBananasStrawberry)));

                const string noChocolate = "Yes, I'm allergic to chocolate.";
                this.AddLine(me, noChocolate);
                this.AddLine(
					bot, await GetAsyncResult(Bot.RespondAsync(botInfo, noChocolate)));

                const string rangeCake = "I want it to be in range between $75 and $130.";
                this.AddLine(me, rangeCake);
                this.AddLine(
					bot, await GetAsyncResult(Bot.RespondAsync(botInfo, rangeCake)));

                const string cakeName = "Fruitty, I guess.";
                this.AddLine(me, cakeName);
                this.AddLine(
					bot, await GetAsyncResult(Bot.RespondAsync(botInfo, cakeName)));

                const string orderCookies = "I also want to order some cookies.";
                this.AddLine(me, orderCookies);
                this.AddLine(
					bot, await GetAsyncResult(Bot.RespondAsync(botInfo, orderCookies)));

                const string rangeCookies = "I want it to be in range between $40 and $70.";
                this.AddLine(me, rangeCookies);
                this.AddLine(
					bot, await GetAsyncResult(Bot.RespondAsync(botInfo, rangeCookies)));

                const string cookieName = "Maybe, Chocolate Kifli.";
                this.AddLine(me, cookieName);
                this.AddLine(
					bot, await GetAsyncResult(Bot.RespondAsync(botInfo, cookieName)));

                const string kilo = "I think, about 1 kilogram.";
                this.AddLine(me, kilo);
                this.AddLine(
					bot, await GetAsyncResult(Bot.RespondAsync(botInfo, kilo)));

                const string orderCandies = "I also want to order some candies.";
                this.AddLine(me, orderCandies);
                this.AddLine(
					bot, await GetAsyncResult(Bot.RespondAsync(botInfo, orderCandies)));

                const string rangeCandies = "I want it to be in range between $50 and $90.";
                this.AddLine(me, rangeCandies);
                this.AddLine(
					bot, await GetAsyncResult(Bot.RespondAsync(botInfo, rangeCandies)));

                const string candyName = "I want to order Snickers.";
                this.AddLine(me, candyName);
                this.AddLine(
					bot, await GetAsyncResult(Bot.RespondAsync(botInfo, candyName)));

                const string kiloAndHalf = "I think, about 1.5 kg.";
                this.AddLine(me, kiloAndHalf);
                this.AddLine(
					bot, await GetAsyncResult(Bot.RespondAsync(botInfo, kiloAndHalf)));

                const string endConversation = "No, that's all order.";
                this.AddLine(me, endConversation);
                this.AddLine(
					bot, await GetAsyncResult(Bot.RespondAsync(botInfo, endConversation)));
                
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
