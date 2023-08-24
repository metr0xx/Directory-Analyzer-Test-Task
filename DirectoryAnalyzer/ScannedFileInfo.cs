namespace DirectoryAnalyzer;

public class ScannedFileInfo
{
    public string Name { get; set; }
    public long Size { get; set; }
    public string Type { get; set; }
    public int DirectoryTypeAmount { get; set; }
    public decimal DirectoryTypePercent { get; set; }
    public long AverageTypeSize { get; set; }
}