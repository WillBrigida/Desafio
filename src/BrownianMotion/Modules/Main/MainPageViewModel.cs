using System.Windows.Input;
using BrownianMotion.Modules.Utils;

namespace BrownianMotion.Modules.Main;

public class MainPageViewModel : BaseViewModel
{
    #region PROPERTIES . . .
    private int? _numDays = 5;
    public double? Sigma { get; set; } = 20;
    public double? Mean { get; set; } = 1;
    public double? InitialPrice { get; set; } = 100;
    public int? NumDays
    {
        get => _numDays;
        set
        {
            if (SetProperty(ref _numDays, value))
            {
                _numDays = value;

                if (Sigma.HasValue && Mean.HasValue && InitialPrice.HasValue)
                    OnGenerateSimulationCommand.Execute(new CancellationToken());
            }
        }
    }

    private GraphicsDrawable? _graphicsDrawable;
    public GraphicsDrawable? GraphicsDrawable
    {
        get => _graphicsDrawable;
        set
        {
            if (SetProperty(ref _graphicsDrawable, value))
                _graphicsDrawable = value;
        }
    }
    #endregion

    public ICommand OnGenerateSimulationCommand => new Command(async () =>
    {
        if (!Sigma.HasValue || !Mean.HasValue || !InitialPrice.HasValue || !NumDays.HasValue )
        {
            await Shell.Current.ToastAlert("O preenchimento de todos os campos é obrigatório.", TimeSpan.FromSeconds(4));
            return;
        }

        if(NumDays.Value < 0) return;

        var graphicsDrawable = 
        new GraphicsDrawable(GenereteBrownianMotion(Sigma.Value / 100, Mean.Value / 100, InitialPrice.Value, NumDays.Value));

        GraphicsDrawable = graphicsDrawable;
    });

    private static double[] GenereteBrownianMotion(double sigma, double mean, double initialPrice, int numDays)
    {
        Random rand = new();
        double[] prices = new double[numDays];
        prices[0] = initialPrice;

        for (int i = 1; i < numDays; i++)
        {
            double u1 = 1.0 - rand.NextDouble();
            double u2 = 1.0 - rand.NextDouble();
            double z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);

            double retornoDiario = mean + sigma * z;

            prices[i] = prices[i - 1] * Math.Exp(retornoDiario);
        }
        return prices;
    }
}
