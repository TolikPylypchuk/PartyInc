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
            
            this.test.Text = await Bot.RespondTemporary(bot, "Hi!");
            this.test.Text = await Bot.RespondTemporary(bot, "Order a cake");
            this.test.Text =
				await Bot.RespondTemporary(bot, "Yes, I like bananas and strawberry.");
            this.test.Text =
				await Bot.RespondTemporary(bot, "Yes, I'm allergic to chocolate.");
            this.test.Text = await Bot.RespondTemporary(bot, "Maybe, 3 killos.");
			//this.test.Text =
			//	await Bot.RespondTemporary(
			//		bot, "I want it to be in range between $50 and $100.");
			//this.test.Text = await bot.Response(bot, "I think, about 1.5 kg")
			this.test.Text = await Bot.RespondTemporary(bot, "No, that's it.");
        }
    }
}
