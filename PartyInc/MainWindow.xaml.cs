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
            
            this.test.Text = bot.Response("Hi!");
            this.test.Text = bot.Response("Order a cake");
            this.test.Text = bot.Response("Yes, I like bananas and strawberry.");
            this.test.Text = bot.Response("Yes, I'm allergic to chocolate.");
            this.test.Text = bot.Response("Maybe, 3 killos.");
            //this.test.Text = bot.Response("I want it to be in range between $50 and $100.");
            //this.test.Text = bot.Response("I think, about 1.5 kg")
            this.test.Text = bot.Response("No, that's it.");
        }
    }
}
