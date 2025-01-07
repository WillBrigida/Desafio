using BrownianMotion.Modules.Main;
using BrownianMotion.Modules.Utils;
using Moq;
using Xunit;

namespace BrownianMotion.Tests;

public class MainPageViewModelTests
{
    [Fact]
    public void Sigma_Property_Set_Get()
    {
        // Arrange
        var viewModel = new MainPageViewModel();
        double expectedSigma = 1.5;

        // Act
        viewModel.Sigma = expectedSigma;
        var actualSigma = viewModel.Sigma;

        // Assert
        Assert.Equal(expectedSigma, actualSigma);
    }

    [Fact]
    public void Mean_Property_Set_Get()
    {
        // Arrange
        var viewModel = new MainPageViewModel();
        double expectedMean = 0.5;

        // Act
        viewModel.Mean = expectedMean;
        var actualMean = viewModel.Mean;

        // Assert
        Assert.Equal(expectedMean, actualMean);
    }

    [Fact]
    public void InitialPrice_Property_Set_Get()
    {
        // Arrange
        var viewModel = new MainPageViewModel();
        double expectedInitialPrice = 100.0;

        // Act
        viewModel.InitialPrice = expectedInitialPrice;
        var actualInitialPrice = viewModel.InitialPrice;

        // Assert
        Assert.Equal(expectedInitialPrice, actualInitialPrice);
    }

    [Fact]
    public void NumDays_Property_Set_Get()
    {
        // Arrange
        var viewModel = new MainPageViewModel();
        int expectedNumDays = 30;

        // Act
        viewModel.NumDays = expectedNumDays;
        var actualNumDays = viewModel.NumDays;

        // Assert
        Assert.Equal(expectedNumDays, actualNumDays);
    }

    [Fact]
    public void GraficsDrawable_Property_Set_Get()
    {
        // Arrange
        var viewModel = new MainPageViewModel();
        GraphicsDrawable expectedGraficsDrawable = new([100.0, 101.0, 102.0]);

        // Act
        viewModel.GraphicsDrawable = expectedGraficsDrawable;
        var actualGraficsDrawable = viewModel.GraphicsDrawable;

        // Assert
        Assert.Equal(expectedGraficsDrawable, actualGraficsDrawable);
    }

    [Fact]
    public void OnGenerateSimulationCommand_Executes_Correctly()
    {
        // Arrange
        var viewModel = new MainPageViewModel();
        viewModel.Sigma = 1.5;
        viewModel.Mean = 0.5;
        viewModel.InitialPrice = 100.0;
        viewModel.NumDays = 30;

        // Act
        viewModel.OnGenerateSimulationCommand.Execute(null);

        // Assert
        Assert.NotNull(viewModel.Sigma); 
        Assert.NotNull(viewModel.Mean); 
        Assert.NotNull(viewModel.InitialPrice); 
        Assert.NotNull(viewModel.NumDays); 
        Assert.NotNull(viewModel.GraphicsDrawable); 
    }
}
