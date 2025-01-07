
namespace BrownianMotion.Modules.Main;

public partial class MainPage : ContentPage
{
	public MainPage() => InitializeComponent();

	public async void Entry_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (string.IsNullOrEmpty(e.NewTextValue))
		{
			(sender as Entry)!.Text = null;
			return;
		}

		if (!double.TryParse(e.NewTextValue, out double value))
		{
			if (!string.IsNullOrEmpty((sender as Entry)!.Text))
				await Shell.Current.ToastAlert("Informe um valor válido.");

			(sender as Entry)!.Text = !double.TryParse(e.OldTextValue, out double a) ? null : e.OldTextValue;
		}
	}

	private async void NumDays_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (string.IsNullOrEmpty(e.NewTextValue))
		{
			(sender as Entry)!.Text = null;
			return;
		}

		if (!int.TryParse(e.NewTextValue, out int value))
		{
			if (!string.IsNullOrEmpty((sender as Entry)!.Text))
				await Shell.Current.ToastAlert("Valor inválido. Informe a quantidade de dias que deseja simular.", TimeSpan.FromSeconds(4));

			(sender as Entry)!.Text = !int.TryParse(e.OldTextValue, out int a) ? null : e.OldTextValue;
		}
	}
}