using Xunit;

public class VitalCheckerTests
{
    [Fact]
    public void NormalVitalsReturnTrue()
    {
        var result = VitalChecker.CheckVitals(98.6f, 75, 98);
        Assert.All(result, r => Assert.True(r.IsOk()));
    }

    [Fact]
    public void TemperatureTooLow()
    {
        var result = VitalChecker.CheckVitals(94f, 75, 98);
        Assert.Contains(result, r => r.Parameter == "Temperature" && r.Breach == BreachType.TooLow);
    }

    [Fact]
    public void TemperatureTooHigh()
    {
        var result = VitalChecker.CheckVitals(103f, 75, 98);
        Assert.Contains(result, r => r.Parameter == "Temperature" && r.Breach == BreachType.TooHigh);
    }

    [Fact]
    public void PulseRateTooLow()
    {
        var result = VitalChecker.CheckVitals(98f, 50, 98);
        Assert.Contains(result, r => r.Parameter == "Pulse Rate" && r.Breach == BreachType.TooLow);
    }

    [Fact]
    public void PulseRateTooHigh()
    {
        var result = VitalChecker.CheckVitals(98f, 120, 98);
        Assert.Contains(result, r => r.Parameter == "Pulse Rate" && r.Breach == BreachType.TooHigh);
    }

    [Fact]
    public void SpO2TooLow()
    {
        var result = VitalChecker.CheckVitals(98f, 75, 85);
        Assert.Contains(result, r => r.Parameter == "Oxygen Saturation" && r.Breach == BreachType.TooLow);
    }
}
