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
			var bot = new BotInfo(
				Settings.Default.SweetsOrderConsultantId,
				Settings.Default.SweetsOrderConsultantSubscriptionKey);
            
            const string dialogStarted = "###DIALOG STARTED###\n";

            this.test.Text += dialogStarted;

            const string hi = "Hi!";
			this.AddLine(hi);
			this.AddLine(await Bot.Respond(bot, hi));

			const string orderCake = "I want to order a cake.";
			this.AddLine(orderCake);
			this.AddLine(await Bot.Respond(bot, orderCake));

            const string yesPreferences = "Yes, let's discuss ingredients.";
            this.AddLine(yesPreferences);
            this.AddLine(await Bot.Respond(bot, yesPreferences));

            const string yesBananasStrawberry = "Yes, I like bananas and strawberry.";
			this.AddLine(yesBananasStrawberry);
			this.AddLine(await Bot.Respond(bot, yesBananasStrawberry));

			const string noChocolate = "Yes, I'm allergic to chocolate.";
			this.AddLine(noChocolate);
			this.AddLine(await Bot.Respond(bot, noChocolate));

            const string rangeCake = "I want it to be in range between $75 and $130.";
            this.AddLine(rangeCake);
            this.AddLine(await Bot.Respond(bot, rangeCake));

            const string cakeName = "Fruitty, I guess.";
            this.AddLine(cakeName);
            this.AddLine(await Bot.Respond(bot, cakeName));

            const string orderCookies = "I also want to order some cookies.";
            this.AddLine(orderCookies);
            this.AddLine(await Bot.Respond(bot, orderCookies));

            const string rangeCookies = "I want it to be in range between $40 and $70.";
            this.AddLine(rangeCookies);
            this.AddLine(await Bot.Respond(bot, rangeCookies));

            const string cookieName = "Maybe, Chocolate Kifli.";
            this.AddLine(cookieName);
            this.AddLine(await Bot.Respond(bot, cookieName));

            const string kilo = "I think, about 1 kilogram.";
            this.AddLine(kilo);
            this.AddLine(await Bot.Respond(bot, kilo));

            const string orderCandies = "I also want to order some candies.";
            this.AddLine(orderCandies);
            this.AddLine(await Bot.Respond(bot, orderCandies));

            const string rangeCandies = "I want it to be in range between $50 and $90.";
            this.AddLine(rangeCandies);
            this.AddLine(await Bot.Respond(bot, rangeCandies));

            const string candyName = "I want to order Snickers.";
            this.AddLine(candyName);
            this.AddLine(await Bot.Respond(bot, candyName));

            const string kiloAndHalf = "I think, about 1.5 kg.";
            this.AddLine(kiloAndHalf);
            this.AddLine(await Bot.Respond(bot, kiloAndHalf));

            const string endConversation = "No, that's all order.";
            this.AddLine(endConversation);
            this.AddLine(await Bot.Respond(bot, endConversation));

            this.test.Text += dialogStarted;
            
            this.AddLine(hi);
            this.AddLine(await Bot.Respond(bot, hi));
            
            this.AddLine(orderCake);
            this.AddLine(await Bot.Respond(bot, orderCake));

            const string noPreferences = "No, not interested.";
            this.AddLine(noPreferences);
            this.AddLine(await Bot.Respond(bot, noPreferences));

            this.AddLine(cakeName);
            this.AddLine(await Bot.Respond(bot, cakeName));

            this.AddLine(endConversation);
            this.AddLine(await Bot.Respond(bot, endConversation));
        }

		private void AddLine(string text)
		{
			this.test.Text += $" - {text}\n\n";
		}
	}
}
