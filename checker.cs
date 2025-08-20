using System;
using System.Collections.Generic;
using System.Threading;

public enum BreachType { Normal, TooLow, TooHigh }

public class VitalCheckResult
{
    public string Parameter { get; }
    public BreachType Breach { get; }

    public VitalCheckResult(string parameter, BreachType breach)
    {
        Parameter = parameter;
        Breach = breach;
    }

    public bool IsOk() => Breach == BreachType.Normal;

    public string Message() =>
        Breach switch
        {
            BreachType.TooLow => $"{Parameter} is too low!",
            BreachType.TooHigh => $"{Parameter} is too high!",
            _ => $"{Parameter} is OK"
        };
}

public static class VitalChecker
{
    private static BreachType InferBreach(float value, float min, float max)
    {
        if (value < min) return BreachType.TooLow;
        if (value > max) return BreachType.TooHigh;
        return BreachType.Normal;
    }

    // Pure logic
    public static List<VitalCheckResult> CheckVitals(float temperature, int pulseRate, int spo2)
    {
        return new List<VitalCheckResult>
        {
            new VitalCheckResult("Temperature", InferBreach(temperature, 95, 102)),
            new VitalCheckResult("Pulse Rate", InferBreach(pulseRate, 60, 100)),
            new VitalCheckResult("Oxygen Saturation", InferBreach(spo2, 90, 200)) // 200 as practical max
        };
    }

    // I/O: reporting + alarm
    public static bool ReportVitals(float temperature, int pulseRate, int spo2)
    {
        var results = CheckVitals(temperature, pulseRate, spo2);
        foreach (var r in results)
        {
            if (!r.IsOk())
            {
                Console.WriteLine(r.Message());
                TriggerAlarm();
                return false;
            }
        }

        Console.WriteLine($"Vitals received within normal range");
        Console.WriteLine($"Temperature: {temperature}, Pulse: {pulseRate}, SpO2: {spo2}");
        return true;
    }

    private static void TriggerAlarm()
    {
        for (int i = 0; i < 6; i++)
        {
            Console.Write("\r* ");
            Thread.Sleep(200); // shorter delay for demo/test
            Console.Write("\r *");
            Thread.Sleep(200);
        }
        Console.WriteLine();
    }
}
