using ApplianceRepairCenter.Models;

namespace ApplianceRepairCenter.Services;

public class RepairCenter
{
    private readonly List<Appliance> _appliances = new();
    public IReadOnlyList<Appliance> Appliances => _appliances.AsReadOnly();

    public void AddAppliance(Appliance appliance)
    {
        if (_appliances.Any(a => a.SerialNumber.Equals(appliance.SerialNumber, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("الرقم التسلسلي مسجل مسبقاً.");

        appliance.StatusChanged += OnApplianceStatusChanged;
        _appliances.Add(appliance);
    }

    public Appliance? FindBySerialNumber(string serialNumber) =>
        _appliances.FirstOrDefault(a => a.SerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase));

    public void ShowAllAppliances()
    {
        if (_appliances.Count == 0)
        {
            Console.WriteLine("لا توجد أجهزة مسجلة.");
            return;
        }

        foreach (Appliance appliance in _appliances)
        {
            Console.WriteLine($"النوع: {appliance.GetApplianceType()} | الرقم: {appliance.SerialNumber} | " +
                $"الزبون: {appliance.CustomerName} | الحالة: {appliance.Status} | " +
                $"التكلفة: {appliance.CalculateRepairCost():0.00}");
        }
    }

    public void CheckLateAppliances()
    {
        foreach (Appliance appliance in _appliances.Where(a => a.IsLate()))
            OnApplianceStatusChanged(appliance, $"تنبيه: الجهاز {appliance.SerialNumber} تجاوز مدة الصيانة المتوقعة.");
    }

    private static void OnApplianceStatusChanged(Appliance appliance, string message) => Console.WriteLine($"[تنبيه] {message}");
}
