namespace ApplianceRepairCenter.Models;

public delegate void ApplianceStatusChangedHandler(Appliance appliance, string message);

// الفئة المجردة التي تجمع الخصائص المشتركة بين الأجهزة.
public abstract class Appliance : IRepairable
{
    private string _serialNumber;
    private string _brand;
    private string _customerName;
    private string _problemDescription;
    private decimal _baseCost;

    public string SerialNumber => _serialNumber; // للقراءة فقط بعد الإنشاء
    public string Brand
    {
        get => _brand;
        set => _brand = string.IsNullOrWhiteSpace(value) ? throw new ArgumentException("اسم الماركة مطلوب") : value;
    }
    public string CustomerName
    {
        get => _customerName;
        set => _customerName = string.IsNullOrWhiteSpace(value) ? throw new ArgumentException("اسم الزبون مطلوب") : value;
    }
    public string ProblemDescription
    {
        get => _problemDescription;
        set => _problemDescription = string.IsNullOrWhiteSpace(value) ? throw new ArgumentException("وصف العطل مطلوب") : value;
    }
    public decimal BaseCost
    {
        get => _baseCost;
        protected set => _baseCost = value < 0 ? throw new ArgumentOutOfRangeException(nameof(value)) : value;
    }
    public string Status { get; private set; }
    public DateTime ReceivedDate { get; }
    public DateTime? CompletedDate { get; private set; }

    public event ApplianceStatusChangedHandler? StatusChanged;
    public static int TotalAppliances { get; private set; }
    public static decimal TotalRepairCosts { get; private set; }

    protected Appliance(string serialNumber, string brand, string customerName,
        string problemDescription, decimal baseCost, DateTime receivedDate)
    {
        if (!ValidationHelper.IsValidSerialNumber(serialNumber))
            throw new ArgumentException("الرقم التسلسلي يجب أن يتكون من 4 إلى 20 حرفاً أو رقماً.");
        if (!ValidationHelper.IsValidDate(receivedDate))
            throw new ArgumentException("تاريخ الاستلام غير صحيح.");

        _serialNumber = serialNumber;
        _brand = string.IsNullOrWhiteSpace(brand) ? throw new ArgumentException("اسم الماركة مطلوب") : brand;
        _customerName = string.IsNullOrWhiteSpace(customerName) ? throw new ArgumentException("اسم الزبون مطلوب") : customerName;
        _problemDescription = string.IsNullOrWhiteSpace(problemDescription) ? throw new ArgumentException("وصف العطل مطلوب") : problemDescription;
        BaseCost = baseCost;
        ReceivedDate = receivedDate;
        Status = "قيد الانتظار";
        TotalAppliances++;
    }

    public void StartRepair()
    {
        if (Status == "تم الإصلاح") throw new InvalidOperationException("تم إصلاح هذا الجهاز مسبقاً.");
        Status = "قيد الصيانة";
        StatusChanged?.Invoke(this, $"بدأت صيانة الجهاز {SerialNumber}.");
    }

    public void CompleteRepair()
    {
        if (Status == "تم الإصلاح") throw new InvalidOperationException("تم إصلاح هذا الجهاز مسبقاً.");
        Status = "تم الإصلاح";
        CompletedDate = DateTime.Now;
        TotalRepairCosts += CalculateRepairCost();
        StatusChanged?.Invoke(this, $"اكتمل إصلاح الجهاز {SerialNumber}.");
    }

    public bool IsLate(int allowedDays = 7) =>
        Status != "تم الإصلاح" && (DateTime.Now - ReceivedDate).TotalDays > allowedDays;

    public abstract decimal CalculateRepairCost();
    public abstract string GetApplianceType();
}
