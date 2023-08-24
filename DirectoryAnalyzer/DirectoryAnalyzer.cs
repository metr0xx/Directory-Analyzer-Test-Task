namespace DirectoryAnalyzer;

public static class DirectoryAnalyzer
{
    private static List<ScannedFileInfo> _scannedFiles = new List<ScannedFileInfo>();
    private static List<ScannedDirectoryInfo> _scannedDirectories = new List<ScannedDirectoryInfo>();
    
    public static long GetDirectorySize(DirectoryInfo d)
    {
        long size = 0;
        long currentSize = 0;
           
        var fileInfos = d.EnumerateFiles();

        size = fileInfos.Sum(f => f.Length);
        
        var directoryInfos = d.EnumerateDirectories();
            
        foreach (var di in directoryInfos)
        {
            currentSize = GetDirectorySize(di);
            _scannedDirectories.Add(new ScannedDirectoryInfo()
            {
                Name = di.Name,
                Size = currentSize
            });
                
            size += currentSize;
        }
        return size;
    }
    
    
    public static List<FileInfo> GetFilesList(DirectoryInfo d)
    {
        var files = new List<FileInfo>();
        var fileInfos = d.EnumerateFiles();
            
        foreach (var file in fileInfos)
        {
            files.Add(file);
        }
        
        var directoryInfos = d.EnumerateDirectories();
            
        foreach (var di in directoryInfos)
        {
            files.AddRange(GetFilesList(di));
        }

        return files;
    }

    public static List<DirectoryInfo> GetDirectoriesList(string path)
    {
        var directories = new List<DirectoryInfo>();
        
        foreach (var directory in Directory.EnumerateDirectories(path))
        {
            directories.Add(new DirectoryInfo(directory));
        }

        return directories;
    }

    public static List<ScannedDirectoryInfo> GetScannedDirectories(string path)
    {
        _scannedDirectories.Clear();
        
        var directories = GetDirectoriesList(path);
        
        foreach (var dir in directories)
        {
            _scannedDirectories.Add(new ScannedDirectoryInfo()
            {
                Name = dir.Name,
                Size = GetDirectorySize(dir)
            });
        }
        
        return _scannedDirectories;
    }
    
    public static List<ScannedFileInfo> GetScannedFiles(string path)
    {
        _scannedFiles.Clear();
        
        var files = GetFilesList(new DirectoryInfo(path));

        foreach (var file in files)
        {
            var directoryTypeAmount = files.Count(f => f.Extension == file.Extension);
            var typeSizes = files
                .Where(f => f.Extension == file.Extension)
                .Select(f => f.Length)
                .ToList();
            
            _scannedFiles.Add(new ScannedFileInfo()
            {
                Name = file.Name,
                Size = file.Length,
                Type = file.Extension != "" ? file.Extension : "Файл",
                DirectoryTypeAmount = directoryTypeAmount,
                DirectoryTypePercent = Math.Round((decimal)directoryTypeAmount / files.Count * 100, 1),
                AverageTypeSize = typeSizes.Sum() / typeSizes.Count
            });
        }

        return _scannedFiles;
    }
}