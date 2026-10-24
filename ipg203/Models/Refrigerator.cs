namespace ApplianceRepairCenter.Models;

public class Refrigerator : Appliance
{
    public int CapacityLiters { get; }

    public Refrigerator(string serialNumber, string brand, string customerName, string problem,
        decimal baseCost, int capacityLiters, DateTime receivedDate)
        : base(serialNumber, brand, customerName, problem, baseCost, receivedDate)
    {
        if (capacityLiters <= 0) throw new ArgumentOutOfRangeException(nameof(capacityLiters));
        CapacityLiters = capacityLiters;
    }

    public override decimal CalculateRepairCost() => BaseCost + 150m;
    public override string GetApplianceType() => "ثلاجة";
}
