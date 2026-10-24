namespace ApplianceRepairCenter.Models;

public class AirConditioner : Appliance
{
    public int CoolingCapacityBtu { get; }

    public AirConditioner(string serialNumber, string brand, string customerName, string problem,
        decimal baseCost, int coolingCapacityBtu, DateTime receivedDate)
        : base(serialNumber, brand, customerName, problem, baseCost, receivedDate)
    {
        if (coolingCapacityBtu <= 0) throw new ArgumentOutOfRangeException(nameof(coolingCapacityBtu));
        CoolingCapacityBtu = coolingCapacityBtu;
    }

    public override decimal CalculateRepairCost() => BaseCost + 200m;
    public override string GetApplianceType() => "مكيف";
}
