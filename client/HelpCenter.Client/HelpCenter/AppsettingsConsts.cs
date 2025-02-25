namespace HelpCenter;

public class AppsettingsConsts
{
    public static string ApiUrl => DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:41000" : "http://localhost:41000";
}
