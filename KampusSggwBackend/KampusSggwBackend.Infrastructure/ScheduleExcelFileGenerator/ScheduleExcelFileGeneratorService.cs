namespace KampusSggwBackend.Infrastructure.ScheduleExcelFileGenerator;

using KampusSggwBackend.Infrastructure.ScheduleExcelFileGenerator.Model;
using OfficeOpenXml;
using OfficeOpenXml.Style;

/// <summary>
/// EPPlust library. Documentation: https://www.epplussoftware.com/en/Developers/
/// </summary>
public class ScheduleExcelFileGeneratorService : IScheduleExcelFileGeneratorService
{
    public byte[] GenerateExcelFile(ScheduleExcelFileModel model)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage();

        var worksheet = package.Workbook.Worksheets.Add(model.PlanName);

        worksheet.Cells[1, 1].Value = model.PlanName;
        CreateThinBorder(worksheet.Cells[1, 1]);

        worksheet.Cells[1, 2].Value = "Grupy";

        var currentTime = new TimeSpan(hours: 8, minutes: 0, seconds: 0);
        var currentTimeCellPositionColumn = 3;
        for (int i = 0; i < 47; i++)
        {
            worksheet.Cells[1, currentTimeCellPositionColumn].Value = currentTime.ToString("h\\:mm");
            currentTimeCellPositionColumn++;
            currentTime = currentTime.Add(TimeSpan.FromMinutes(15));
        }

        worksheet.Cells[2, 1].Value = "Poniedziałek";

        worksheet.Calculate();

        return package.GetAsByteArray();
    }

    private void CreateThinBorder(ExcelRange cellsRange)
    {
        cellsRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        cellsRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        cellsRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
        cellsRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
    }
}
