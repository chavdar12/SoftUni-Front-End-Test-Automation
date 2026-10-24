using System.Net.Sockets;

// Appium tests need a running Appium server and a device/emulator; skip them instead of failing when there is none (e.g. in CI).
public static class AppiumServer
{
    public static readonly Uri Url = new("http://127.0.0.1:4723/");

    public static void RequireRunning()
    {
        try
        {
            using var client = new TcpClient();
            if (client.ConnectAsync(Url.Host, Url.Port).Wait(500))
            {
                return;
            }
        }
        catch (AggregateException)
        {
        }

        Assert.Ignore($"No Appium server listening on {Url}.");
    }
}
