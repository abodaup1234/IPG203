using System.Text.RegularExpressions;

namespace ApplianceRepairCenter.Models;

// فئة ثابتة للتحقق من صحة البيانات قبل استخدامها.
public static class ValidationHelper
{
    public static bool IsValidSerialNumber(string serialNumber) =>
        !string.IsNullOrWhiteSpace(serialNumber) && Regex.IsMatch(serialNumber, @"^[A-Za-z0-9-]{4,20}$");

    public static bool IsValidDate(DateTime date) => date <= DateTime.Now.AddDays(1);

    public static bool IsValidCost(decimal cost) => cost >= 0;
}
