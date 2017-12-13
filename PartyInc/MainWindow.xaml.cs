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

			const string hi = "Hi!";

			this.AddLine(hi);
			this.AddLine(await Bot.RespondTemporary(bot, hi));

			const string orderCake = "Order a cake";

			this.AddLine(orderCake);
			this.AddLine(await Bot.RespondTemporary(bot, orderCake));

			const string yesBananasStrrawberry = "Yes, I like bananas and strawberry.";

			this.AddLine(yesBananasStrrawberry);
			this.AddLine(await Bot.RespondTemporary(bot, yesBananasStrrawberry));

			const string yesChocolate = "Yes, I'm allergic to chocolate.";

			this.AddLine(yesChocolate);
			this.AddLine(await Bot.RespondTemporary(bot, yesChocolate));

			const string threeKilos = "Maybe, 3 killos.";

			this.AddLine(threeKilos);
			this.AddLine(await Bot.RespondTemporary(bot, threeKilos));

			const string range = "I want it to be in range between $50 and $100.";

			this.AddLine(range);
			this.AddLine(await Bot.RespondTemporary(bot, range));

			const string kiloAndHalf = "I think, about 1.5 kg";

			this.AddLine(kiloAndHalf);
			this.AddLine(await Bot.RespondTemporary(bot, kiloAndHalf));

			const string end = "No, that's it.";

			this.AddLine(end);
			this.AddLine(await Bot.RespondTemporary(bot, end));
        }

		private void AddLine(string text)
		{
			this.test.Text += $" - {text}\n\n";
		}
    }
}
