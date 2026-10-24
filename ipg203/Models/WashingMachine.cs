namespace ApplianceRepairCenter.Models;

public class WashingMachine : Appliance
{
    public int MaximumLoadKg { get; }

    public WashingMachine(string serialNumber, string brand, string customerName, string problem,
        decimal baseCost, int maximumLoadKg, DateTime receivedDate)
        : base(serialNumber, brand, customerName, problem, baseCost, receivedDate)
    {
        if (maximumLoadKg <= 0) throw new ArgumentOutOfRangeException(nameof(maximumLoadKg));
        MaximumLoadKg = maximumLoadKg;
    }

    public override decimal CalculateRepairCost() => BaseCost + 100m;
    public override string GetApplianceType() => "غسالة";
}
