namespace Frank.Games.SpaceLaneTycoon;

public class Assets
{
    public Assets()
    {
        StarsFile = new FileInfo(Path.Combine(AppContext.BaseDirectory, "Assets", "hygdata_v41.csv"));
        if (!StarsFile.Exists)
        {
            throw new FileNotFoundException("Stars file not found", StarsFile.FullName);
        }
    }
    
    public FileInfo StarsFile { get; }
}