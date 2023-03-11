using OfficeOpenXml;
using System.ComponentModel;
using WindPowerWebApp.Model;

namespace WindPowerWebApp.Service
{
    public class ExcelExporter
    {
        public static MemoryStream ExportToExcel(List<DataModel> dataModels)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("SystemData");

                // Add column headers
                var headers = typeof(DataModel).GetProperties()
                    .Select(p => (DisplayNameAttribute)p.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() ?? new DisplayNameAttribute(p.Name))
                    .Select(a => a.DisplayName);
                int columnCount = 1;
                foreach (var header in headers)
                {
                    worksheet.Cells[1, columnCount].Value = header;
                    columnCount++;
                }

                // Add rows
                int rowCount = 2;
                foreach (var dataModel in dataModels)
                {
                    columnCount = 1;
                    foreach (var property in typeof(DataModel).GetProperties())
                    {
                        if (property.PropertyType == typeof(DateTime))
                            worksheet.Cells[rowCount, columnCount].Style.Numberformat.Format = "yyyy-MM-dd hh:mm:ss";
                        var cellValue = property.GetValue(dataModel);
                        worksheet.Cells[rowCount, columnCount].Value = cellValue;
                        columnCount++;
                    }
                    rowCount++;
                }

                // Auto-fit columns
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Return the Excel file as a MemoryStream
                var memoryStream = new MemoryStream();
                package.SaveAs(memoryStream);
                memoryStream.Position = 0;
                return memoryStream;
            }
        }
    }
}
