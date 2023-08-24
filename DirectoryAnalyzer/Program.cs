using Aspose.Html;

namespace DirectoryAnalyzer
{
    class Program
    {
        public static void Main(string[] args)
        {
            const string path = "./";
            var files = DirectoryAnalyzer.GetScannedFiles(path);
            var directories = DirectoryAnalyzer.GetScannedDirectories(path);
            const string outFile = "ScannedCatalog.html";

            using (var htmlDocument = new HTMLDocument())
            {
                var text = htmlDocument.CreateElement("files");
                text.InnerHTML = "<div style='font-size: 21px'>Список файлов:</div>";
                htmlDocument.Body.AppendChild(text);
                
                var table = (HTMLTableElement)htmlDocument.CreateElement("table");
                HTMLTableRowElement row;
                
                table.CellSpacing = "10";
              
                var headerRow = (HTMLTableRowElement)htmlDocument.CreateElement("tr");
                
                headerRow.InnerHTML = "<th>Название</th><th>Размер, байт</th><th>Тип</th><th>Количество файлов с данным типом</th><th>Процент файлов с данным типом</th><th>Средний размер файла с данным типом, байт</th>";
                
                table.AppendChild(headerRow);
                
                foreach (var file in files)
                {
                    if (file.Name == "DirectoryAnalyzer.exe") continue;

                    row = (HTMLTableRowElement)htmlDocument.CreateElement("tr");
                    
                    row.InnerHTML =
                        $"<td>{file.Name}</td><td>{file.Size}</td><td>{file.Type}</td><td>{file.DirectoryTypeAmount}</td><td>{file.DirectoryTypePercent}%</td><td>{file.AverageTypeSize}</td>";
                    
                    table.AppendChild(row);
                    
                    Console.WriteLine(
                        $"Name: {file.Name} Size: {file.Size} Type: {file.Type} Avg size: {file.AverageTypeSize} type amount: {file.DirectoryTypeAmount} type percent: {file.DirectoryTypePercent}");
                }
                
                htmlDocument.Body.AppendChild(table);
                
                text = htmlDocument.CreateElement("dirs");
                text.InnerHTML = "<div style='font-size: 21px'>Список поддиректорий:</div>";
                htmlDocument.Body.AppendChild(text);
                
                table = (HTMLTableElement)htmlDocument.CreateElement("table");
                
                table.CellSpacing = "20";

                headerRow = (HTMLTableRowElement)htmlDocument.CreateElement("tr");
                
                headerRow.InnerHTML = "<th>Название</th><th>Размер, байт</th>";

                table.AppendChild(headerRow);

                foreach (var directory in directories)
                {
                    row = (HTMLTableRowElement)htmlDocument.CreateElement("tr");
                    
                    row.InnerHTML = $"<td>{directory.Name}</td><td>{directory.Size}</td>";
                    
                    table.AppendChild(row);
                }
                
                htmlDocument.Body.AppendChild(table);
                
                htmlDocument.Save(outFile);
            }
        }
    }
}