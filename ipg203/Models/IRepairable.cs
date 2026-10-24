namespace ApplianceRepairCenter.Models;

// الواجهة التي تحدد العمليات المشتركة بين الأجهزة القابلة للصيانة.
public interface IRepairable
{
    void StartRepair();
    void CompleteRepair();
    decimal CalculateRepairCost();
}
