using OpenQA.Selenium.Chrome;

public static class Browser
{
    // The course code opens a visible Chrome; CI has no display, so every test creates its browser through here.
    public static ChromeDriver Create(ChromeOptions options = null)
    {
        options ??= new ChromeOptions();
        options.AddArguments("--headless=new", "--no-sandbox", "--disable-dev-shm-usage", "--window-size=1920,1080");
        // Chrome's leaked-password popup blocks the page after logging in with public demo credentials.
        options.AddUserProfilePreference("profile.password_manager_leak_detection", false);
        return new ChromeDriver(options);
    }

    // Several course apps were hosted by SoftUni and have since been taken down; skip instead of failing when the app is unreachable.
    public static void RequireReachable(string url)
    {
        try
        {
            using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
            http.GetAsync(url).Result.EnsureSuccessStatusCode();
            return;
        }
        catch (Exception e) when (e is AggregateException or HttpRequestException)
        {
        }

        Assert.Ignore($"{url} is not reachable.");
    }
}
