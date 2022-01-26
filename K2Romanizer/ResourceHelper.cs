namespace K2Romanizer;

public class ResourceHelper
{
    #region singleton
    private static readonly Lazy<ResourceHelper> _instance = new(() => new ResourceHelper());

    public static ResourceHelper Instance => _instance.Value;

    private ResourceHelper()
    {
    }
    #endregion

    public string GetHelp()
    {
        return ReadText("Help.txt");
    }

    public string GetSystemData()
    {
        return ReadText("SystemData.txt");
    }

    private string ReadText(string fileName)
    {
        using Stream stream = GetType().Assembly.GetManifestResourceStream($"K2Romanizer.Resources.{fileName}");
        using StreamReader reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }
}