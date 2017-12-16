using System.Windows;

using Microsoft.FSharp.Collections;

using Chessie.ErrorHandling;
using Prolog;

using PartyInc.Core;

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
					"getCandyByPriceMoreEqualThan(50, candy(Name, Price))"));

			switch (result)
			{
				case Result<FSharpList<Solution>, string>.Ok ok:
					var solutions = SeqModule.OfList(ok.Item1);

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
					var errors = SeqModule.OfList(bad.Item);

					foreach (string error in errors)
					{
						this.AddLine(error);
					}
					break;
			}
		}

		private void AddLine(string text = "")
		{
			this.test.Text += $"{text}\n";
		}
    }
}
