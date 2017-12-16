using System.Windows;

using Microsoft.FSharp.Collections;

using Chessie.ErrorHandling;
using Prolog;

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
			var prolog = new PrologEngine();

			var result = await AsyncToTask(
				PrologInterop.GetSolutions(
					prolog,
					"Data\\food.pl",
					"getCakeByPriceMoreEqualThan(50, cake(Name, Ingredients, Price))"));

			switch (result)
			{
				case Result<FSharpList<Solution>, string>.Ok ok:
					var solutions = ListModule.ToSeq(ok.Item1);

					this.AddLine("Variables:");
					this.AddLine();

					foreach (var solution in solutions)
					{
						foreach (var variable in PrologInterop.GetVariables(solution))
						{
							this.AddLine($"{variable.Name}: {variable.Value}");
						}

						this.AddLine();
					}
					break;
				case Result<FSharpList<Solution>, string>.Bad bad:
					var errors = ListModule.ToSeq(bad.Item);

					this.AddLine("Errors:");
					this.AddLine();

					foreach (string error in errors)
					{
						this.AddLine(error);
					}
					break;
			}

            var bot = new BotInfo(
                Settings.Default.SweetsOrderConsultantId,
                Settings.Default.SweetsOrderConsultantSubscriptionKey,
                SweetsOrderConsultant.ManageResponse);
            
            const string dialogStarted = "###DIALOG STARTED###\n";

            this.test.Text += dialogStarted;

            const string hi = "Hi!";
            this.AddLine(hi);
            this.AddLine(await Bot.RespondAsync(bot, hi));

            const string orderCake = "I want to order a cake.";
            this.AddLine(orderCake);
            this.AddLine(await Bot.RespondAsync(bot, orderCake));

            const string yesPreferences = "Yes, let's discuss ingredients.";
            this.AddLine(yesPreferences);
            this.AddLine(await Bot.RespondAsync(bot, yesPreferences));

            const string yesBananasStrawberry = "Yes, I like bananas and strawberry.";
            this.AddLine(yesBananasStrawberry);
            this.AddLine(await Bot.RespondAsync(bot, yesBananasStrawberry));

            const string noChocolate = "Yes, I'm allergic to chocolate.";
            this.AddLine(noChocolate);
            this.AddLine(await Bot.RespondAsync(bot, noChocolate));

            const string rangeCake = "I want it to be in range between $75 and $130.";
            this.AddLine(rangeCake);
            this.AddLine(await Bot.RespondAsync(bot, rangeCake));

            const string cakeName = "Fruitty, I guess.";
            this.AddLine(cakeName);
            this.AddLine(await Bot.RespondAsync(bot, cakeName));

            const string orderCookies = "I also want to order some cookies.";
            this.AddLine(orderCookies);
            this.AddLine(await Bot.RespondAsync(bot, orderCookies));

            const string rangeCookies = "I want it to be in range between $40 and $70.";
            this.AddLine(rangeCookies);
            this.AddLine(await Bot.RespondAsync(bot, rangeCookies));

            const string cookieName = "Maybe, Chocolate Kifli.";
            this.AddLine(cookieName);
            this.AddLine(await Bot.RespondAsync(bot, cookieName));

            const string kilo = "I think, about 1 kilogram.";
            this.AddLine(kilo);
            this.AddLine(await Bot.RespondAsync(bot, kilo));

            const string orderCandies = "I also want to order some candies.";
            this.AddLine(orderCandies);
            this.AddLine(await Bot.RespondAsync(bot, orderCandies));

            const string rangeCandies = "I want it to be in range between $50 and $90.";
            this.AddLine(rangeCandies);
            this.AddLine(await Bot.RespondAsync(bot, rangeCandies));

            const string candyName = "I want to order Snickers.";
            this.AddLine(candyName);
            this.AddLine(await Bot.RespondAsync(bot, candyName));

            const string kiloAndHalf = "I think, about 1.5 kg.";
            this.AddLine(kiloAndHalf);
            this.AddLine(await Bot.RespondAsync(bot, kiloAndHalf));

            const string endConversation = "No, that's all order.";
            this.AddLine(endConversation);
            this.AddLine(await Bot.RespondAsync(bot, endConversation));

            this.test.Text += dialogStarted;
            
            this.AddLine(hi);
            this.AddLine(await Bot.RespondAsync(bot, hi));
            
            this.AddLine(orderCake);
            this.AddLine(await Bot.RespondAsync(bot, orderCake));

            const string noPreferences = "No, not interested.";
            this.AddLine(noPreferences);
            this.AddLine(await Bot.RespondAsync(bot, noPreferences));

            this.AddLine(cakeName);
            this.AddLine(await Bot.RespondAsync(bot, cakeName));

            this.AddLine(endConversation);
            this.AddLine(await Bot.RespondAsync(bot, endConversation));
		}

		private void AddLine(string text = "")
		{
			this.test.Text += $"{text}\n";
		}
    }
}
