using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using PartyInc.Core;
using PartyInc.Properties;

using static PartyInc.Core.API;

namespace PartyInc
{
    public partial class MainWindow : Window
	{
		enum CurrentBot { PartyOrganizer, SweetsOrderConsultant, DrinksOrderConsultant }

		private BotInfo<PartyOrganizerState> partyOrganizer;
		// TODO private BotInfo<SweetsOrderConsultantState> sweetsOrderConsultant;
		// TODO private BotInfo<DrinksOrderConsultantState> drinksOrderConsultant;

		private PartyOrganizerState partyOrganizerState = PartyOrganizerStateModule.Initial;
		// TODO private SweetsOrderConsultantState sweetsOrderConsultantState;
		// TODO private DrinksOrderConsultantState sweetsOrderConsultantState;

		private CurrentBot currentBot = CurrentBot.PartyOrganizer;

		private const string me  = " me";
		private const string bot = "bot";

        public MainWindow()
        {
            this.InitializeComponent();

			this.partyOrganizer = new BotInfo<PartyOrganizerState>(
				Settings.Default.PartyOrganizerId,
				Settings.Default.PartyOrganizerSubscriptionKey,
				PartyOrganizer.HandleResponse);

			// TODO
			//this.sweetsOrderConsultant = new BotInfo<SweetsOrderConsultantState>(
			//	Settings.Default.SweetsOrderConsultantId,
			//	Settings.Default.SweetsOrderConsultantSubscriptionKey,
			//	SweetsOrderConsultant.HandleResponse);

			// TODO
			//this.drinksOrderConsultant = new BotInfo<DrinksOrderConsultantState>(
			//	Settings.Default.DrinksOrderConsultantId,
			//	Settings.Default.DrinksOrderConsultantSubscriptionKey,
			//	DrinksOrderConsultant.HandleResponse);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.AddLine(
				bot,
				"Welcome to Party Inc! " +
				"We'll help you organize a mind-blowing party for kids!");

			FocusManager.SetFocusedElement(this.userTextBox, this.userTextBox);
		}

		private async void New_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			string text = this.userTextBox.Text;
			this.AddLine(me, text);
			this.userTextBox.Text = String.Empty;
			this.userTextBox.IsEnabled = false;

			try
			{
				switch (this.currentBot)
				{
					case CurrentBot.PartyOrganizer:
						var newPartyOrganizerState =
							await this.Respond(
								this.partyOrganizer,
								this.partyOrganizerState, text);

						this.partyOrganizerState = newPartyOrganizerState;

						if (PartyOrganizerStateModule.IsAwaitingFood(
							newPartyOrganizerState))
						{
							this.currentBot = CurrentBot.SweetsOrderConsultant;
						}

						if (PartyOrganizerStateModule.IsAwaitingDrinks(
							newPartyOrganizerState))
						{
							this.currentBot = CurrentBot.DrinksOrderConsultant;
						}

						if (PartyOrganizerStateModule.IsFinished(newPartyOrganizerState))
						{
							this.SaveOrder(
								GetResult(PartyOrganizerStateModule.GetOrder(
									newPartyOrganizerState)));
						}
						break;
					case CurrentBot.SweetsOrderConsultant:
						// var newSweetsOrderConsultantState =
						//	await this.Respond(
						//		this.sweetsOrderConsultant,
						//		this.sweetsOrderConsultantState,
						//		text);

						// this.sweetsOrderConsultantState = newSweetsOrderConsultantState;

						// TODO implement IsFinished for SweetsOrderConsultantState module
						// if (SweetsOrderConsultantStateModule.IsFinished(state))
						// {
						// 	this.currentBot = CurrentBot.PartyOrganizer;
						//	this.partyOrganizerState =
						//		PartyOrganizerStateModule.AddFood(
						//			this.sweetsOrderConsultantState,
						//			this.partyOrganizerState);
						// }
						break;
					case CurrentBot.DrinksOrderConsultant:
						// var newDrinksOrderConsultantState =
						//	await this.Respond(
						//		this.drinksOrderConsultant,
						//		this.drinksOrderConsultantState,
						//		text);

						// this.sweetsOrderConsultantState = newSweetsOrderConsultantState;

						// TODO implement IsFinished for DrinksOrderConsultantState module
						// if (SweetsOrderConsultantStateModule.IsFinished(state))
						// {
						// 	this.currentBot = CurrentBot.PartyOrganizer;
						//	this.partyOrganizerState =
						//		PartyOrganizerStateModule.AddFood(
						//			this.sweetsOrderConsultantState,
						//			this.partyOrganizerState);
						// }
						break;
				}
			} catch (Exception exp)
			{
				this.AddLine(bot, $"Whoops! {exp.Message}");
			}

			this.userTextBox.IsEnabled = true;
			this.contentTextBox.ScrollToEnd();
			this.userTextBox.Focus();
		}

		private async Task<TState> Respond<TState>(
			BotInfo<TState> botInfo,
			TState state,
			string query)
		{
			var (response, newState) = await GetAsyncResult(Bot.RespondAsync(
				botInfo, state, query));

			this.AddLine(bot, response);

			return newState;
		}

		private void AddLine(string party, string text = "")
		{
			this.contentTextBox.Text += $"{party}: {text}\n\n";
		}

		private void SaveOrder(Order order)
		{
			using (var writer = new StreamWriter("Data\\orders.pl", true))
			{
				writer.WriteLine(PrologOrder.FormatOrder(order) + ".");
			}
		}
	}
}
