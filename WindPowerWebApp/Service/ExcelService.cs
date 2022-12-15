using ClosedXML.Excel;
using System.ComponentModel;
using System.Reflection;

namespace WindPowerWebApp.Service
{
    public class ExcelService
    {
        public MemoryStream CreateExcel<T>(List<T> objs, string title = "SystemData", string fontFamily = "Calibri", string color = "#3498DB")
        {
            // Workbook creation.
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = addHeader(wb, objs, title, fontFamily, color);
                ws = addBody(ws, objs);
                MemoryStream XLSStream = new();
                wb.SaveAs(XLSStream);
                return XLSStream;
            }
        }

        IXLWorksheet addHeader<T>(XLWorkbook wb, List<T> objs, string title, string fontFamily, string color)
        {
            // Sheet initialisation.
            var ws = wb.Worksheets.Add("WorkSheet1").SetTabColor(XLColor.UaBlue);
            // font choice.
            ws.Style.Font.FontName = fontFamily;
            ws.Style.Font.SetFontSize(13);
            ws.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Style.Alignment.WrapText = true;
            object obj = objs.FirstOrDefault();
            // Add the model fields to the header of the excel file.
            int totalOfFields = obj.GetType().GetProperties().Length; // number of fields in the object.
            int numberOfFields = 0;
            //Looping all propeties of the object.
            foreach (var prop in obj.GetType().GetProperties())
            {
                var displayNameAttribute = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                string displayName = prop.Name;
                if (displayNameAttribute.Count() != 0)
                {
                    displayName = (displayNameAttribute[0] as DisplayNameAttribute).DisplayName;
                }
                numberOfFields++;
                ws.Cell(1, totalOfFields - numberOfFields + 1).Value = displayName;
                ws.Cell(1, totalOfFields - numberOfFields + 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(1, totalOfFields - numberOfFields + 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                ws.Cell(1, totalOfFields - numberOfFields + 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                ws.Cell(1, totalOfFields - numberOfFields + 1).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                ws.Column(totalOfFields - numberOfFields + 1).Width = 30;
                ws.Column(totalOfFields - numberOfFields + 1).Style.Font.Bold = true;
            }
            ws.Range(ws.Cell(1, 1), ws.Cell(5, totalOfFields)).SetAutoFilter();
            return ws;
        }
        IXLWorksheet addBody<T>(IXLWorksheet ws, List<T> objs)
        {
            int numberOfFields = 0;
            int numberOfRecords = 0;
            var obj = objs.FirstOrDefault();
            int totalOfFields = obj.GetType().GetProperties().Length;
            //string previousValue = "";
            //int indexOfPreviousValue = 0;

            foreach (var item in objs.ToList())
            {
                numberOfFields = 0;
                Type myType = item.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                foreach (PropertyInfo prop in props)
                {
                    var propValue = prop.GetValue(item, null);
                    numberOfFields++;
                    ws.Cell(2 + numberOfRecords, totalOfFields - numberOfFields + 1).Value = propValue;
                    ws.Cell(2 + numberOfRecords, totalOfFields - numberOfFields + 1).Style.Font.Bold = true;
                    ws.Cell(2 + numberOfRecords, totalOfFields - numberOfFields + 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Cell(2 + numberOfRecords, totalOfFields - numberOfFields + 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    ws.Cell(2 + numberOfRecords, totalOfFields - numberOfFields + 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    ws.Cell(2 + numberOfRecords, totalOfFields - numberOfFields + 1).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                    /*string propValueStr;
                    if (propValue == null)
                        propValueStr = "";
                    else
                        propValueStr = propValue.ToString();

                    if (numberOfFields == 1 && numberOfRecords == 0)
                    {
                        previousValue = propValueStr;
                    }
                    else
                    {
                        if (numberOfFields == 1)
                        {
                            if (previousValue == propValueStr)
                            {
                                ws.Range(ws.Cell(6 + numberOfRecords - (1 + indexOfPreviousValue), totalOfFields - numberOfFields + 4), ws.Cell(6 + numberOfRecords, totalOfFields - numberOfFields + 4)).Merge().Value = propValueStr;

                                ws.Range(ws.Cell(6 + numberOfRecords - (1 + indexOfPreviousValue), totalOfFields - numberOfFields + 4), ws.Cell(6 + numberOfRecords, totalOfFields - numberOfFields + 4)).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                ws.Range(ws.Cell(6 + numberOfRecords - (1 + indexOfPreviousValue), totalOfFields - numberOfFields + 4), ws.Cell(6 + numberOfRecords, totalOfFields - numberOfFields + 4)).Merge().Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                indexOfPreviousValue++;
                            }
                            else
                            {
                                previousValue = propValueStr;
                                indexOfPreviousValue = 0;
                            }
                        }
                    }*/
                }
                numberOfRecords++;
            }
            return ws;
        }

    }
}
