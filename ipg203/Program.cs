using ApplianceRepairCenter.Models;
using ApplianceRepairCenter.Services;

RepairCenter center = new();

// بيانات تجريبية لتجربة النظام عند التشغيل.
center.AddAppliance(new Refrigerator("RF-1001", "Samsung", "أحمد", "الثلاجة لا تبرد", 250m, 350, DateTime.Now.AddDays(-2)));
center.AddAppliance(new WashingMachine("WM-1002", "LG", "سارة", "الغسالة لا تصرّف الماء", 180m, 8, DateTime.Now.AddDays(-9)));
center.AddAppliance(new AirConditioner("AC-1003", "Gree", "خالد", "ضعف التبريد", 300m, 18000, DateTime.Now.AddDays(-1)));

bool running = true;
while (running)
{
    Console.WriteLine("\n=== نظام إدارة مركز صيانة الأجهزة المنزلية ===");
    Console.WriteLine("1- عرض جميع الأجهزة");
    Console.WriteLine("2- بدء صيانة جهاز");
    Console.WriteLine("3- إنهاء صيانة جهاز");
    Console.WriteLine("4- البحث بالرقم التسلسلي");
    Console.WriteLine("5- فحص الأجهزة المتأخرة");
    Console.WriteLine("6- عرض الإحصائيات");
    Console.WriteLine("0- خروج");
    Console.Write("اختر: ");
    string? choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1":
                center.ShowAllAppliances();
                break;
            case "2":
                ChangeStatus(center, start: true);
                break;
            case "3":
                ChangeStatus(center, start: false);
                break;
            case "4":
                Console.Write("أدخل الرقم التسلسلي: ");
                Appliance? found = center.FindBySerialNumber(Console.ReadLine() ?? "");
                Console.WriteLine(found is null ? "الجهاز غير موجود." :
                    $"{found.GetApplianceType()} - {found.Brand} - {found.Status} - التكلفة: {found.CalculateRepairCost():0.00}");
                break;
            case "5":
                center.CheckLateAppliances();
                break;
            case "6":
                Console.WriteLine($"عدد الأجهزة المسجلة: {Appliance.TotalAppliances}");
                Console.WriteLine($"مجموع تكاليف الإصلاح المكتملة: {Appliance.TotalRepairCosts:0.00}");
                break;
            case "0":
                running = false;
                break;
            default:
                Console.WriteLine("الرجاء اختيار رقم صحيح.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"حدث خطأ: {ex.Message}");
    }
}

static void ChangeStatus(RepairCenter center, bool start)
{
    Console.Write("أدخل الرقم التسلسلي للجهاز: ");
    string serial = Console.ReadLine() ?? "";
    Appliance? appliance = center.FindBySerialNumber(serial);
    if (appliance is null)
    {
        Console.WriteLine("الجهاز غير موجود.");
        return;
    }

    if (start) appliance.StartRepair();
    else appliance.CompleteRepair();
}
