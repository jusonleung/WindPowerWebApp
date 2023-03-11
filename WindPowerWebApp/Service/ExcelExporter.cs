using OfficeOpenXml;
using System.ComponentModel;
using WindPowerWebApp.Model;

namespace WindPowerWebApp.Service
{
    public class ExcelExporter
    {
        public MemoryStream ExportToExcel<T>(List<T> objs)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("SystemData");
                var props = typeof(T).GetProperties();

                // Add column headers
                var headers = props
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
                foreach (var obj in objs)
                {
                    columnCount = 1;
                    foreach (var property in props)
                    {
                        if (property.PropertyType == typeof(DateTime))
                            worksheet.Cells[rowCount, columnCount].Style.Numberformat.Format = "yyyy-MM-dd hh:mm:ss";
                        var cellValue = property.GetValue(obj);
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
